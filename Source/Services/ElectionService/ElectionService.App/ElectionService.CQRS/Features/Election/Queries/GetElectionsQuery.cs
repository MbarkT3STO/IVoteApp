using ElectionService.CQRS.Common.Base;
using ElectionService.Database;
using Microsoft.EntityFrameworkCore;

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



public class GetElectionsQuery : IRequest<GetElectionsQueryResult>
{
	public GetElectionsQuery()
	{
	}
}


public class GetElectionsQueryHandler(IMapper mapper, AppDbContext dbContext) : BaseQueryHandler<GetElectionsQuery, GetElectionsQueryResult>(mapper, dbContext)
{

	public override async Task<GetElectionsQueryResult> Handle(GetElectionsQuery query, CancellationToken cancellationToken)
	{
		try
		{
			var elections = await dbContext.Elections.ToListAsync(cancellationToken);
			var queryResultDto = mapper.Map<IEnumerable<GetElectionsQueryResultDto>>(elections);
			var queryResult = GetElectionsQueryResult.Succeeded(queryResultDto);

			return queryResult;
		}
		catch (Exception ex)
		{
			return GetElectionsQueryResult.Failed(ex);
		}
	}

}