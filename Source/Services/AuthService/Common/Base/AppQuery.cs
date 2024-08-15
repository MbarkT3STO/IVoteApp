
namespace AuthService.Common.Base;

/// <summary>
/// Represents a base class for query operation for the application.
/// </summary>
/// <typeparam name="TResult">The type of the query result.</typeparam>
public abstract class AppQuery<TResult>: IQuery<TResult> where TResult: class
{
	public Guid QueryId { get; }

	public TResult? Result { get; }

	public ICacheSettings CacheSettings { get; }
	public IPaginationSettings PaginationSettings { get; }


	// protected AppQuery()
	// {
	// 	QueryId = Guid.NewGuid();
	// }

	protected AppQuery(string cacheKey)
	{
		QueryId = Guid.NewGuid();

		CacheSettings      = new QueryCacheSettings(cacheKey);
		PaginationSettings = new QueryPaginationSettings();
	}

	protected AppQuery(string cacheKey, int pageNumber, int pageSize = 10)
	{
		QueryId = Guid.NewGuid();

		CacheSettings      = new QueryCacheSettings(cacheKey);
		PaginationSettings = new QueryPaginationSettings(pageNumber, pageSize);
	}

	protected AppQuery(int pageNumber, int pageSize = 10)
	{
		QueryId = Guid.NewGuid();

		CacheSettings      = new QueryCacheSettings();
		PaginationSettings = new QueryPaginationSettings(pageNumber, pageSize);
	}


	/// <summary>
	/// Creates a Cached and Paginated query.
	/// </summary>
	/// <param name="cacheKey">The cache key.</param>
	/// <param name="pageNumber">The page number.</param>
	/// <param name="pageSize">The page size.</param>
	/// <typeparam name="TQuery">The type of the query.</typeparam>
	public static TQuery CreateCachedAndPaginatedQuery<TQuery>(string cacheKey, int pageNumber, int pageSize = 10)
	{
		return (TQuery)Activator.CreateInstance(typeof(TQuery), cacheKey, pageNumber, pageSize)!;
	}

	/// <summary>
	/// Creates a Cached query (without pagination).
	/// </summary>
	/// <param name="cacheKey">The cache key.</param>
	/// <typeparam name="TQuery">The type of the query.</typeparam>
	public static TQuery CreateCachedQuery<TQuery>(string cacheKey)
	{
		return (TQuery)Activator.CreateInstance(typeof(TQuery), cacheKey)!;
	}

	/// <summary>
	/// Creates a Paginated query (without caching).
	/// </summary>
	/// <param name="pageNumber">The page number.</param>
	/// <param name="pageSize">The page size.</param>
	/// <typeparam name="TQuery">The type of the query.</typeparam>
	public static TQuery CreatePaginatedQuery<TQuery>(int pageNumber, int pageSize = 10)
	{
		return (TQuery)Activator.CreateInstance(typeof(TQuery), pageNumber, pageSize)!;
	}
}






/// <summary>
/// Represents a base class for query operation for the application.
/// </summary>
/// <typeparam name="TQuery">The type of the query.</typeparam>
/// <typeparam name="TQueryResult">The type of the query result.</typeparam>
public abstract class AppQuery<TQuery, TQueryResult>: IQuery<TQueryResult> where TQueryResult: class where TQuery: AppQuery<TQuery, TQueryResult>
{
	public Guid QueryId { get; }

	public TQueryResult? Result { get; set;}

	public ICacheSettings CacheSettings { get; private set; }
	public IPaginationSettings PaginationSettings { get; }

	protected AppQuery()
	{
		QueryId = Guid.NewGuid();

		CacheSettings      = QueryCacheSettings.CreateNotCachedQuerySettings();
		PaginationSettings = QueryPaginationSettings.CreateNotPaginatedQuerySettings();
	}

	protected AppQuery(string cacheKey)
	{
		QueryId = Guid.NewGuid();

		CacheSettings      = new QueryCacheSettings(cacheKey);
		PaginationSettings = new QueryPaginationSettings();
	}

