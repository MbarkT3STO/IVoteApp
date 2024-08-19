using Microsoft.Extensions.Options;

namespace AuthService.Common.Base;

/// <summary>
/// Represents a base class for command handlers.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
/// <typeparam name="TCommandResult">The type of the command result.</typeparam>
public abstract class BaseAppCommandHandler<TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult>
where TCommand : AppCommand<TCommand, TCommandResult>
where TCommandResult : class, ICommandResult
{
	private protected readonly IMediator _mediator;
	private protected readonly IMapper _mapper;
	private protected readonly AppDbContext _dbContext;
	private protected readonly JwtSettings _jwtSettings;
	private protected readonly JWTService _jwtService;
	private protected readonly UserManager<AppUser> _userManager;

	protected BaseAppCommandHandler(AppDbContext context, IMapper mapper, IMediator mediator, UserManager<AppUser> userManager, IOptions<JwtSettings> jwtSettingsOptions, JWTService jwtService)
	{
		_mediator = mediator;
		_mapper = mapper;
		_dbContext = context;
		_userManager = userManager;
		_jwtSettings = jwtSettingsOptions.Value;
		_jwtService = jwtService;
	}


	public virtual async Task<TCommandResult> Handle(TCommand command, CancellationToken cancellationToken)
	{
		try
		{
			var result = await HandleCore(command, cancellationToken);
			command.Result = result;

			return result;
		}
		catch (Exception ex)
		{
			var error = Error.FromException(ex);
			return (TCommandResult)Activator.CreateInstance(typeof(TCommandResult), error)!;
		}
	}


	/// <summary>
	/// Gets the user by name or throws an exception if not exists.
	/// </summary>
	/// <param name="userName">The user username.</param>
	protected async Task<AppUser> GetUserByNameOrThrowAsync(string userName)
	{
		var user = await _userManager.FindByNameAsync(userName);

		if (user is null)
		{
			throw new Exception($"User with name {userName} not found");
		}

		return user;
	}


	/// <summary>
	/// Handles the core logic of the Command.
	/// </summary>
	/// <param name="command">The command.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	protected abstract Task<TCommandResult> HandleCore(TCommand command, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a succeeded <typeparamref name="TCommandResult"/>.
	/// </summary>
	protected TCommandResult SucceededResult() => (TCommandResult)Activator.CreateInstance(typeof(TCommandResult), [true])!;

	/// <summary>
	/// Creates a failed <typeparamref name="TCommandResult"/> with the specified error message.
	/// </summary>
	/// <param name="message">The error message.</param>
	protected TCommandResult FailedResult(string message) => (TCommandResult)Activator.CreateInstance(typeof(TCommandResult), new Error(message))!;

	/// <summary>
	/// Creates a failed <typeparamref name="TCommandResult"/> with the specified error.
	/// </summary>
	/// <param name="error">The error.</param>
	protected TCommandResult FailedResult(Error error) => (TCommandResult)Activator.CreateInstance(typeof(TCommandResult), error)!;
}













/// <summary>
/// Represents a base class for command handlers.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
/// <typeparam name="TCommandResult">The type of the command result.</typeparam>
/// <typeparam name="TCommandResultValue">The type of the command result value.</typeparam>
public abstract class BaseAppCommandHandler<TCommand, TCommandResult, TCommandResultValue> : IRequestHandler<TCommand, TCommandResult>
where TCommand : AppCommand<TCommand, TCommandResult>
where TCommandResult : class, ICommandResult<TCommandResultValue>
{
	private protected readonly IMediator _mediator;
	private protected readonly IMapper _mapper;
	private protected readonly AppDbContext _dbContext;
	private protected readonly JwtSettings _jwtSettings;
	private protected readonly JWTService _jwtService;
	private protected readonly UserManager<AppUser> _userManager;


	protected BaseAppCommandHandler(AppDbContext context, IMapper mapper, IMediator mediator, UserManager<AppUser> userManager,  IOptions<JwtSettings> jwtSettingsOptions, JWTService jwtService)
	{
		_mediator = mediator;
		_mapper = mapper;
		_dbContext = context;
		_userManager = userManager;
		_jwtSettings= jwtSettingsOptions.Value;
		_jwtService = jwtService;
	}

	public virtual async Task<TCommandResult> Handle(TCommand command, CancellationToken cancellationToken)
	{
		try
		{
			var result = await HandleCore(command, cancellationToken);
			command.Result = result;

			return result;
		}
		catch (Exception ex)
		{
			var error = Error.FromException(ex);
			return (TCommandResult)Activator.CreateInstance(typeof(TCommandResult), error)!;
		}
	}


	/// <summary>
	/// Gets the user by name or throws an exception if not exists.
	/// </summary>
	/// <param name="userName">The user username.</param>
	protected async Task<AppUser> GetUserByNameOrThrowAsync(string userName)
	{
		var user = await _userManager.FindByNameAsync(userName);

		if (user is null)
		{
			throw new Exception($"User with name {userName} not found");
		}

		return user;
	}

	/// <summary>
	/// Checks if user exists by username.
	/// </summary>
	protected async Task<bool> IsUserExistsByNameAsync(string userName, CancellationToken cancellationToken)
	{
		var user = await _userManager.FindByNameAsync(userName);

		return user is not null;
	}

	/// <summary>
	/// Checks if user exists by email.
	/// </summary>
	protected async Task<bool> IsUserExistsByEmailAsync(string email, CancellationToken cancellationToken)
	{
		var user = await _userManager.FindByEmailAsync(email);

		return user is not null;
	}

	/// <summary>
	/// Handles the core logic of the Command.
	/// </summary>
	/// <param name="command">The command.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	protected abstract Task<TCommandResult> HandleCore(TCommand command, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a succeeded <typeparamref name="TCommandResult"/> with the specified value.
	/// </summary>
	/// <param name="value">The value of the command result.</param>
	protected TCommandResult SucceededResult(TCommandResultValue value) => (TCommandResult)Activator.CreateInstance(typeof(TCommandResult), value)!;

	/// <summary>
	/// Creates a failed <typeparamref name="TCommandResult"/> with the specified error message.
	/// </summary>
	/// <param name="message">The error message.</param>
	protected TCommandResult FailedResult(string message) => (TCommandResult)Activator.CreateInstance(typeof(TCommandResult), new Error(message))!;

	/// <summary>
	/// Creates a failed <typeparamref name="TCommandResult"/> with the specified error.
	/// </summary>
	/// <param name="error">The error.</param>
	protected TCommandResult FailedResult(Error error) => (TCommandResult)Activator.CreateInstance(typeof(TCommandResult), error)!;
}
