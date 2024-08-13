using ElectionService.CQRS.Common.Base;
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

	[HttpGet(nameof(Get))]
	public async Task<IActionResult> Get()
	{
		var cacheKey = $"{nameof(GetPoliticalPartiesQuery)}";
		var query = GetPoliticalPartiesQuery.CreateCachedQuery(cacheKey);
		var result = await mediator.Send(query);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}


	[HttpGet(nameof(GetById))]
	public async Task<IActionResult> GetById(Guid id)
	{
		var cacheKey = $"{nameof(GetPoliticalPartyByIdQuery)}-{id}";
		var query = GetPoliticalPartyByIdQuery.CreateCachedQuery(cacheKey).WithPoliticalPartyId(id);
		var result = await mediator.Send(query);

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
