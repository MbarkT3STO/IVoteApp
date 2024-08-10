using System.Reflection;
using System.Text.Json;
using ElectionService.CQRS.Common.Implementations;
using ElectionService.Database;
using Microsoft.Extensions.Caching.Distributed;

namespace ElectionService.CQRS.Common.Base;

/// <summary>
/// Base class for query handlers that implements IRequestHandler interface.
/// </summary>
/// <typeparam name="TQuery">The type of the query.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public abstract class BaseQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IRequest<TResponse>
{
	protected readonly IMapper _mapper;
	protected readonly IMediator _mediator;
	protected readonly AppDbContext _dbContext;
	protected readonly IDistributedCache _distributedCache;

	protected BaseQueryHandler(IMapper mapper, AppDbContext dbContext)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}

	protected BaseQueryHandler(IMapper mapper, AppDbContext dbContext, IDistributedCache distributedCache)
	{
		_mapper = mapper;
		_dbContext = dbContext;
		_distributedCache = distributedCache;
	}

	protected BaseQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext, IDistributedCache distributedCache)
	{
		_mapper = mapper;
		_mediator = mediator;
		_dbContext = dbContext;
		_distributedCache = distributedCache;
	}

	public virtual async Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken)
	{
		return await Task.FromResult(default(TResponse));
	}
}



/// <summary>
/// Base class for query handlers that implements IRequestHandler interface.
/// </summary>
/// <typeparam name="TQuery">The type of the query.</typeparam>
/// <typeparam name="TQueryResult">The type of the response.</typeparam>
public abstract class BaseCacheQueryHandler<TQuery, TQueryResult, TQueryResultDTO> : IRequestHandler<TQuery, TQueryResult>
where TQuery : AppQuery<TQueryResult>
where TQueryResult : QueryResult<TQueryResultDTO, TQueryResult>
{
	protected readonly IMapper _mapper;
	protected readonly IMediator _mediator;
	protected readonly AppDbContext _dbContext;
	protected readonly IDistributedCache _distributedCache;


	protected BaseCacheQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext, IDistributedCache distributedCache)
	{
		_mapper = mapper;
		_mediator = mediator;
		_dbContext = dbContext;
		_distributedCache = distributedCache;
	}

	public virtual async Task<TQueryResult> Handle(TQuery query, CancellationToken cancellationToken)
	{
		try
		{
			if (query.UseCacheIfAvailable)
			{
				var cachedResult = await TryGetFromCache(query, cancellationToken);
				if (cachedResult != null)
				{
					return cachedResult;
				}
			}

			return await HandleCore(query, cancellationToken);
		}
		catch (Exception ex)
		{
			var error = Error.FromException(ex);
			return (TQueryResult)Activator.CreateInstance(typeof(TQueryResult), error);
		}
	}

	/// <summary>
	/// Handles the core logic of the query.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	protected abstract Task<TQueryResult> HandleCore(TQuery query, CancellationToken cancellationToken);

	/// <summary>
	/// Tries to get query result from cache.
	/// </summary>
	/// <param name="query">The query to get from cache.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	private async Task<TQueryResult?> TryGetFromCache(TQuery query, CancellationToken cancellationToken)
	{
		var availableCache = await _distributedCache.GetStringAsync(query.CacheKey, cancellationToken);

		if (availableCache != null)
		{
			var queryResultValue = JsonSerializer.Deserialize<TQueryResultDTO>(availableCache);
			var queryResult = (TQueryResult)Activator.CreateInstance(typeof(TQueryResult), queryResultValue);

			return queryResult;
		}

		return default;
	}
}
