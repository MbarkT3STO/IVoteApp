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
public class GetCandidatesQuery : AppQuery<GetCandidatesQueryResult>
{
	public GetCandidatesQuery(int pageNumber=1, int pageSize = 10) : base($"{nameof(GetCandidatesQuery)}-page-{pageNumber}-pageSize-{pageSize}", pageNumber, pageSize)
	{
	}
}


// public class GetCandidatesQueryHandler : BaseQueryHandler<GetCandidatesQuery, GetCandidatesQueryResult>
// {
// 	public GetCandidatesQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext, IDistributedCache distributedCache) : base(mapper, mediator, dbContext, distributedCache)
// 	{
// 	}

// 	public override async Task<GetCandidatesQueryResult> Handle(GetCandidatesQuery query, CancellationToken cancellationToken)
// 	{
// 		try
// 		{
// 			if (query.UseCacheIfAvailable)
// 			{
// 				var availableCache = await _distributedCache.GetStringAsync(query.CacheKey, cancellationToken);
// 				if (availableCache != null)
// 				{
// 					var queryResultDto = JsonSerializer.Deserialize<IEnumerable<GetCandidatesQueryResultDto>>(availableCache);
// 					var queryResult = GetCandidatesQueryResult.Succeeded(queryResultDto);

// 					return queryResult;
// 				}
// 				else
// 				{
// 					var candidates = await _dbContext.Candidates.ToListAsync(cancellationToken);
// 					var queryResultDto = _mapper.Map<IEnumerable<GetCandidatesQueryResultDto>>(candidates);
// 					var queryResult = GetCandidatesQueryResult.Succeeded(queryResultDto);

// 					_mediator.Send(new SetQueryCacheEntry(query.CacheKey, queryResult.Value));

// 					return queryResult;
// 				}
// 			}
// 			else
// 			{
// 				var candidates = await _dbContext.Candidates.ToListAsync(cancellationToken);
// 				var queryResultDto = _mapper.Map<IEnumerable<GetCandidatesQueryResultDto>>(candidates);
// 				var queryResult = GetCandidatesQueryResult.Succeeded(queryResultDto);

// 				return queryResult;
// 			}
// 		}
// 		catch (Exception ex)
// 		{
// 			return GetCandidatesQueryResult.Failed(ex);
// 		}
// 	}

// }

public class GetCandidatesQueryHandler : BaseQueryHandler<GetCandidatesQuery, GetCandidatesQueryResult, IEnumerable<GetCandidatesQueryResultDto>>
{
	public GetCandidatesQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext, IDistributedCache distributedCache) : base(mapper, mediator, dbContext, distributedCache)
	{
	}

	// public override async Task<GetCandidatesQueryResult> Handle(GetCandidatesQuery query, CancellationToken cancellationToken)
	// {
	// 	try
	// 	{
	// 		if (query.UseCacheIfAvailable)
	// 		{
	// 			var availableCache = await _distributedCache.GetStringAsync(query.CacheKey, cancellationToken);
	// 			if (availableCache != null)
	// 			{
	// 				var queryResultDto = JsonSerializer.Deserialize<IEnumerable<GetCandidatesQueryResultDto>>(availableCache);
	// 				var queryResult = GetCandidatesQueryResult.Succeeded(queryResultDto);

	// 				return queryResult;
	// 			}
	// 			else
	// 			{
	// 				var candidates = await _dbContext.Candidates.ToListAsync(cancellationToken);
	// 				var queryResultDto = _mapper.Map<IEnumerable<GetCandidatesQueryResultDto>>(candidates);
	// 				var queryResult = GetCandidatesQueryResult.Succeeded(queryResultDto);

	// 				_mediator.Send(new SetQueryCacheEntry(query.CacheKey, queryResult.Value));

	// 				return queryResult;
	// 			}
	// 		}
	// 		else
	// 		{
	// 			var candidates = await _dbContext.Candidates.ToListAsync(cancellationToken);
	// 			var queryResultDto = _mapper.Map<IEnumerable<GetCandidatesQueryResultDto>>(candidates);
	// 			var queryResult = GetCandidatesQueryResult.Succeeded(queryResultDto);

	// 			return queryResult;
	// 		}
	// 	}
	// 	catch (Exception ex)
	// 	{
	// 		return GetCandidatesQueryResult.Failed(ex);
	// 	}
	// }


	protected override async Task<GetCandidatesQueryResult> HandleCore(GetCandidatesQuery query, CancellationToken cancellationToken)
	{
		// var candidates = await _dbContext.Candidates.Skip(query.PageOffset).Take(query.PageSize).ToListAsync(cancellationToken);
		var candidates = await _dbContext.Candidates.FromPage(query).ToListAsync(cancellationToken);
		var queryResultDto = _mapper.Map<IEnumerable<GetCandidatesQueryResultDto>>(candidates);
		var queryResult = GetCandidatesQueryResult.Succeeded(queryResultDto);

		_mediator.Send(new SetQueryCacheEntry(query.CacheKey, queryResult.Value));

		return queryResult;
	}
}