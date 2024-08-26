namespace ElectionService.CQRS.Features.Election.Commands;

public class CreateElectionCommandResultDto
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
/// Represents the result of a command that creates an election.
/// </summary>
public class CreateElectionCommandResult : AppCommandResult<CreateElectionCommandResultDto, CreateElectionCommandResult>
{
	public CreateElectionCommandResult(CreateElectionCommandResultDto value) : base(value)
	{
	}

	public CreateElectionCommandResult(Error error) : base(error)
	{
	}
}


public class CreateElectionCommandMappingProfile : Profile
{
	public CreateElectionCommandMappingProfile()
	{
		CreateMap<CreateElectionCommand, Entities.Election>();
		CreateMap<Entities.Election, CreateElectionCommandResultDto>();
	}
}




public class CreateElectionCommand : AppCommand<CreateElectionCommand, CreateElectionCommandResult>
{
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime StartDateAndTime { get; set; }
	public DateTime EndDateAndTime { get; set; }
	public ElectionStatus Status { get; set; }
	public string CreatedBy { get; set; }

	public CreateElectionCommand(string title, string description, DateTime startDateAndTime, DateTime endDateAndTime, ElectionStatus status, string createdBy)
	{
		Title             = title;
		Description       = description;
		StartDateAndTime  = startDateAndTime;
		EndDateAndTime    = endDateAndTime;
		Status            = status;
		CreatedBy         = createdBy;
	}
}


public class CreateElectionCommandHandler : BaseAppCommandHandler<CreateElectionCommand, CreateElectionCommandResult, CreateElectionCommandResultDto>
{
	public CreateElectionCommandHandler(IMediator mediator, IMapper mapper, AppDbContext dbContext) : base(mediator, mapper, dbContext)
	{
	}

	protected override async Task<CreateElectionCommandResult> HandleCore(CreateElectionCommand command, CancellationToken cancellationToken)
	{
		var election = _mapper.Map<Entities.Election>(command);
		election.Id = Guid.NewGuid();

		election.WriteCreateAudit(command.CreatedBy);

		_dbContext.Elections.Add(election);
		await _dbContext.SaveChangesAsync(cancellationToken);

		var resultDto = _mapper.Map<CreateElectionCommandResultDto>(election);

		return SucceededResult(resultDto);
	}
}