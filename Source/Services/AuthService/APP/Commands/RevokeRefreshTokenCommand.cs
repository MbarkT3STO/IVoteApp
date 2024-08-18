using Microsoft.Extensions.Options;

namespace AuthService.APP.Commands;

public class RevokeRefreshTokenCommandResult : AppCommandResult
{
    public RevokeRefreshTokenCommandResult(Error error) : base(error)
    {
    }

    public RevokeRefreshTokenCommandResult(bool isSuccess) : base(isSuccess)
    {
    }
}

/// <summary>
/// Represents the revoke refresh token command.
/// </summary>
public class RevokeRefreshTokenCommand : AppCommand<RevokeRefreshTokenCommand, RevokeRefreshTokenCommandResult>
{
    public string UserName { get; set; }
    public string RefreshToken { get; set; }

    public RevokeRefreshTokenCommand(string userName, string refreshToken)
    {
        UserName = userName;
        RefreshToken = refreshToken;
    }
}

public class RevokeRefreshTokenCommandHandler : BaseAppCommandHandler<RevokeRefreshTokenCommand, RevokeRefreshTokenCommandResult>
{
    public RevokeRefreshTokenCommandHandler(AppDbContext context, IMapper mapper, IMediator mediator, UserManager<AppUser> userManager, IOptions<JwtSettings> jwtSettingsOptions, JWTService jwtService) : base(context, mapper, mediator, userManager, jwtSettingsOptions, jwtService)
    {
    }

    protected override async Task<RevokeRefreshTokenCommandResult> HandleCore(RevokeRefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var user = await GetUserByNameOrThrowAsync(command.UserName);
        var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == command.RefreshToken && x.UserId == user.Id, cancellationToken);

        if (refreshToken is null)
        {
            return FailedResult("Refresh token not found");
        }

        // Revoke refresh token
        refreshToken.UpdateStatus(RefreshTokenStatus.Revoked);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return SucceededResult();
    }
}
