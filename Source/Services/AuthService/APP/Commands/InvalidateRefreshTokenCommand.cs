
using Microsoft.Extensions.Options;

namespace AuthService.APP.Commands;

public class InvalidateRefreshTokenCommandResult: AppCommandResult
{
	public InvalidateRefreshTokenCommandResult(Error error): base(error)
	{
	}

	public InvalidateRefreshTokenCommandResult(bool isSuccess): base(isSuccess)
	{
	}
}

/// <summary>
/// Represents the invalidate refresh token command.
/// </summary>
public class InvalidateRefreshTokenCommand: AppCommand<InvalidateRefreshTokenCommand, InvalidateRefreshTokenCommandResult>
{
	public string UserName { get; set; }
	public string RefreshToken { get; set; }

	public InvalidateRefreshTokenCommand(string userName, string refreshToken)
	{
		UserName     = userName;
		RefreshToken = refreshToken;
	}
}

public class InvalidateRefreshTokenCommandHandler: BaseAppCommandHandler<InvalidateRefreshTokenCommand, InvalidateRefreshTokenCommandResult>
{
	public InvalidateRefreshTokenCommandHandler(AppDbContext context, IMapper mapper, IMediator mediator, UserManager<AppUser> userManager, IOptions<JwtSettings> jwtSettingsOptions, JWTService jwtService): base(context, mapper, mediator, userManager, jwtSettingsOptions, jwtService)
	{
	}

	protected override async Task<InvalidateRefreshTokenCommandResult> HandleCore(InvalidateRefreshTokenCommand command, CancellationToken cancellationToken)
	{
		var user         = await GetUserByNameOrThrowAsync(command.UserName);
		var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == command.RefreshToken && x.UserId == user.Id, cancellationToken);

		if (refreshToken is null)
		{
			return FailedResult("Refresh token not found");
		}

		// Invalidate refresh token
		refreshToken.UpdateStatus(RefreshTokenStatus.Invalidated);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return SucceededResult();
	}
}
