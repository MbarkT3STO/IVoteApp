
using AuthService.Extensions;
using Microsoft.Extensions.Options;

namespace AuthService.APP.Commands;

public class RefreshAccessTokenCommandResultDto
{
	public string Token { get; set; }
	public string RefreshToken { get; set; }
	public DateTime TokenExpiration { get; set; }
	public DateTime RefreshTokenExpiration { get; set; }

	public RefreshAccessTokenCommandResultDto(string token, DateTime tokenExpiration, string refreshToken, DateTime refreshTokenExpiration)
	{
		Token                  = token;
		TokenExpiration        = tokenExpiration;
		RefreshToken           = refreshToken;
		RefreshTokenExpiration = refreshTokenExpiration;
	}
}

public class RefreshAccessTokenCommandResult: AppCommandResult<RefreshAccessTokenCommandResultDto, RefreshAccessTokenCommandResult>
{
	public RefreshAccessTokenCommandResult(RefreshAccessTokenCommandResultDto value): base(value)
	{
	}

	public RefreshAccessTokenCommandResult(Error error): base(error)
	{
	}
}

public class RefreshAccessTokenCommand: AppCommand<RefreshAccessTokenCommand, RefreshAccessTokenCommandResult>
{
	public string UserName { get; set; }
	public string RefreshToken { get; set; }

	public RefreshAccessTokenCommand(string userName, string refreshToken)
	{
		UserName     = userName;
		RefreshToken = refreshToken;
	}
}

public class RefreshAccessTokenCommandHandler: BaseAppCommandHandler<RefreshAccessTokenCommand, RefreshAccessTokenCommandResult, RefreshAccessTokenCommandResultDto>
{
	public RefreshAccessTokenCommandHandler(AppDbContext context, IMapper mapper, IMediator mediator, UserManager<AppUser> userManager, IOptions<JwtSettings> jwtSettingsOptions, JWTService jwtService): base(context, mapper, mediator, userManager, jwtSettingsOptions, jwtService)
	{

	}

	protected override async Task<RefreshAccessTokenCommandResult> HandleCore(RefreshAccessTokenCommand command, CancellationToken cancellationToken)
	{
		var (isUserExistsAndRefreshTokenIsValid, isUserExistsAndRefreshTokenIsValidError, user, refreshToken) = await IsUserExistsAndRefreshTokenIsValidAsync(command.UserName, command.RefreshToken, cancellationToken);

		if (isUserExistsAndRefreshTokenIsValid == false)
		{
			return FailedResult(isUserExistsAndRefreshTokenIsValidError);
		}

		var (accessToken, accessTokenExpirationDate) = _jwtService.GenerateJwtToken(user);
		var resultValue                              = new RefreshAccessTokenCommandResultDto(accessToken, accessTokenExpirationDate, command.RefreshToken, DateTime.Now.AddMinutes(10));

		refreshToken.MarkAsUsed();
		await _dbContext.SaveChangesAsync(cancellationToken);

		return SucceededResult(resultValue);
	}


	/// <summary>
	/// Checks if user exists and refresh token is valid.
	/// </summary>
	/// <returns>Returns a tuple of (isUserExistsAndRefreshTokenIsValid, error, user, refreshToken)</returns>
	private async Task<(bool IsUserExistsAndRefreshTokenIsValid, Error? Error, AppUser? User, RefreshToken? RefreshToken)> IsUserExistsAndRefreshTokenIsValidAsync(string userName, string refreshToken, CancellationToken cancellationToken)
	{
		var user = await _userManager.FindByNameAsync(userName);

		if (user == null)
		{
			return (false, new Error("User not found"), null , null);
		}

		var refreshTokenEntity = await _dbContext.RefreshTokens
			.FirstOrDefaultAsync(x => x.UserId == user.Id && x.Token == refreshToken, cancellationToken);

		if (refreshTokenEntity is null)
		{
			return (false, new Error("Refresh token not found"), null , null);
		}

		var refreshTokenStatus = refreshTokenEntity.GetStatus();

		if (refreshTokenStatus == RefreshTokenStatus.Expired || refreshTokenStatus == RefreshTokenStatus.Revoked || refreshTokenStatus == RefreshTokenStatus.Invalidated)
		{
			if(refreshTokenEntity.IsExpiredAndNotMarkedAsExpiredYet())
			{
				refreshTokenEntity.MarkAsExpired();
				await _dbContext.SaveChangesAsync(cancellationToken);
			}

			return (false, new Error("Refresh token Expired, Revoked or Invalidated"), null , null);
		}

		return (true, null, user, refreshTokenEntity);
	}
}
