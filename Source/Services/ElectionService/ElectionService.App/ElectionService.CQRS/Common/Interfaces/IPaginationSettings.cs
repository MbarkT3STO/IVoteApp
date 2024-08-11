namespace ElectionService.CQRS.Common.Interfaces;

/// <summary>
/// Represents the pagination settings.
/// </summary>
public interface IPaginationSettings
{
	/// <summary>
	/// Gets a value indicating whether pagination should be used.
	/// </summary>
	public bool UsePagination { get; }

	/// <summary>
	/// Gets the page number.
	/// </summary>
	public int PageNumber { get; }

	/// <summary>
	/// Gets the page size (number of items per page).
	/// </summary>
	public int PageSize { get; }

	/// <summary>
	/// Gets the index of the page (starting from 0).
	/// </summary>
	public int PageIndex => PageNumber - 1;

	/// <summary>
	/// Gets the offset of the page (means the number of items to skip).
	/// </summary>
	public int PageOffset => PageIndex * PageSize;
}
