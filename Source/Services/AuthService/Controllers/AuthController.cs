
namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator, IMapper mapper) : BaseExtendedController(mediator, mapper)
{

	[HttpPost(nameof(Login))]
	public async Task<IActionResult> Login(string username, string password)
	{
		var command = new LoginCommand(username, password);
		var commandResult = await _mediator.Send(command);

		if (commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}

		return Ok(commandResult.Value);
	}

	[HttpPost(nameof(RefreshToken))]
	public async Task<IActionResult> RefreshToken(string username, string refreshToken)
	{
		var command = new RefreshAccessTokenCommand(username, refreshToken);
		var commandResult = await _mediator.Send(command);

		if (commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}

		return Ok(commandResult.Value);
	}


	[HttpPatch(nameof(InvalidateRefreshToken))]
	public async Task<IActionResult> InvalidateRefreshToken(string username, string refreshToken)
	{
		var command = new InvalidateRefreshTokenCommand(username, refreshToken);
		var commandResult = await _mediator.Send(command);

		if (commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}

		return Ok(new { message = "Refresh token successfully invalidated" });
	}

	[HttpPost(nameof(RevokeRefreshToken))]
	public async Task<IActionResult> RevokeRefreshToken(string username, string refreshToken)
	{
		var command = new RevokeRefreshTokenCommand(username, refreshToken);
		var commandResult = await _mediator.Send(command);

		if (commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}

		return Ok(new { message = "Refresh token successfully revoked" });
	}
}
