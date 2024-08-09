


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
public class GetCandidateByIdQuery : AppQuery<GetCandidateByIdQueryResult>
{
	public Guid CandidateId { get; set; }

	public GetCandidateByIdQuery(Guid candidateId) : base(cacheKey: $"{nameof(GetCandidateByIdQuery)}-{candidateId}")
	{
		CandidateId = candidateId;
	}
}


public class GetCandidateByIdQueryHandler : BaseQueryHandler<GetCandidateByIdQuery, GetCandidateByIdQueryResult>
{
	public GetCandidateByIdQueryHandler(IMapper mapper, AppDbContext dbContext) : base(mapper, dbContext)
	{
	}

	public override async Task<GetCandidateByIdQueryResult> Handle(GetCandidateByIdQuery query, CancellationToken cancellationToken)
	{
		try
		{
			var candidate = await _dbContext.Candidates.FirstOrDefaultAsync(x => x.Id == query.CandidateId, cancellationToken);

			if (candidate is null)
				return GetCandidateByIdQueryResult.Failed(new Error("Candidate not found."));

			var resultDto = _mapper.Map<GetCandidateByIdQueryResultDto>(candidate);

			return GetCandidateByIdQueryResult.Succeeded(resultDto);
		}
		catch (Exception e)
		{
			return GetCandidateByIdQueryResult.Failed(new Error(e.Message));
		}
	}
}