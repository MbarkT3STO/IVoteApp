

using Microsoft.Extensions.Options;

namespace AuthService.APP.Commands;

public class ResetPasswordCommandResult: AppCommandResult
{
	public ResetPasswordCommandResult(Error error): base(error)
	{
	}

	public ResetPasswordCommandResult(bool isSuccess): base(isSuccess)
	{
	}
}

/// <summary>
/// Represents a command to reset the password.
/// </summary>
public class ResetPasswordCommand: AppCommand<ResetPasswordCommand, ResetPasswordCommandResult>
{
	public string UserName { get; }
	public string CurrentPassword { get; }
	public string NewPassword { get; }
	public string ConfirmPassword { get; }

	public ResetPasswordCommand(string userName, string currentPassword, string newPassword, string confirmPassword)
	{
		UserName        = userName;
		CurrentPassword = currentPassword;
		NewPassword     = newPassword;
		ConfirmPassword = confirmPassword;
	}
}

public class ResetPasswordCommandHandler: BaseAppCommandHandler<ResetPasswordCommand, ResetPasswordCommandResult>
{
	public ResetPasswordCommandHandler(AppDbContext context, IMapper mapper, IMediator mediator, UserManager<AppUser> userManager, IOptions<JwtSettings> jwtSettingsOptions, JWTService jwtService): base(context, mapper, mediator, userManager, jwtSettingsOptions, jwtService)
	{
	}

	protected override async Task<ResetPasswordCommandResult> HandleCore(ResetPasswordCommand command, CancellationToken cancellationToken)
	{
		var user = await GetUserByNameOrThrowAsync(command.UserName);

		// Check if current password is correct
		var isCurrentPasswordCorrect = await _userManager.CheckPasswordAsync(user, command.CurrentPassword);

		if (!isCurrentPasswordCorrect) return FailedResult("Current password is incorrect");

		// Check if password and confirm password match
		if (!IsPasswordAndConfirmPasswordMatch(command.NewPassword, command.ConfirmPassword)) return FailedResult("Password and confirm password do not match");

		// Reset password
		var resetPasswordToken  = await _userManager.GeneratePasswordResetTokenAsync(user);
		var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetPasswordToken, command.NewPassword);

		if (!resetPasswordResult.Succeeded)
		{
			return FailedResult(resetPasswordResult.Errors.FirstOrDefault()?.Description);
		}

		return SucceededResult();
	}

	/// <summary>
	/// Checks if password and confirm password match or not
	/// </summary>
	private bool IsPasswordAndConfirmPasswordMatch(string password, string confirmPassword)
	{
		return password == confirmPassword;
	}
}