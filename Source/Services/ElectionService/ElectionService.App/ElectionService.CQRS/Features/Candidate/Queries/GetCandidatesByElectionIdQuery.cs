

using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Caching.Distributed;

namespace ElectionService.CQRS.Features.Candidate.Queries;

public class GetCandidatesByElectionIdQueryResultDto
{
	public Guid Id { get; set; }
	public Guid ElectionId { get; set; }
	public Guid PoliticalPartyId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string PhotoUrl { get; set; }
}

public class GetCandidatesByElectionIdQueryResult : AppQueryResult<IEnumerable<GetCandidatesByElectionIdQueryResultDto>, GetCandidatesByElectionIdQueryResult>
{
	public GetCandidatesByElectionIdQueryResult(IEnumerable<GetCandidatesByElectionIdQueryResultDto>? value) : base(value)
	{
	}

	public GetCandidatesByElectionIdQueryResult(Error error) : base(error)
	{
	}
}

public class GetCandidatesByElectionIdQueryMappingProfile : Profile
{
	public GetCandidatesByElectionIdQueryMappingProfile()
	{
		CreateMap<Entities.Candidate, GetCandidatesByElectionIdQueryResultDto>();
	}
}


/// <summary>
/// Represents the query used to get candidates by election id.
/// </summary>
public class GetCandidatesByElectionIdQuery : AppQuery<GetCandidatesByElectionIdQuery, GetCandidatesByElectionIdQueryResult>
{
	public Guid ElectionId { get; set; }

	public GetCandidatesByElectionIdQuery(string cacheKey) : base(cacheKey)
	{
	}

	/// <summary>
	/// Sets the election id for the query.
	/// </summary>
	public GetCandidatesByElectionIdQuery WithElectionId(Guid electionId)
	{
		ElectionId = electionId;

		return this;
	}
}


public class GetCandidatesByElectionIdQueryHandler : BaseAppQueryHandler<GetCandidatesByElectionIdQuery, GetCandidatesByElectionIdQueryResult, IEnumerable<GetCandidatesByElectionIdQueryResultDto>>
{
	public GetCandidatesByElectionIdQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext, IDistributedCache distributedCache) : base(mapper, mediator, dbContext, distributedCache)
	{
	}

	protected override async Task<GetCandidatesByElectionIdQueryResult> HandleCore(GetCandidatesByElectionIdQuery query, CancellationToken cancellationToken)
	{
		if (!await IsElectionExists(query.ElectionId, cancellationToken))
		{
			return FailedResult($"Election with id {query.ElectionId} does not exist.");
		}

		var candidates = await _dbContext.Candidates
			.Where(x => x.ElectionId == query.ElectionId)
			.ProjectTo<GetCandidatesByElectionIdQueryResultDto>(_mapper.ConfigurationProvider)
			.ToListAsync(cancellationToken);

			return SucceededResult(candidates);
	}


	private async Task<bool> IsElectionExists(Guid electionId, CancellationToken cancellationToken)
	{
		return await _dbContext.Elections.AnyAsync(x => x.Id == electionId, cancellationToken);
	}
}
