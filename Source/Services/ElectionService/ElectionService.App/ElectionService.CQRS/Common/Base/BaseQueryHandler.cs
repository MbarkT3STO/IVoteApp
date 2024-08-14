using System.Text.Json;
using ElectionService.CQRS.Extensions;
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
/// <typeparam name="TQueryResultValue">The type of the query result value.</typeparam>
public abstract class BaseAppQueryHandler<TQuery, TQueryResult, TQueryResultValue> : IRequestHandler<TQuery, TQueryResult>
where TQuery : AppQuery<TQuery, TQueryResult>
where TQueryResult : QueryResult<TQueryResultValue, TQueryResult>
{
	protected readonly IMapper _mapper;
	protected readonly IMediator _mediator;
	protected readonly AppDbContext _dbContext;
	protected readonly IDistributedCache _distributedCache;


	protected BaseAppQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext, IDistributedCache distributedCache)
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
			if (query.CacheSettings.UseCacheIfAvailable)
			{
				SetCacheKey(query);
				var cachedQueryResult = await TryGetFromCache(query, cancellationToken);
				if (cachedQueryResult != null)
				{
					query.Result = cachedQueryResult;
					return cachedQueryResult;
				}
			}

			var queryResult = await HandleCore(query, cancellationToken);
			query.Result = queryResult;

			if (query.CacheSettings.UseCacheIfAvailable && queryResult.Value != null)
			{
				CacheQueryResult(queryResult, query, cancellationToken);
			}

			return queryResult;
		}
		catch (Exception ex)
		{
			var error = Error.FromException(ex);
			return (TQueryResult)Activator.CreateInstance(typeof(TQueryResult), error)!;
		}
	}


	/// <summary>
	/// Applies pagination settings to the query.
	/// </summary>
	protected IQueryable<T> ApplyPagination<T>(IQueryable<T> queryable, TQuery query) where T : class
	{
		return queryable.FromPage(query.PaginationSettings);
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
		var availableCache = await _distributedCache.GetStringAsync(query.CacheSettings.CacheKey, cancellationToken);

		if (availableCache != null && availableCache != "null")
		{
			var queryResultValue = JsonSerializer.Deserialize<TQueryResultValue>(availableCache);
			var queryResult = (TQueryResult)Activator.CreateInstance(typeof(TQueryResult), queryResultValue)!;

			return queryResult;
		}

		return default;
	}

	/// <summary>
	/// Sets or overrides the cache key (This method could be overridden in derived classes to change the cache key).
	/// </summary>
	/// <param name="query">The query to set the cache key for.</param>
	protected virtual void SetCacheKey(TQuery query)
	{
		// query.SetCacheKey(query.CacheSettings.CacheKey);
	}

	/// <summary>
	/// Caches the query result value in distributed cache.
	/// </summary>
	/// <param name="queryResult">The query result.</param>
	/// <param name="query">The query.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	protected Task CacheQueryResult(TQueryResult queryResult, TQuery query, CancellationToken cancellationToken=default)
	{
		if (query.CacheSettings.UseCacheIfAvailable)
		{
			_mediator.Send(new SetQueryCacheEntry(query.CacheSettings.CacheKey, queryResult.Value));
		}

		return Task.CompletedTask;
	}

	/// <summary>
	/// Creates a succeeded query result from the query result value.
	/// </summary>
	protected virtual TQueryResult SucceededResult(TQueryResultValue value)
	{
		return (TQueryResult)Activator.CreateInstance(typeof(TQueryResult), value)!;
	}

	/// <summary>
	/// Creates a failed query result with the specified error message.
	/// </summary>
	/// <param name="message">The error message.</param>
	protected virtual TQueryResult FailedResult(string message)
	{
		return (TQueryResult)Activator.CreateInstance(typeof(TQueryResult), new Error(message))!;
	}
}









