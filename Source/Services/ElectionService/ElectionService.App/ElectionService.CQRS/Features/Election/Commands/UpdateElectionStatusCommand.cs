
namespace ElectionService.CQRS.Features.Election.Commands;

public class UpdateElectionStatusCommandResultDto
{
	public Guid Id { get; set; }

	public string Title { get; set; }

	public string Description { get; set; }

	public DateTime StartDateAndTime { get; set; }

	public DateTime EndDateAndTime { get; set; }

	public ElectionStatus Status { get; set; }
	public string CreatedBy { get; set; }
}

/// <summary>
/// Represents the result of a command that updates the status of an election.
/// </summary>
public class UpdateElectionStatusCommandResult : CommandResult<UpdateElectionStatusCommandResultDto, UpdateElectionStatusCommandResult>
{
	public UpdateElectionStatusCommandResult(UpdateElectionStatusCommandResultDto value) : base(value)
	{
	}

	public UpdateElectionStatusCommandResult(Error error) : base(error)
	{
	}
}


public class UpdateElectionStatusCommandMappingProfile : Profile
{
	public UpdateElectionStatusCommandMappingProfile()
	{
		CreateMap<Entities.Election, UpdateElectionStatusCommandResultDto>();
	}
}


/// <summary>
/// Represents a command that updates the status of an election.
/// </summary>
public class UpdateElectionStatusCommand : AppCommand<UpdateElectionStatusCommand, UpdateElectionStatusCommandResult>
{
	public Guid Id { get; set; }

	public ElectionStatus Status { get; set; }
}


public class UpdateElectionStatusCommandHandler : BaseCommandHandler<UpdateElectionStatusCommand, UpdateElectionStatusCommandResult, UpdateElectionStatusCommandResultDto>
{
    public UpdateElectionStatusCommandHandler(IMediator mediator, IMapper mapper, AppDbContext dbContext) : base(mediator, mapper, dbContext)
    {
    }

    protected override async Task<UpdateElectionStatusCommandResult> HandleCore(UpdateElectionStatusCommand command, CancellationToken cancellationToken)
    {
        var election = await _dbContext.Elections.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

			if (election == null)
				return UpdateElectionStatusCommandResult.Failed(new Error("Election not found"));

			election.Status = command.Status;

			await _dbContext.SaveChangesAsync(cancellationToken);

			var resultDto = _mapper.Map<UpdateElectionStatusCommandResultDto>(election);

			return SucceededResult(resultDto);
    }
}