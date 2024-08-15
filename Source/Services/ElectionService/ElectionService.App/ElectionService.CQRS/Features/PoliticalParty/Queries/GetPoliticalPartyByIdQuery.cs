

using Microsoft.Extensions.Caching.Distributed;

namespace ElectionService.CQRS.Features.PoliticalParty.Queries;

public class GetPoliticalPartyByIdQueryResultDto
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
/// Represents the result of the get political party by id query.
/// </summary>
public class GetPoliticalPartyByIdQueryResult : AppQueryResult<GetPoliticalPartyByIdQueryResultDto, GetPoliticalPartyByIdQueryResult>
{
	public GetPoliticalPartyByIdQueryResult(GetPoliticalPartyByIdQueryResultDto? value) : base(value)
	{
	}

	public GetPoliticalPartyByIdQueryResult(Error error) : base(error)
	{
	}
}

public class GetPoliticalPartyByIdQueryMapProfile : Profile
{
	public GetPoliticalPartyByIdQueryMapProfile()
	{
		CreateMap<Entities.PoliticalParty, GetPoliticalPartyByIdQueryResultDto>();
	}
}



/// <summary>
/// Represents the query used to get a political party by id.
/// </summary>
public class GetPoliticalPartyByIdQuery : AppQuery<GetPoliticalPartyByIdQuery, GetPoliticalPartyByIdQueryResult>
{
	public Guid PoliticalPartyId { get; set; }

	public GetPoliticalPartyByIdQuery() : base()
	{

	}

	public GetPoliticalPartyByIdQuery(string cacheKey) : base(cacheKey)
	{

	}


	/// <summary>
	/// Sets the id of the political party to get.
	/// </summary>
	/// <param name="politicalPartyId">The id of the political party.</param>
	public GetPoliticalPartyByIdQuery WithPoliticalPartyId(Guid politicalPartyId)
	{
		PoliticalPartyId = politicalPartyId;

		return this;
	}
}


public class GetPoliticalPartyByIdQueryHandler : BaseAppQueryHandler<GetPoliticalPartyByIdQuery, GetPoliticalPartyByIdQueryResult, GetPoliticalPartyByIdQueryResultDto>
{
	public GetPoliticalPartyByIdQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext, IDistributedCache distributedCache) : base(mapper, mediator, dbContext, distributedCache)
	{
	}

	protected override async Task<GetPoliticalPartyByIdQueryResult> HandleCore(GetPoliticalPartyByIdQuery query, CancellationToken cancellationToken)
	{
		var politicalParty = await _dbContext.PoliticalParties.FirstOrDefaultAsync(x => x.Id == query.PoliticalPartyId, cancellationToken: cancellationToken);

		if (politicalParty is null)
		{
			return GetPoliticalPartyByIdQueryResult.Failed(new Error("Political party not found."));
		}

		var queryResultDto = _mapper.Map<GetPoliticalPartyByIdQueryResultDto>(politicalParty);

		return GetPoliticalPartyByIdQueryResult.Succeeded(queryResultDto);
	}
}
