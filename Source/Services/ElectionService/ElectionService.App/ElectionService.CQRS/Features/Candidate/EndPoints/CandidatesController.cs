using ElectionService.CQRS.Features.Candidate.Commands;
using ElectionService.CQRS.Features.Candidate.Queries;
using Microsoft.AspNetCore.Authorization;

namespace ElectionService.CQRS.Features.Candidate.EndPoints;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class CandidatesController: BaseExtendedController
{
	public CandidatesController(IMediator mediator): base(mediator)
	{
	}


	[HttpGet(nameof(Get))]
	public async Task<IActionResult> Get()
	{
		var cacheKey = $"{nameof(GetCandidatesQuery)}";
		var query    = GetCandidatesQuery.CreateCachedQuery(cacheKey);
		var result   = await _mediator.Send(query);

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
		var query    = GetCandidatesQuery.CreateCachedAndPaginatedQuery(cacheKey, pageNumber);
		var result   = await _mediator.Send(query);

		if (result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}

	[HttpGet(nameof(GetById))]
	public async Task<IActionResult> GetById(Guid id)
	{
		var cacheKey = $"{nameof(GetCandidateByIdQuery)}-{id}";
		var query    = GetCandidateByIdQuery.CreateCachedQuery(cacheKey).WithCandidateId(id);
		var result   = await _mediator.Send(query);

		if (result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}

	[HttpGet(nameof(GetByElectionId))]
	public async Task<IActionResult> GetByElectionId(Guid id)
	{
		var cacheKey = $"{nameof(GetCandidatesByElectionIdQuery)}-{id}";
		var query    = GetCandidatesByElectionIdQuery.CreateCachedQuery(cacheKey).WithElectionId(id);
		var result   = await _mediator.Send(query);

		if (result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}



	[HttpPost(nameof(Create))]
	public async Task<IActionResult> Create(Guid electionId, Guid politicalPartyId, string name, string description, string photoUrl)
	{
		var userId  = GetUserId();
		var command = new CreateCandidateCommand(electionId, politicalPartyId, name, description, photoUrl, userId);
		var result  = await _mediator.Send(command);

		if (result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}

	[HttpPut(nameof(Update))]
	public async Task<IActionResult> Update(Guid id, Guid politicalPartyId, Guid electionId, string name, string description, string photoUrl)
	{
		var userId  = GetUserId();
		var command = UpdateCandidateCommand.Create(id, politicalPartyId, electionId, name, description, photoUrl, userId);
		var result  = await _mediator.Send(command);

		if (result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}

}
