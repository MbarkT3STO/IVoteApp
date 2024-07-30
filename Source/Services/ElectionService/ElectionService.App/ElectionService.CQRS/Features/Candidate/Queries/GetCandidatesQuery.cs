

namespace ElectionService.CQRS.Features.Candidate.Queries;

public class GetCandidatesQueryResultDto
{
	public Guid Id { get; set; }
	public Guid ElectionId { get; set; }
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
public class GetCandidatesQuery : IRequest<GetCandidatesQueryResult>
{

}


public class GetCandidatesQueryHandler : BaseQueryHandler<GetCandidatesQuery, GetCandidatesQueryResult>
{
	public GetCandidatesQueryHandler(IMapper mapper, AppDbContext dbContext) : base(mapper, dbContext)
	{
	}

	public override async Task<GetCandidatesQueryResult> Handle(GetCandidatesQuery query, CancellationToken cancellationToken)
	{
		try
		{
			var candidates = await _dbContext.Candidates.ToListAsync(cancellationToken);
			var queryResultDto = _mapper.Map<IEnumerable<GetCandidatesQueryResultDto>>(candidates);
			var queryResult = GetCandidatesQueryResult.Succeeded(queryResultDto);

			return queryResult;
		}
		catch (Exception ex)
		{
			return GetCandidatesQueryResult.Failed(ex);
		}
	}
}