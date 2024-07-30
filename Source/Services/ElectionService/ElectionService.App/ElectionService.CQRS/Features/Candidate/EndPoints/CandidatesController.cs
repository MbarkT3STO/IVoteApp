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
}
