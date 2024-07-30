

namespace ElectionService.CQRS.Features.PoliticalParty.Queries;

public class GetPoliticalPartiesQueryResultDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime EstablishmentDate { get; set; }
	public string LogoUrl { get; set; }
	public string WebsiteUrl { get; set; }
	public string CreatedBy { get; set; }
}


/// <summary>
/// Represents the query used to get political parties.
/// </summary>
public class GetPoliticalPartiesQueryResult : QueryResult<IEnumerable<GetPoliticalPartiesQueryResultDto>, GetPoliticalPartiesQueryResult>
{
	public GetPoliticalPartiesQueryResult(IEnumerable<GetPoliticalPartiesQueryResultDto>? value) : base(value)
	{
	}

	public GetPoliticalPartiesQueryResult(Error error) : base(error)
	{
	}
}


public class GetPoliticalPartiesQueryMapProfile : Profile
{
	public GetPoliticalPartiesQueryMapProfile()
	{
		CreateMap<Entities.PoliticalParty, GetPoliticalPartiesQueryResultDto>();
	}
}



/// <summary>
/// Represents the query used to get political parties.
/// </summary>
public class GetPoliticalPartiesQuery : IRequest<GetPoliticalPartiesQueryResult>
{

}



public class GetPoliticalPartiesQueryHandler : BaseQueryHandler<GetPoliticalPartiesQuery, GetPoliticalPartiesQueryResult>
{
	public GetPoliticalPartiesQueryHandler(IMapper mapper, AppDbContext dbContext) : base(mapper, dbContext)
	{
	}


	public override async Task<GetPoliticalPartiesQueryResult> Handle(GetPoliticalPartiesQuery query, CancellationToken cancellationToken)
	{
		try
		{
			var politicalParties = await _dbContext.PoliticalParties.ToListAsync(cancellationToken);

			var queryResultDto = _mapper.Map<IEnumerable<GetPoliticalPartiesQueryResultDto>>(politicalParties);

			var queryResult = GetPoliticalPartiesQueryResult.Succeeded(queryResultDto);

			return queryResult;
		}
		catch (Exception ex)
		{
			return GetPoliticalPartiesQueryResult.Failed(ex);
		}
	}
}