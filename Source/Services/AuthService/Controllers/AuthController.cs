
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

		if(commandResult.IsFailure)
		{
			return BadRequest(commandResult.Error.Message);
		}

		return Ok(commandResult.Value);
	}
}
