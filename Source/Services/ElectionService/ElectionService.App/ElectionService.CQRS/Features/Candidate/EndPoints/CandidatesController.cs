using System.Diagnostics;
using ElectionService.CQRS.Features.Candidate.Commands;
using ElectionService.CQRS.Features.Candidate.Queries;

namespace ElectionService.CQRS.Features.Candidate.EndPoints;

[ApiController]
[Route("api/[controller]")]
public class CandidatesController(IMediator mediator) : ControllerBase
{
	readonly IMediator mediator = mediator;

	[HttpGet]
	[Route(nameof(Get))]
	public async Task<IActionResult> Get()
	{

		var stopwatch = Stopwatch.StartNew();

		var query = new GetCandidatesQuery();
		var result = await mediator.Send(query);

		stopwatch.Stop();

		if (result.IsSuccess)
		{
			// Return the result with the elapsed time
			return Ok(new
			{
				result.Value,
				ElapsedTime = stopwatch.Elapsed
			});
		}
		if (result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}

	[HttpGet(nameof(GetFromPage))]
	public async Task<IActionResult> GetFromPage(int pageNumber)
	{
		var stopwatch = Stopwatch.StartNew();

		var query = new GetCandidatesQuery(pageNumber);
		var result = await mediator.Send(query);

		stopwatch.Stop();

		if (result.IsSuccess)
		{
			// Return the result with the elapsed time
			return Ok(new
			{
				result.Value,
				ElapsedTime = stopwatch.Elapsed
			});
		}
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
		var query = new GetCandidateByIdQuery(id);

		var result = await mediator.Send(query);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}
}
