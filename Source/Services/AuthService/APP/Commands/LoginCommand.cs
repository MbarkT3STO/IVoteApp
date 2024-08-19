using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace AuthService.APP.Commands;

public class LoginCommandResultDto
{
	public string Token { get; set; }
	public string RefreshToken { get; set; }
	public DateTime TokenExpiration { get; set; }
	public DateTime RefreshTokenExpiration { get; set; }

	public LoginCommandResultDto(string token, DateTime tokenExpiration, string refreshToken, DateTime refreshTokenExpiration)
	{
		Token                  = token;
		TokenExpiration        = tokenExpiration;
		RefreshToken           = refreshToken;
		RefreshTokenExpiration = refreshTokenExpiration;
	}
}

public class LoginCommandResult: AppCommandResult<LoginCommandResultDto, LoginCommandResult>
{
	public LoginCommandResult(LoginCommandResultDto value): base(value)
	{
	}

	public LoginCommandResult(Error error): base(error)
	{
	}
}

/// <summary>
/// Represents the login command.
/// </summary>
public class LoginCommand: AppCommand<LoginCommand, LoginCommandResult>
{
	public string UserName { get; set; }
	public string Password { get; set; }

	public LoginCommand(string userName, string password)
	{
		UserName = userName;
		Password = password;
	}
}


public class LoginCommandHandler: BaseAppCommandHandler<LoginCommand, LoginCommandResult, LoginCommandResultDto>
{
	public LoginCommandHandler(AppDbContext context, IMapper mapper, IMediator mediator, UserManager<AppUser> userManager, IOptions<JwtSettings> jwtSettingsOptions, JWTService jwtService): base(context, mapper, mediator, userManager, jwtSettingsOptions, jwtService)
	{

	}


	protected override async Task<LoginCommandResult> HandleCore(LoginCommand command, CancellationToken cancellationToken)
	{
		var user = await _userManager.FindByNameAsync(command.UserName);

		if (user == null)
			return FailedResult("Invalid username And/Or password");

		var passwordValid = await _userManager.CheckPasswordAsync(user, command.Password);

		if (!passwordValid)
			return FailedResult("Invalid username And/Or password");

		var userRoles = await _userManager.GetRolesAsync(user);

		if (userRoles.Count == 0)
			return FailedResult("User has no roles");

		var (AccessToken, AccessTokenExpirationDate) = _jwtService.GenerateJwtToken(user, userRoles.FirstOrDefault());
		var refreshToken                             = _jwtService.GenerateRefreshToken(user);

		await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);

		var resultValue = new LoginCommandResultDto(AccessToken, AccessTokenExpirationDate, refreshToken.Token, refreshToken.ExpiresAt);

		return SucceededResult(resultValue);
	}


	/// <summary>
	/// Generates a JWT token.
	/// </summary>
	private (string Token, DateTime ValidTo) GenerateJwtToken(AppUser user)
	{
		var claims = new Claim[]
		{
			new (JwtRegisteredClaimNames.Sub, user.Email.ToString()),
			new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		};

		var securityKey        = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
		var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer            : _jwtSettings.Issuer,
			audience          : _jwtSettings.Audience,
			claims            : claims,
			expires           : DateTime.UtcNow.AddMinutes(1),
			signingCredentials: signingCredentials
		);

		var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

		return (encodedToken, token.ValidTo);
	}


	/// <summary>
	/// Creates a new refresh token.
	/// </summary>
	private async Task<RefreshToken> CreateRefreshToken(AppUser user)
	{
		var refreshToken = new RefreshToken
		{
			UserId    = user.Id,
			Token     = Guid.NewGuid().ToString(),
			CreatedAt = DateTime.Now,
			ExpiresAt = DateTime.Now.AddMinutes(10)
		};

		await _dbContext.RefreshTokens.AddAsync(refreshToken);
		await _dbContext.SaveChangesAsync();

		return refreshToken;
	}
}
