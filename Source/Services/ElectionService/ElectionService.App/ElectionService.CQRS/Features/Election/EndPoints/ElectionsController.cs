using ElectionService.CQRS.Features.Election.Commands;
using ElectionService.CQRS.Features.Election.Queries;
using Microsoft.AspNetCore.Authorization;

namespace ElectionService.CQRS.Features.EndPoints;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class ElectionsController: BaseExtendedController
{
	public ElectionsController(IMediator mediator): base(mediator)
	{
	}

	[HttpGet(nameof(Get))]
	public async Task<IActionResult> Get()
	{
		var cacheKey = $"{nameof(GetElectionsQuery)}";
		var query    = GetElectionsQuery.CreateCachedQuery(cacheKey);
		var result   = await _mediator.Send(query);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}


	[HttpGet(nameof(GetFromPage))]
	public async Task<IActionResult> GetFromPage(int page)
	{
		var cacheKey = $"{nameof(GetElectionsQuery)}-page-{page}-pageSize-10";
		var query    = GetElectionsQuery.CreateCachedAndPaginatedQuery(cacheKey, page);
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
		var cacheKey = $"{nameof(GetElectionByIdQuery)}-{id}";
		var query    = GetElectionByIdQuery.CreateCachedQuery(cacheKey).WithElectionId(id);
		var result   = await _mediator.Send(query);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}




	[HttpPost(nameof(Create))]
	public async Task<IActionResult> Create(string title, string description, DateTime startDateAndTime, DateTime endDateAndTime, ElectionStatus status)
	{
		var userId  = GetUserId();
		var command = new CreateElectionCommand(title, description, startDateAndTime, endDateAndTime, status, userId);
		var result  = await _mediator.Send(command);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}


	[HttpPost(nameof(UpdateStatus))]
	public async Task<IActionResult> UpdateStatus(Guid id, ElectionStatus status)
	{
		var userId  = GetUserId();
		var command = new UpdateElectionStatusCommand(id, status, userId);
		var result  = await _mediator.Send(command);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}


	[HttpPost(nameof(UpdateTitle))]
	public async Task<IActionResult> UpdateTitle(Guid id, string title)
	{
		var userId  = GetUserId();
		var command = new UpdateElectionTitleCommand(id, title, userId);
		var result  = await _mediator.Send(command);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}

}
