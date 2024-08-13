using System.Text.Json;
using ElectionService.CQRS.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace ElectionService.CQRS.Features.Candidate.Queries;

public class GetCandidatesQueryResultDto
{
	public Guid Id { get; set; }
	public Guid ElectionId { get; set; }
	public Guid PoliticalPartyId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string PhotoUrl { get; set; }
}

/// <summary>
/// Represents the result of the get candidates query.
/// </summary>
public class GetCandidatesQueryResult : QueryResult<IEnumerable<GetCandidatesQueryResultDto>, GetCandidatesQueryResult>
{
	public GetCandidatesQueryResult(IEnumerable<GetCandidatesQueryResultDto>? value) : base(value)
	{
	}

	public GetCandidatesQueryResult(Error error) : base(error)
	{
	}
}

public class GetCandidatesQueryMapProfile : Profile
{
	public GetCandidatesQueryMapProfile()
	{
		CreateMap<Entities.Candidate, GetCandidatesQueryResultDto>();
	}
}



/// <summary>
/// Represents the query used to get candidates.
/// </summary>
public class GetCandidatesQuery : AppQuery<GetCandidatesQuery, GetCandidatesQueryResult>
{
	public GetCandidatesQuery(string cacheKey) : base(cacheKey)
	{
	}

	public GetCandidatesQuery(int pageNumber, int pageSize = 10) : base(pageNumber, pageSize)
	{
	}

	public GetCandidatesQuery(string cacheKey, int pageNumber, int pageSize = 10) : base(cacheKey, pageNumber, pageSize)
	{
	}
}


public class GetCandidatesQueryHandler : BaseQueryHandler<GetCandidatesQuery, GetCandidatesQueryResult, IEnumerable<GetCandidatesQueryResultDto>>
{
	public GetCandidatesQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext, IDistributedCache distributedCache) : base(mapper, mediator, dbContext, distributedCache)
	{
	}

	protected override async Task<GetCandidatesQueryResult> HandleCore(GetCandidatesQuery query, CancellationToken cancellationToken)
	{
		var candidatesQuery = _dbContext.Candidates.AsQueryable();

		// Use the pagination method from the base class
		if(query.PaginationSettings.UsePagination)
		{
			candidatesQuery = ApplyPagination(candidatesQuery, query);
		}

		var candidates = await candidatesQuery.ToListAsync(cancellationToken);
		var queryResultDto = _mapper.Map<IEnumerable<GetCandidatesQueryResultDto>>(candidates);
		var queryResult = GetCandidatesQueryResult.Succeeded(queryResultDto);

		return queryResult;
	}
}