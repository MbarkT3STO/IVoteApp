

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
public class UpdateElectionTitleCommandResult : CommandResult<UpdateElectionTitleCommandResultDto, UpdateElectionTitleCommandResult>
{
	public UpdateElectionTitleCommandResult(UpdateElectionTitleCommandResultDto value) : base(value)
	{
	}

	public UpdateElectionTitleCommandResult(Error error) : base(error)
	{
	}

	public UpdateElectionTitleCommandResult(bool isSuccess) : base(isSuccess)
	{
	}
}


public class UpdateElectionTitleCommandMappingProfile : Profile
{
	public UpdateElectionTitleCommandMappingProfile()
	{
		CreateMap<Entities.Election, UpdateElectionTitleCommandResultDto>();
	}
}




/// <summary>
/// Represents the command used to update an election's title.
/// </summary>
public class UpdateElectionTitleCommand : IRequest<UpdateElectionTitleCommandResult>
{
	public Guid Id { get; set; }


	/// <summary>
	/// The new title of the election
	/// </summary>
	public string Title { get; set; }
}


public class UpdateElectionTitleCommandHandler : BaseCommandHandler<UpdateElectionTitleCommand, UpdateElectionTitleCommandResult, UpdateElectionTitleCommandResultDto>
{
	public UpdateElectionTitleCommandHandler(IMapper mapper, AppDbContext dbContext) : base(mapper, dbContext)
	{
	}



	public override async Task<UpdateElectionTitleCommandResult> Handle(UpdateElectionTitleCommand command, CancellationToken cancellationToken)
	{
		try
		{
			var election = await _dbContext.Elections
				.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

			if (election == null)
				return UpdateElectionTitleCommandResult.Failed(new Error("Election not found"));

			election.Title = command.Title;

			await _dbContext.SaveChangesAsync(cancellationToken);

			var resultDto = _mapper.Map<UpdateElectionTitleCommandResultDto>(election);

			return UpdateElectionTitleCommandResult.Succeeded(resultDto);
		}
		catch (Exception ex)
		{
			return UpdateElectionTitleCommandResult.Failed(ex);
		}
	}
}