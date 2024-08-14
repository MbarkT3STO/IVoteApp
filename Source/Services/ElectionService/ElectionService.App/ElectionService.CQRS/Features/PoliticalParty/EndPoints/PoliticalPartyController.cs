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


	[HttpGet(nameof(GetFromPage))]
	public async Task<IActionResult> GetFromPage(int pageNumber)
	{
		var cacheKey = $"{nameof(GetPoliticalPartiesQuery)}-page-{pageNumber}-pageSize-10";
		var query = GetPoliticalPartiesQuery.CreateCachedAndPaginatedQuery(cacheKey, pageNumber);
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


	[HttpPost(nameof(Update))]
	public async Task<IActionResult> Update(Guid id, string name, string description, DateTime establishmentDate, string logoUrl, string webSiteUrl)
	{
		var command = UpdatePoliticalPartyCommand.Create(id, name, description, establishmentDate, logoUrl, webSiteUrl);
		var result = await mediator.Send(command);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}
}
