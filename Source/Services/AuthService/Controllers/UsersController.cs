using Microsoft.AspNetCore.Authorization;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator mediator, IMapper mapper): BaseExtendedController(mediator, mapper)
{
	[Authorize(Roles = "Admin")]
	[HttpGet(nameof(GetUsers))]
	public async Task<IActionResult> GetUsers()
	{
		var query  = new GetUsersQuery();
		var result = await mediator.Send(query);

		if(result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Ok(result.Value);
	}


	[Authorize(Roles = "Admin")]
	[HttpPost(nameof(Create))]
	public async Task<IActionResult> Create(string username, string firstName, string lastName, string email, string password, string? imageUrl)
	{
		var command = new RegisterUserCommand(username, firstName, lastName, email, password, imageUrl);
		var result  = await mediator.Send(command);

		if(result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Ok(result.Value);
	}
}
