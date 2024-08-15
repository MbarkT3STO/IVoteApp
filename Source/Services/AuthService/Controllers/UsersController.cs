using Microsoft.AspNetCore.Authorization;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator mediator, IMapper mapper): BaseExtendedController(mediator, mapper)
{
	[Authorize]
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
}
