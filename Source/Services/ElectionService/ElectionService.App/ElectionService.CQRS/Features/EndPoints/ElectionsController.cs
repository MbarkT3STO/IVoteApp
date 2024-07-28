using ElectionService.CQRS.Features.Election.Queries;

namespace ElectionService.CQRS.Features.EndPoints;

[ApiController]
[Route("api/[controller]")]
public class ElectionsController : ControllerBase
{
	readonly IMediator mediator;

	public ElectionsController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	[HttpGet]
	[Route(nameof(Get))]
	public async Task<IActionResult> Get()
	{
		var result = await mediator.Send(new GetElectionsQuery());

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}
}
