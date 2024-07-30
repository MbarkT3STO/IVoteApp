using ElectionService.CQRS.Features.Election.Commands;
using ElectionService.CQRS.Features.Election.Queries;

namespace ElectionService.CQRS.Features.EndPoints;

[ApiController]
[Route("api/[controller]")]
public class ElectionsController(IMediator mediator) : ControllerBase
{
	readonly IMediator mediator = mediator;

    [HttpGet]
	[Route(nameof(Get))]
	public async Task<IActionResult> Get()
	{
		var result = await mediator.Send(new GetElectionsQuery());

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}


	[HttpPost]
	[Route(nameof(Create))]
	public async Task<IActionResult> Create(CreateElectionCommand command)
	{
		command.CreatedBy = "admin";

		var result = await mediator.Send(command);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}


	[HttpPost(nameof(UpdateStatus))]
	public async Task<IActionResult> UpdateStatus(UpdateElectionStatusCommand command)
	{
		var result = await mediator.Send(command);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}


	[HttpPost(nameof(UpdateTitle))]
	public async Task<IActionResult> UpdateTitle(UpdateElectionTitleCommand command)
	{
		var result = await mediator.Send(command);

		if(result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Error);
	}

}
