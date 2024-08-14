using ElectionService.CQRS.Features.Candidate.Commands;
using ElectionService.CQRS.Features.Candidate.Queries;

namespace ElectionService.CQRS.Features.Candidate.EndPoints;

[ApiController]
[Route("api/[controller]")]
public class CandidatesController(IMediator mediator) : ControllerBase
{
	readonly IMediator mediator = mediator;

	[HttpGet(nameof(Get))]
	public async Task<IActionResult> Get()
	{
		var cacheKey = $"{nameof(GetCandidatesQuery)}";
		var query = GetCandidatesQuery.CreateCachedQuery(cacheKey);
		var result = await mediator.Send(query);

		if (result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}

	[HttpGet(nameof(GetFromPage))]
	public async Task<IActionResult> GetFromPage(int pageNumber)
	{
		var cacheKey = $"{nameof(GetCandidatesQuery)}-page-{pageNumber}-pageSize-10";
		var query = GetCandidatesQuery.CreateCachedAndPaginatedQuery(cacheKey, pageNumber);
		var result = await mediator.Send(query);

		if (result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}


	[HttpPost(nameof(Create))]
	public async Task<IActionResult> Create(CreateCandidateCommand command)
	{
		var result = await mediator.Send(command);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}


	[HttpGet(nameof(GetById))]
	public async Task<IActionResult> GetById(Guid id)
	{
		var cacheKey = $"{nameof(GetCandidateByIdQuery)}-{id}";
		var query = GetCandidateByIdQuery.CreateCachedQuery(cacheKey).WithCandidateId(id);
		var result = await mediator.Send(query);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}


	[HttpPut(nameof(Update))]
	public async Task<IActionResult> Update(Guid id, string name, string description, string photoUrl, string webSiteUrl, Guid politicalPartyId, Guid electionId)
	{
		var command = UpdateCandidateCommand.Create(id, name, description, photoUrl, politicalPartyId, electionId);
		var result = await mediator.Send(command);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}

}
