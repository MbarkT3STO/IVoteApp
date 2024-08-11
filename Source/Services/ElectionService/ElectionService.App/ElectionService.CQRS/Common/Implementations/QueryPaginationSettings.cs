namespace ElectionService.CQRS.Common.Implementations;

/// <summary>
/// Represents the pagination settings for a query.
/// </summary>
public class QueryPaginationSettings : IPaginationSettings
{
	public bool UsePagination { get; }
	// public int PageNumber { get { return UsePagination? PageIndex +1 : -1; }  private set{}  }
	// public int PageSize { get { return UsePagination? PageSize : -1; } private set{} }
	public int PageNumber { get; }
	public int PageSize { get; }

	public int PageIndex => UsePagination ? PageNumber - 1 : -1;
	public int PageOffset => UsePagination ? PageIndex * PageSize : -1;


	public QueryPaginationSettings()
	{
		UsePagination = false;
	}

	public QueryPaginationSettings(int pageNumber, int pageSize)
	{
		UsePagination = true;
		PageNumber = pageNumber;
		PageSize = pageSize;
	}


	/// <summary>
	/// Creates a <see cref="QueryPaginationSettings"/> with pagination settings.
	/// </summary>
	/// <param name="pageNumber">The page number.</param>
	/// <param name="pageSize">The page size.</param>
	public static QueryPaginationSettings CreatePaginatedQuerySettings(int pageNumber, int pageSize = 10) => new (pageNumber, pageSize);


	/// <summary>
	/// Creates a <see cref="QueryPaginationSettings"/> with no pagination settings.
	/// </summary>
	public static QueryPaginationSettings CreateNotPaginatedQuerySettings() => new ();
}
