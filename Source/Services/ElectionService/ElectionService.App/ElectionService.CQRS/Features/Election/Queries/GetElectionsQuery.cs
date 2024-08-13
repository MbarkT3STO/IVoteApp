using Microsoft.Extensions.Caching.Distributed;

namespace ElectionService.CQRS.Features.Election.Queries;

public class GetElectionsQueryResultDto
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
/// Represents the result of a query that returns the elections.
/// </summary>
public class GetElectionsQueryResult : QueryResult<IEnumerable<GetElectionsQueryResultDto>, GetElectionsQueryResult>
{
	public GetElectionsQueryResult(IEnumerable<GetElectionsQueryResultDto>? value) : base(value)
	{
	}

	public GetElectionsQueryResult(Error error) : base(error)
	{
	}
}

public class GetElectionsQueryMappingProfile : Profile
{
	public GetElectionsQueryMappingProfile()
	{
		CreateMap<Entities.Election, GetElectionsQueryResultDto>();
	}
}



/// <summary>
/// Represents the query used to get the elections.
/// </summary>
public class GetElectionsQuery : AppQuery<GetElectionsQuery, GetElectionsQueryResult>
{
	public GetElectionsQuery() : base()
	{
	}

	public GetElectionsQuery(string cacheKey) : base(cacheKey)
	{
	}

	public GetElectionsQuery(int pageNumber, int pageSize = 10) : base(pageNumber, pageSize)
	{
	}

	public GetElectionsQuery(string cacheKey, int pageNumber, int pageSize = 10) : base(cacheKey, pageNumber, pageSize)
	{
	}
}


public class GetElectionsQueryHandler : BaseQueryHandler<GetElectionsQuery, GetElectionsQueryResult, IEnumerable<GetElectionsQueryResultDto>>
{
	public GetElectionsQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext, IDistributedCache distributedCache) : base(mapper, mediator, dbContext, distributedCache)
	{
	}

	protected override async Task<GetElectionsQueryResult> HandleCore(GetElectionsQuery query, CancellationToken cancellationToken)
	{
		var electionsQuery = _dbContext.Elections.AsQueryable();

		if (query.PaginationSettings.UsePagination)
		{
			electionsQuery = ApplyPagination(electionsQuery, query);
		}

		var elections = await electionsQuery.ToListAsync(cancellationToken);
		var queryResultDto = _mapper.Map<IEnumerable<GetElectionsQueryResultDto>>(elections);
		var queryResult = GetElectionsQueryResult.Succeeded(queryResultDto);

		return queryResult;
	}
}