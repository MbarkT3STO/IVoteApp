

namespace ElectionService.CQRS.Features.PoliticalParty.Commands;

public class CreatePoliticalPartyCommandResultDto
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
/// Represents the command used to create a political party.
/// </summary>
public class CreatePoliticalPartyCommandResult : CommandResult<CreatePoliticalPartyCommandResultDto, CreatePoliticalPartyCommandResult>
{
	public CreatePoliticalPartyCommandResult(CreatePoliticalPartyCommandResultDto value) : base(value)
	{
	}

	public CreatePoliticalPartyCommandResult(Error error) : base(error)
	{
	}

	public CreatePoliticalPartyCommandResult(bool isSuccess) : base(isSuccess)
	{
	}
}

public class CreatePoliticalPartyCommandMappingProfile : Profile
{
	public CreatePoliticalPartyCommandMappingProfile()
	{
		CreateMap<CreatePoliticalPartyCommand, Entities.PoliticalParty>();
		CreateMap<Entities.PoliticalParty, CreatePoliticalPartyCommandResultDto>();
	}
}



/// <summary>
/// Represents the command used to create a political party.
/// </summary>
public class CreatePoliticalPartyCommand : IRequest<CreatePoliticalPartyCommandResult>
{
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime EstablishmentDate { get; set; }
	public string LogoUrl { get; set; }
	public string WebsiteUrl { get; set; }
	public string CreatedBy { get; set; }
}


public class CreatePoliticalPartyCommandHandler : BaseCommandHandler<CreatePoliticalPartyCommand, CreatePoliticalPartyCommandResult, CreatePoliticalPartyCommandResultDto>
{
	public CreatePoliticalPartyCommandHandler(IMapper mapper, AppDbContext dbContext) : base(mapper, dbContext)
	{
	}

	public override async Task<CreatePoliticalPartyCommandResult> Handle(CreatePoliticalPartyCommand command, CancellationToken cancellationToken)
	{
		try
		{
			var politicalParty = _mapper.Map<Entities.PoliticalParty>(command);
			politicalParty.Id = Guid.NewGuid();

			_dbContext.PoliticalParties.Add(politicalParty);
			await _dbContext.SaveChangesAsync(cancellationToken);

			var resultDto = _mapper.Map<CreatePoliticalPartyCommandResultDto>(politicalParty);

			return CreatePoliticalPartyCommandResult.Succeeded(resultDto);
		}
		catch (Exception ex)
		{
			return CreatePoliticalPartyCommandResult.Failed(ex);
		}
	}
}