	protected AppQuery(string cacheKey, int pageNumber, int pageSize = 10)
	{
		QueryId = Guid.NewGuid();

		CacheSettings      = new QueryCacheSettings(cacheKey);
		PaginationSettings = new QueryPaginationSettings(pageNumber, pageSize);
	}

	protected AppQuery(int pageNumber, int pageSize = 10)
	{
		QueryId = Guid.NewGuid();

		CacheSettings      = new QueryCacheSettings();
		PaginationSettings = new QueryPaginationSettings(pageNumber, pageSize);
	}

	/// <summary>
	/// Overrides the current cache settings with a new cache key.
	/// </summary>
	/// <param name="cacheKey">The new cache key.</param>
	public void SetCacheKey(string cacheKey)
	{
		CacheSettings = new QueryCacheSettings(cacheKey);
	}


	/// <summary>
	/// Creates a normal query without caching or pagination.
	/// </summary>
	/// <returns></returns>
	public static TQuery Create() => (TQuery)Activator.CreateInstance(typeof(TQuery))!;

	/// <summary>
	/// Creates a Cached and Paginated query.
	/// </summary>
	/// <param name="cacheKey">The cache key.</param>
	/// <param name="pageNumber">The page number.</param>
	/// <param name="pageSize">The page size.</param>
	public static TQuery CreateCachedAndPaginatedQuery(string cacheKey, int pageNumber, int pageSize = 10)
	{
		try
		{
			return (TQuery)Activator.CreateInstance(typeof(TQuery), cacheKey, pageNumber, pageSize)!;
		}
		catch (MissingMethodException)
		{
			throw new MissingMethodException($"The type {typeof(TQuery)} must implement this {nameof(AppQuery<TQuery, TQueryResult>)}(string cacheKey, int pageNumber, int pageSize) constructor signature to use CreateCachedAndPaginatedQuery(string cacheKey, int pageNumber, int pageSize)");
		}
	}

	/// <summary>
	/// Creates a Cached query (without pagination).
	/// </summary>
	/// <param name="cacheKey">The cache key.</param>
	public static TQuery CreateCachedQuery(string cacheKey)
	{
		try
		{
			return (TQuery)Activator.CreateInstance(typeof(TQuery), cacheKey)!;
		}
		catch (MissingMethodException)
		{
			throw new MissingMethodException($"The type {typeof(TQuery)} must implement this {nameof(AppQuery<TQuery, TQueryResult>)}(string cacheKey) constructor signature to use CreateCachedQuery(string cacheKey)");
		}
	}

	/// <summary>
	/// Creates a Paginated query (without caching).
	/// </summary>
	/// <param name="pageNumber">The page number.</param>
	/// <param name="pageSize">The page size.</param>
	/// <exception cref="MissingMethodException"></exception>
	public static TQuery CreatePaginatedQuery(int pageNumber, int pageSize = 10)
	{
		try
		{
			return (TQuery)Activator.CreateInstance(typeof(TQuery), pageNumber, pageSize)!;
		}
		catch (MissingMethodException)
		{
			throw new MissingMethodException($"The type {typeof(TQuery)} must implement this {nameof(AppQuery<TQuery, TQueryResult>)}(int pageNumber, int pageSize) constructor signature to use CreatePaginatedQuery(int pageNumber, int pageSize)");
		}
	}
}






// /// <summary>
// /// Represents a base class for query operation for the application.
// /// </summary>
// /// <typeparam name="TResult">The type of the query result.</typeparam>
// /// <typeparam name="TResultValue">The type of the query result value.</typeparam>
// public abstract class AppQuery<TQuery, TResult, TResultValue> : IQuery<TResult, TResultValue> where TResult : QueryResult<TResultValue, TResult>
// {
// 	public Guid QueryId { get; } = Guid.NewGuid();

// 	public TResult Result { get; }

// 	public ICacheSettings CacheSettings { get; }

// 	public IPaginationSettings PaginationSettings { get; }
// }
