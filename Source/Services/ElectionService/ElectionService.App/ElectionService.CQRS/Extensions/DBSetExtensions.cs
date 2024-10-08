namespace ElectionService.CQRS.Extensions;

public static class DBSetExtensions
{
	/// <summary>
	/// Get the specified page items from a <see cref="DbSet{T}"/>
	/// </summary>
	/// <returns>An <see cref="IQueryable{T}"/> of <typeparamref name="T"/></returns>
	public static IQueryable<T> FromPage<T>(this DbSet<T> dbSet, int pageNumber, int pageSize) where T : class
	{
		return dbSet
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize);
	}

	/// <summary>
	/// Get the specified page items from a <see cref="DbSet{T}"/>
	/// </summary>
	/// <param name="query">The query.</param>
	public static IQueryable<T> FromPage<T>(this DbSet<T> dbSet, IQuery query) where T : class
	{
		return dbSet
			.Skip(query.PaginationSettings.PageOffset)
			.Take(query.PaginationSettings.PageSize);
	}

	/// <summary>
	/// Get the specified page items from a <see cref="DbSet{T}"/>
	/// </summary>
	/// <param name="paginationSettings">The pagination settings for the query.</param>
	public static IQueryable<T> FromPage<T>(this DbSet<T> dbSet, IPaginationSettings paginationSettings) where T : class
	{
		return dbSet
			.Skip(paginationSettings.PageOffset)
			.Take(paginationSettings.PageSize);
	}





	/// <summary>
	/// Get the specified page items from a <see cref="IQueryable{T}"/>
	/// </summary>
	/// <returns>An <see cref="IQueryable{T}"/> of <typeparamref name="T"/></returns>
	public static IQueryable<T> FromPage<T>(this IQueryable<T> queryable, int pageNumber, int pageSize) where T : class
	{
		return queryable
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize);
	}

	/// <summary>
	/// Get the specified page items from a <see cref="IQueryable{T}"/>
	/// </summary>
	/// <param name="query">The query.</param>
	public static IQueryable<T> FromPage<T>(this IQueryable<T> queryable, IQuery query) where T : class
	{
		return queryable
			.Skip(query.PaginationSettings.PageOffset)
			.Take(query.PaginationSettings.PageSize);
	}

	/// <summary>
	/// Get the specified page items from a <see cref="IQueryable{T}"/>
	/// </summary>
	/// <param name="paginationSettings">The pagination settings for the query.</param>
	public static IQueryable<T> FromPage<T>(this IQueryable<T> queryable, IPaginationSettings paginationSettings) where T : class
	{
		return queryable
			.Skip(paginationSettings.PageOffset)
			.Take(paginationSettings.PageSize);
	}
}
