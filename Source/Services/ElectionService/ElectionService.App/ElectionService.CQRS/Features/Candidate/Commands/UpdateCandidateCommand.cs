



namespace ElectionService.CQRS.Features.Candidate.Commands;

public class UpdateCandidateCommandResultDto
{
	public Guid Id { get; set; }
	public Guid ElectionId { get; set; }
	public Guid PoliticalPartyId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string PhotoUrl { get; set; }
}

public class UpdateCandidateCommandResult: AppCommandResult<UpdateCandidateCommandResultDto, UpdateCandidateCommandResult>
{
	public UpdateCandidateCommandResult(UpdateCandidateCommandResultDto value): base(value)
	{
	}

	public UpdateCandidateCommandResult(Error error): base(error)
	{
	}

	public UpdateCandidateCommandResult(bool isSuccess): base(isSuccess)
	{
	}
}

public class UpdateCandidateCommandMappingProfile: Profile
{
	public UpdateCandidateCommandMappingProfile()
	{
		CreateMap<Entities.Candidate, UpdateCandidateCommandResultDto >();
	}
}


/// <summary>
/// Represents the command used to update a candidate.
/// </summary>
public class UpdateCandidateCommand: AppCommand<UpdateCandidateCommand, UpdateCandidateCommandResult>
{
	public Guid Id { get; set; }
	public Guid ElectionId { get; set; }
	public Guid PoliticalPartyId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string PhotoUrl { get; set; }
	public string UpdatedBy { get; set; }

	public UpdateCandidateCommand(Guid id, Guid electionId, Guid politicalPartyId, string name, string description, string photoUrl, string updatedBy)
	{
		Id               = id;
		Name             = name;
		Description      = description;
		PhotoUrl         = photoUrl;
		PoliticalPartyId = politicalPartyId;
		ElectionId       = electionId;
		UpdatedBy        = updatedBy;
	}

	/// <summary>
	/// Creates a new update candidate command.
	/// </summary>
	public static UpdateCandidateCommand Create(Guid id, Guid electionId, Guid politicalPartyId, string name, string description, string photoUrl, string updatedBy)
	{
		return new UpdateCandidateCommand(id, politicalPartyId, electionId, name, description, photoUrl, updatedBy);
	}
}


public class UpdateCandidateCommandHandler: BaseAppCommandHandler<UpdateCandidateCommand, UpdateCandidateCommandResult, UpdateCandidateCommandResultDto>
{
	public UpdateCandidateCommandHandler(IMediator mediator, IMapper mapper, AppDbContext dbContext): base(mediator, mapper, dbContext)
	{
	}

	protected override async Task<UpdateCandidateCommandResult> HandleCore(UpdateCandidateCommand command, CancellationToken cancellationToken)
	{
		var candidate = await _dbContext.Candidates.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

		if (candidate == null)
		{
			return FailedResult("Candidate not found.");
		}

		await DetectAndApplyChangesAsync(command, candidate, cancellationToken);

		var resultDto = _mapper.Map<UpdateCandidateCommandResultDto>(candidate);

		return SucceededResult(resultDto);
	}

	/// <summary>
	/// Detects and applies changes to the candidate.
	/// </summary>
	private async Task DetectAndApplyChangesAsync(UpdateCandidateCommand command, Entities.Candidate candidate, CancellationToken cancellationToken)
	{
		if(command.ElectionId != Guid.Empty && command.ElectionId != candidate.ElectionId)
		{
			if(!await IsElectionExists(command.ElectionId, cancellationToken))
				throw new Exception("Election not found.");

			candidate.ElectionId = command.ElectionId;
		}

		if(command.PoliticalPartyId != Guid.Empty && command.PoliticalPartyId != candidate.PoliticalPartyId)
		{
			if(!await IsPoliticalPartyExists(command.PoliticalPartyId, cancellationToken))
				throw new Exception("Political party not found.");

			candidate.PoliticalPartyId = command.PoliticalPartyId;
		}

		if (command.Name != null && command.Name != candidate.Name)
		{
			candidate.Name = command.Name;
		}

		if (command.Description != null && command.Description != candidate.Description)
		{
			candidate.Description = command.Description;
		}

		if (command.PhotoUrl != null && command.PhotoUrl != candidate.PhotoUrl)
		{
			candidate.PhotoUrl = command.PhotoUrl;
		}

		candidate.WriteUpdateAudit(command.UpdatedBy);

		_dbContext.Candidates.Update(candidate);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}


	private async Task<bool> IsElectionExists(Guid electionId, CancellationToken cancellationToken)
	{
		return await _dbContext.Elections.AnyAsync(x => x.Id == electionId, cancellationToken);
	}

	private async Task<bool> IsPoliticalPartyExists(Guid politicalPartyId, CancellationToken cancellationToken)
	{
		return await _dbContext.PoliticalParties.AnyAsync(x => x.Id == politicalPartyId, cancellationToken);
	}
}
