using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthService.Common.Services;

public class JWTService
{
	readonly JwtSettings jwtSettings = null!;

	public JWTService(JwtSettings jwtSettings)
	{
		this.jwtSettings = jwtSettings;
	}

	/// <summary>
	/// Generates a JWT token.
	/// </summary>
	public (string Token, DateTime ValidTo) GenerateJwtToken(AppUser user, string? role = null)
	{
		var claims = new Claim[]
		{
			new (JwtRegisteredClaimNames.Sub, user.Email.ToString()),
			new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		};

		if (!string.IsNullOrEmpty(role))
		{
			claims = claims
				.Append(new Claim(ClaimTypes.Role, role))
				.ToArray();
		}

		var securityKey        = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
		var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer            : jwtSettings.Issuer,
			audience          : jwtSettings.Audience,
			claims            : claims,
			expires           : DateTime.UtcNow.AddMinutes(1),
			signingCredentials: signingCredentials
		);

		var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

		return (encodedToken, token.ValidTo);
	}

	/// <summary>
	/// Generates a new refresh token.
	/// </summary>
	public RefreshToken GenerateRefreshToken(AppUser user)
	{
		var refreshToken = new RefreshToken
		{
			UserId    = user.Id,
			Token     = Guid.NewGuid().ToString(),
			CreatedAt = DateTime.UtcNow,
			ExpiresAt = DateTime.UtcNow.AddMinutes(10)
		};

		return refreshToken;
	}

	/// <summary>
	/// Validates a refresh token and returns its validation result.
	/// </summary>
	public RefreshTokenValidationResult ValidateRefreshToken(RefreshToken refreshToken)
	{
		if (refreshToken.IsExpired || refreshToken.ExpiresAt < DateTime.UtcNow)
		{
			return RefreshTokenValidationResult.Invalid;
		}

		if (refreshToken.RevokedAt != null)
		{
			return RefreshTokenValidationResult.Invalid;
		}

		if(refreshToken.IsInvalidated)
		{
			return RefreshTokenValidationResult.Invalid;
		}

		return RefreshTokenValidationResult.Valid;
	}
}
