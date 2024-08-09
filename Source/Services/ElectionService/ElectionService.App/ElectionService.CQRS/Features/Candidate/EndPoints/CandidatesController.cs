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
		var result = await mediator.Send(new GetCandidatesQuery());

		if(result.IsSuccess)
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
