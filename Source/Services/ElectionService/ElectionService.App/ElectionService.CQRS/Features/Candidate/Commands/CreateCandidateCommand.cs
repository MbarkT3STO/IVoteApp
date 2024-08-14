


namespace ElectionService.CQRS.Features.Candidate.Commands;

public class CreateCandidateCommandResultDto
{
	public Guid Id { get; set; }
	public Guid ElectionId { get; set; }
	public Guid PoliticalPartyId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string PhotoUrl { get; set; }
}


/// <summary>
/// Represents the result of a command that creates a candidate.
/// </summary>
public class CreateCandidateCommandResult : AppCommandResult<CreateCandidateCommandResultDto, CreateCandidateCommandResult>
{
	public CreateCandidateCommandResult(CreateCandidateCommandResultDto value) : base(value)
	{
	}

	public CreateCandidateCommandResult(Error error) : base(error)
	{
	}
}

public class CreateCandidateCommandMappingProfile : Profile
{
	public CreateCandidateCommandMappingProfile()
	{
		CreateMap<CreateCandidateCommand, Entities.Candidate>();

		CreateMap<Entities.Candidate, CreateCandidateCommandResultDto>();
	}
}




/// <summary>
/// Represents the command used to create a candidate.
/// </summary>
public class CreateCandidateCommand : AppCommand<CreateCandidateCommand, CreateCandidateCommandResult>
{
	public Guid ElectionId { get; set; }
	public Guid PoliticalPartyId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string PhotoUrl { get; set; }

	public CreateCandidateCommand()
	{

	}

	public CreateCandidateCommand(string name, string description, string photoUrl, Guid politicalPartyId, Guid electionId)
	{
		Name = name;
		Description = description;
		PhotoUrl = photoUrl;
		PoliticalPartyId = politicalPartyId;
		ElectionId = electionId;
	}
}


public class CreateCandidateCommandHandler : BaseAppCommandHandler<CreateCandidateCommand, CreateCandidateCommandResult, CreateCandidateCommandResultDto>
{
	public CreateCandidateCommandHandler(IMediator mediator, IMapper mapper, AppDbContext dbContext) : base(mediator, mapper, dbContext)
	{
	}

	protected override async Task<CreateCandidateCommandResult> HandleCore(CreateCandidateCommand command, CancellationToken cancellationToken)
	{
		var candidate = _mapper.Map<Entities.Candidate>(command);
		candidate.Id = Guid.NewGuid();

		_dbContext.Candidates.Add(candidate);
		await _dbContext.SaveChangesAsync(cancellationToken);

		var resultDto = _mapper.Map<CreateCandidateCommandResultDto>(candidate);

		return SucceededResult(resultDto);
	}
}