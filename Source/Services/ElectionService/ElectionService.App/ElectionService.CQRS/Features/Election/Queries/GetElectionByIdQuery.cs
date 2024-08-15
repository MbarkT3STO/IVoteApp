
using Microsoft.Extensions.Caching.Distributed;

namespace ElectionService.CQRS.Features.Election.Queries;

public class GetElectionByIdQueryResultDto
{
	public Guid Id { get; set; }

	public string Title { get; set; }

	public string Description { get; set; }

	public DateTime StartDateAndTime { get; set; }

	public DateTime EndDateAndTime { get; set; }

	public ElectionStatus Status { get; set; }
	public string CreatedBy { get; set; }
}

public class GetElectionByIdQueryResult : AppQueryResult<GetElectionByIdQueryResultDto, GetElectionByIdQueryResult>
{
	public GetElectionByIdQueryResult(GetElectionByIdQueryResultDto? value) : base(value)
	{
	}

	public GetElectionByIdQueryResult(Error error) : base(error)
	{
	}
}

public class GetElectionByIdQueryMappingProfile : Profile
{
	public GetElectionByIdQueryMappingProfile()
	{
		CreateMap<Entities.Election, GetElectionByIdQueryResultDto>();
	}
}


/// <summary>
/// Represents the query used to get an election by id.
/// </summary>
public class GetElectionByIdQuery : AppQuery<GetElectionByIdQuery, GetElectionByIdQueryResult>
{
	public Guid ElectionId { get; set; }

	public GetElectionByIdQuery() : base()
	{
	}

	public GetElectionByIdQuery(string cacheKey) : base(cacheKey)
	{
	}

	/// <summary>
	/// Creates a query that returns the election with the specified id.
	/// </summary>
	/// <param name="electionId">The election id.</param>
	public GetElectionByIdQuery WithElectionId(Guid electionId)
	{
		ElectionId = electionId;

		return this;
	}


	public class GetElectionByIdQueryHandler : BaseAppQueryHandler<GetElectionByIdQuery, GetElectionByIdQueryResult, GetElectionByIdQueryResultDto>
	{
		public GetElectionByIdQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext, IDistributedCache distributedCache) : base(mapper, mediator, dbContext, distributedCache)
		{
		}

		protected override async Task<GetElectionByIdQueryResult> HandleCore(GetElectionByIdQuery query, CancellationToken cancellationToken)
		{
			var election = await _dbContext.Elections.FirstOrDefaultAsync(x => x.Id == query.ElectionId, cancellationToken);

			if (election == null)
			{
				return GetElectionByIdQueryResult.Failed(new Error("Election not found"));
			}

			var queryResultValue = _mapper.Map<GetElectionByIdQueryResultDto>(election);

			return GetElectionByIdQueryResult.Succeeded(queryResultValue);
		}
	}
}
