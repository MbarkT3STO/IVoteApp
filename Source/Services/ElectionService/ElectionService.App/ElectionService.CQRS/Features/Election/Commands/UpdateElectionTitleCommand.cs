


namespace ElectionService.CQRS.Features.Election.Commands;

public class UpdateElectionTitleCommandResultDto
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
/// Represents the result of a command that updates an election's title.
/// </summary>
public class UpdateElectionTitleCommandResult: AppCommandResult<UpdateElectionTitleCommandResultDto, UpdateElectionTitleCommandResult>
{
	public UpdateElectionTitleCommandResult(UpdateElectionTitleCommandResultDto value): base(value)
	{
	}

	public UpdateElectionTitleCommandResult(Error error): base(error)
	{
	}

	public UpdateElectionTitleCommandResult(bool isSuccess): base(isSuccess)
	{
	}
}


public class UpdateElectionTitleCommandMappingProfile: Profile
{
	public UpdateElectionTitleCommandMappingProfile()
	{
		CreateMap<Entities.Election, UpdateElectionTitleCommandResultDto>();
	}
}




/// <summary>
/// Represents the command used to update an election's title.
/// </summary>
public class UpdateElectionTitleCommand: AppCommand<UpdateElectionTitleCommand, UpdateElectionTitleCommandResult>
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string UpdatedBy { get; set; }


	public UpdateElectionTitleCommand(Guid id, string title, string updatedBy)
	{
		Id        = id;
		Title     = title;
		UpdatedBy = updatedBy;
	}
}


public class UpdateElectionTitleCommandHandler: BaseAppCommandHandler<UpdateElectionTitleCommand, UpdateElectionTitleCommandResult, UpdateElectionTitleCommandResultDto>
{
	public UpdateElectionTitleCommandHandler(IMediator mediator, IMapper mapper, AppDbContext dbContext): base(mediator, mapper, dbContext)
	{
	}

	protected override async Task<UpdateElectionTitleCommandResult> HandleCore(UpdateElectionTitleCommand command, CancellationToken cancellationToken)
	{
		var election = await _dbContext.Elections.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

		if (election == null)
			return FailedResult("Election not found");

		election.Title = command.Title;
		election.WriteUpdateAudit(command.UpdatedBy);

		await _dbContext.SaveChangesAsync(cancellationToken);

		var resultDto = _mapper.Map<UpdateElectionTitleCommandResultDto>(election);

		return SucceededResult(resultDto);
	}
}