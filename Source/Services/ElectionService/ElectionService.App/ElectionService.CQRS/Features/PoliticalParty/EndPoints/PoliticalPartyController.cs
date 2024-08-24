using ElectionService.CQRS.Features.PoliticalParty.Commands;
using ElectionService.CQRS.Features.PoliticalParty.Queries;
using Microsoft.AspNetCore.Authorization;

namespace ElectionService.CQRS.Features.PoliticalParty.EndPoints;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class PoliticalPartyController: BaseExtendedController
{
    public PoliticalPartyController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet(nameof(Get))]
	public async Task<IActionResult> Get()
	{
		var cacheKey = $"{nameof(GetPoliticalPartiesQuery)}";
		var query    = GetPoliticalPartiesQuery.CreateCachedQuery(cacheKey);
		var result   = await _mediator.Send(query);

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
		var query    = GetPoliticalPartyByIdQuery.CreateCachedQuery(cacheKey).WithPoliticalPartyId(id);
		var result   = await _mediator.Send(query);

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
		var query    = GetPoliticalPartiesQuery.CreateCachedAndPaginatedQuery(cacheKey, pageNumber);
		var result   = await _mediator.Send(query);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}


	[HttpPost(nameof(Create))]
	public async Task<IActionResult> Create(string name, string description, DateTime establishmentDate, string logoUrl, string websiteUrl)
	{
		var userId  = GetUserId();
		var command = CreatePoliticalPartyCommand.Create(name, description, establishmentDate, logoUrl, websiteUrl, userId);
		var result  = await _mediator.Send(command);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error.Message);
	}


	[HttpPost(nameof(Update))]
	public async Task<IActionResult> Update(Guid id, string name, string description, DateTime establishmentDate, string logoUrl, string webSiteUrl)
	{
		var userId  = GetUserId();
		var command = UpdatePoliticalPartyCommand.Create(id, name, description, establishmentDate, logoUrl, webSiteUrl, userId);
		var result  = await _mediator.Send(command);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}
}
