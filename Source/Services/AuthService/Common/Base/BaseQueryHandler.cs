using System.Text.Json;
using AuthService.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace AuthService.Common.Base;

/// <summary>
/// Base class for query handlers that implements IRequestHandler interface.
/// </summary>
/// <typeparam name="TQuery">The type of the query.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public abstract class BaseQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IRequest<TResponse>
{
	protected readonly IMapper _mapper;
	protected readonly IMediator _mediator;
	protected readonly AppDbContext _dbContext;
	protected readonly IDistributedCache _distributedCache;

	protected BaseQueryHandler(IMapper mapper, AppDbContext dbContext)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}

	protected BaseQueryHandler(IMapper mapper, AppDbContext dbContext, IDistributedCache distributedCache)
	{
		_mapper = mapper;
		_dbContext = dbContext;
		_distributedCache = distributedCache;
	}

	protected BaseQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext, IDistributedCache distributedCache)
	{
		_mapper = mapper;
		_mediator = mediator;
		_dbContext = dbContext;
		_distributedCache = distributedCache;
	}

	public virtual async Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken)
	{
		return await Task.FromResult(default(TResponse));
	}
}










