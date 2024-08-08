

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
public class CreateCandidateCommandResult : CommandResult<CreateCandidateCommandResultDto, CreateCandidateCommandResult>
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
public class CreateCandidateCommand : AppCommand<CreateCandidateCommandResult>
{
	public Guid ElectionId { get; set; }
	public Guid PoliticalPartyId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string PhotoUrl { get; set; }
}


public class CreateCandidateCommandHandler : BaseCommandHandler<CreateCandidateCommand, CreateCandidateCommandResult, CreateCandidateCommandResultDto>
{
	public CreateCandidateCommandHandler(IMapper mapper, AppDbContext dbContext) : base(mapper, dbContext)
	{
	}

	public override async Task<CreateCandidateCommandResult> Handle(CreateCandidateCommand command, CancellationToken cancellationToken)
	{
		try
		{
			var candidate = _mapper.Map<Entities.Candidate>(command);
			candidate.Id = Guid.NewGuid();

			_dbContext.Candidates.Add(candidate);
			await _dbContext.SaveChangesAsync(cancellationToken);

			var resultDto = _mapper.Map<CreateCandidateCommandResultDto>(candidate);

			return CreateCandidateCommandResult.Succeeded(resultDto);
		}
		catch (Exception ex)
		{
			return CreateCandidateCommandResult.Failed(ex);
		}
	}
}