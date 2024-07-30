using ElectionService.CQRS.Features.PoliticalParty.Commands;
using ElectionService.CQRS.Features.PoliticalParty.Queries;

namespace ElectionService.CQRS.Features.PoliticalParty.EndPoints;

[ApiController]
[Route("api/[controller]")]
public class PoliticalPartyController : ControllerBase
{
	readonly IMediator mediator;

	public PoliticalPartyController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	[HttpGet]
	[Route(nameof(Get))]
	public async Task<IActionResult> Get()
	{
		var result = await mediator.Send(new GetPoliticalPartiesQuery());

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}


	[HttpPost(nameof(Create))]
	public async Task<IActionResult> Create(CreatePoliticalPartyCommand command)
	{
		var result = await mediator.Send(command);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}
}
