


using Microsoft.Extensions.Caching.Distributed;

namespace ElectionService.CQRS.Features.Candidate.Queries;

public class GetCandidateByIdQueryResultDto
{
	public Guid Id { get; set; }
	public Guid ElectionId { get; set; }
	public Guid PoliticalPartyId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string PhotoUrl { get; set; }
}

/// <summary>
/// Represents the result of the get candidate by id query.
/// </summary>
public class GetCandidateByIdQueryResult : QueryResult<GetCandidateByIdQueryResultDto, GetCandidateByIdQueryResult>
{
	public GetCandidateByIdQueryResult(GetCandidateByIdQueryResultDto? value) : base(value)
	{
	}

	public GetCandidateByIdQueryResult(Error error) : base(error)
	{
	}
}

public class GetCandidateByIdQueryMapProfile : Profile
{
	public GetCandidateByIdQueryMapProfile()
	{
		CreateMap<Entities.Candidate, GetCandidateByIdQueryResultDto>();
	}
}



/// <summary>
/// Represents the query used to get a candidate by id.
/// </summary>
public class GetCandidateByIdQuery : AppQuery<GetCandidateByIdQuery, GetCandidateByIdQueryResult>
{
	public Guid CandidateId { get; set; }


	public GetCandidateByIdQuery() : base()
	{
	}

	public GetCandidateByIdQuery(string cacheKey) : base(cacheKey)
	{
	}

	/// <summary>
	/// Sets the candidate id.
	/// </summary>
	public GetCandidateByIdQuery WithCandidateId(Guid candidateId)
	{
		CandidateId = candidateId;

		return this;
	}
}


public class GetCandidateByIdQueryHandler : BaseAppQueryHandler<GetCandidateByIdQuery, GetCandidateByIdQueryResult, GetCandidateByIdQueryResultDto>
{
	public GetCandidateByIdQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext, IDistributedCache distributedCache) : base(mapper, mediator, dbContext, distributedCache)
	{
	}

	protected override async Task<GetCandidateByIdQueryResult> HandleCore(GetCandidateByIdQuery query, CancellationToken cancellationToken)
	{
		var candidate = await _dbContext.Candidates.FirstOrDefaultAsync(x => x.Id == query.CandidateId, cancellationToken);

		if (candidate is null)
			return GetCandidateByIdQueryResult.Failed(new Error("Candidate not found."));

		var resultDto = _mapper.Map<GetCandidateByIdQueryResultDto>(candidate);

		return GetCandidateByIdQueryResult.Succeeded(resultDto);
	}
}