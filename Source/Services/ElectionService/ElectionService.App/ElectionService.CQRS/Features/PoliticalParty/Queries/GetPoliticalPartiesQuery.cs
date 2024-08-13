

using ElectionService.CQRS.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace ElectionService.CQRS.Features.PoliticalParty.Queries;

public class GetPoliticalPartiesQueryResultDto
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
/// Represents the query used to get political parties.
/// </summary>
public class GetPoliticalPartiesQueryResult : QueryResult<IEnumerable<GetPoliticalPartiesQueryResultDto>, GetPoliticalPartiesQueryResult>
{
	public GetPoliticalPartiesQueryResult(IEnumerable<GetPoliticalPartiesQueryResultDto>? value) : base(value)
	{
	}

	public GetPoliticalPartiesQueryResult(Error error) : base(error)
	{
	}
}


public class GetPoliticalPartiesQueryMapProfile : Profile
{
	public GetPoliticalPartiesQueryMapProfile()
	{
		CreateMap<Entities.PoliticalParty, GetPoliticalPartiesQueryResultDto>();
	}
}



/// <summary>
/// Represents the query used to get political parties.
/// </summary>
public class GetPoliticalPartiesQuery : AppQuery<GetPoliticalPartiesQuery, GetPoliticalPartiesQueryResult>
{
	public GetPoliticalPartiesQuery()
	{
	}

	public GetPoliticalPartiesQuery(string cacheKey) : base(cacheKey)
	{
	}

	public GetPoliticalPartiesQuery(int pageNumber, int pageSize = 10) : base(pageNumber, pageSize)
	{
	}

	public GetPoliticalPartiesQuery(string cacheKey, int pageNumber, int pageSize = 10) : base(cacheKey, pageNumber, pageSize)
	{
	}
}



public class GetPoliticalPartiesQueryHandler : BaseQueryHandler<GetPoliticalPartiesQuery, GetPoliticalPartiesQueryResult, IEnumerable<GetPoliticalPartiesQueryResultDto>>
{
	public GetPoliticalPartiesQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext, IDistributedCache distributedCache) : base(mapper, mediator, dbContext, distributedCache)
	{
	}

	protected override async Task<GetPoliticalPartiesQueryResult> HandleCore(GetPoliticalPartiesQuery query, CancellationToken cancellationToken)
	{
		var politicalPartiesQuery = _dbContext.PoliticalParties.AsQueryable();

		// Use the pagination method from the base class
		if(query.PaginationSettings.UsePagination)
		{
			politicalPartiesQuery = ApplyPagination(politicalPartiesQuery, query);
		}

		var politicalParties = await politicalPartiesQuery.ToListAsync(cancellationToken);
		var queryResultDto = _mapper.Map<IEnumerable<GetPoliticalPartiesQueryResultDto>>(politicalParties);
		var queryResult = GetPoliticalPartiesQueryResult.Succeeded(queryResultDto);

		return queryResult;
	}
}