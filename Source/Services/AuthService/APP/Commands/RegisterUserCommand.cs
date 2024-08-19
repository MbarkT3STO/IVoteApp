
using Microsoft.Extensions.Options;

namespace AuthService.APP.Commands;

public class RegisterUserCommandResultDto
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string? ImageUrl { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}

public class RegisterUserCommandResult: AppCommandResult<RegisterUserCommandResultDto, RegisterUserCommandResult>
{
	public RegisterUserCommandResult(RegisterUserCommandResultDto value): base(value)
	{
	}

	public RegisterUserCommandResult(Error error): base(error)
	{
	}
}

public class RegisterUserCommandMapProfile: Profile
{
	public RegisterUserCommandMapProfile()
	{
		CreateMap<AppUser, RegisterUserCommandResultDto>();
	}
}



/// <summary>
/// Represents a command to register a new user.
/// </summary>
public class RegisterUserCommand: AppCommand<RegisterUserCommand, RegisterUserCommandResult>
{
	public string UserName { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string? ImageUrl { get; set; }

	public RegisterUserCommand(string userName, string firstName, string lastName, string email, string password, string? imageUrl)
	{
		UserName  = userName;
		FirstName = firstName;
		LastName  = lastName;
		Email     = email;
		Password  = password;
		ImageUrl  = imageUrl;
	}
}


public class RegisterUserCommandHandler: BaseAppCommandHandler<RegisterUserCommand, RegisterUserCommandResult, RegisterUserCommandResultDto>
{
	public RegisterUserCommandHandler(AppDbContext context, IMapper mapper, IMediator mediator, UserManager<AppUser> userManager, IOptions<JwtSettings> jwtSettingsOptions, JWTService jwtService): base(context, mapper, mediator, userManager, jwtSettingsOptions, jwtService)
	{
	}

	protected override async Task<RegisterUserCommandResult> HandleCore(RegisterUserCommand command, CancellationToken cancellationToken)
	{
		// Check if already a user with same username and/or email exists
		var isUserExistsByName  = await IsUserExistsByNameAsync(command.UserName, cancellationToken);
		var isUserExistsByEmail = await IsUserExistsByEmailAsync(command.Email, cancellationToken);

		if (isUserExistsByName || isUserExistsByEmail)
		{
			return FailedResult("User with same username and/or email already exists");
		}

		// Add user
		var user = new AppUser
		{
			UserName  = command.UserName,
			Email     = command.Email,
			FirstName = command.FirstName,
			LastName  = command.LastName,
			ImageUrl  = command.ImageUrl
		};

		var createUserResult = await _userManager.CreateAsync(user, command.Password);

		if (!createUserResult.Succeeded)
		{
			return FailedResult(createUserResult.Errors.FirstOrDefault()?.Description);
		}

		// Add user to role
		var addUserToRoleResult = await _userManager.AddToRoleAsync(user, "User");

		if (!addUserToRoleResult.Succeeded)
		{
			return FailedResult(addUserToRoleResult.Errors.FirstOrDefault()?.Description);
		}

		return SucceededResult(_mapper.Map<RegisterUserCommandResultDto>(user));
	}
}
