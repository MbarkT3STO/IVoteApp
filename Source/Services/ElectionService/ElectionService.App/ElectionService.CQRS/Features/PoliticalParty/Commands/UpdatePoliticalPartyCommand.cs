


namespace ElectionService.CQRS.Features.PoliticalParty.Commands;

public class UpdatePoliticalPartyCommandResultDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime EstablishmentDate { get; set; }
	public string LogoUrl { get; set; }
	public string WebsiteUrl { get; set; }
	public string CreatedBy { get; set; }
}

/// <summary>
/// Represents the command result used after updating a political party.
/// </summary>
public class UpdatePoliticalPartyCommandResult : AppCommandResult<UpdatePoliticalPartyCommandResultDto, UpdatePoliticalPartyCommandResult>
{
	public UpdatePoliticalPartyCommandResult(UpdatePoliticalPartyCommandResultDto value) : base(value)
	{
	}

	public UpdatePoliticalPartyCommandResult(Error error) : base(error)
	{
	}

	public UpdatePoliticalPartyCommandResult(bool isSuccess) : base(isSuccess)
	{
	}
}

public class UpdatePoliticalPartyCommandMappingProfile : Profile
{
	public UpdatePoliticalPartyCommandMappingProfile()
	{
		CreateMap<Entities.PoliticalParty, UpdatePoliticalPartyCommandResultDto>();
	}
}



/// <summary>
/// Represents the command used to update a political party.
/// </summary>
public class UpdatePoliticalPartyCommand : AppCommand<UpdatePoliticalPartyCommand, UpdatePoliticalPartyCommandResult>
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime EstablishmentDate { get; set; }
	public string LogoUrl { get; set; }
	public string WebsiteUrl { get; set; }

	public UpdatePoliticalPartyCommand(Guid id, string name, string description, DateTime establishmentDate, string logoUrl, string websiteUrl)
	{
		Id = id;
		Name = name;
		Description = description;
		EstablishmentDate = establishmentDate;
		LogoUrl = logoUrl;
		WebsiteUrl = websiteUrl;
	}

	public static UpdatePoliticalPartyCommand Create(Guid id, string name, string description, DateTime establishmentDate, string logoUrl, string websiteUrl)
	{
		return new UpdatePoliticalPartyCommand(id, name, description, establishmentDate, logoUrl, websiteUrl);
	}
}


public class UpdatePoliticalPartyCommandHandler : BaseAppCommandHandler<UpdatePoliticalPartyCommand, UpdatePoliticalPartyCommandResult, UpdatePoliticalPartyCommandResultDto>
{
	public UpdatePoliticalPartyCommandHandler(IMediator mediator, IMapper mapper, AppDbContext dbContext) : base(mediator, mapper, dbContext)
	{
	}

	protected override async Task<UpdatePoliticalPartyCommandResult> HandleCore(UpdatePoliticalPartyCommand command, CancellationToken cancellationToken)
	{
		var politicalParty = await _dbContext.PoliticalParties.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

		if (politicalParty == null)
		{
			return FailedResult("Political party not found.");
		}

		DetectAndApplyChanges(command, politicalParty);

		await _dbContext.SaveChangesAsync(cancellationToken);
		var resultDto = _mapper.Map<UpdatePoliticalPartyCommandResultDto>(politicalParty);

		return SucceededResult(resultDto);
	}

	/// <summary>
	/// Detects and applies changes to the political party.
	/// </summary>
	private void DetectAndApplyChanges(UpdatePoliticalPartyCommand command, Entities.PoliticalParty politicalParty)
	{
		if (politicalParty.Name != command.Name)
		{
			politicalParty.Name = command.Name;
		}

		if (politicalParty.Description != command.Description)
		{
			politicalParty.Description = command.Description;
		}

		if (politicalParty.EstablishmentDate != command.EstablishmentDate)
		{
			politicalParty.EstablishmentDate = command.EstablishmentDate;
		}

		if (politicalParty.LogoUrl != command.LogoUrl)
		{
			politicalParty.LogoUrl = command.LogoUrl;
		}

		if (politicalParty.WebsiteUrl != command.WebsiteUrl)
		{
			politicalParty.WebsiteUrl = command.WebsiteUrl;
		}
	}
}
