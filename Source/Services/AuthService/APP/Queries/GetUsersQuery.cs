
namespace AuthService.APP.Queries;

public class GetUsersQueryResultDTO
{
	public string Id { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
}

public class GetUsersQueryResult : AppQueryResult<IEnumerable<GetUsersQueryResultDTO>, GetUsersQueryResult>
{
	public GetUsersQueryResult(IEnumerable<GetUsersQueryResultDTO>? value) : base(value)
	{
	}

	public GetUsersQueryResult(Error error) : base(error)
	{
	}
}

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<AppUser, GetUsersQueryResultDTO>();
	}
}


public class GetUsersQuery : AppQuery<GetUsersQuery, GetUsersQueryResult>
{

}


public class GetUsersQueryHandler : BaseAppQueryHandler<GetUsersQuery, GetUsersQueryResult>
{
    public GetUsersQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext) : base(mapper, mediator, dbContext)
    {
    }

    public override async Task<GetUsersQueryResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var users = await _dbContext.Users.ToListAsync(cancellationToken);
			var usersDTO = _mapper.Map<IEnumerable<GetUsersQueryResultDTO>>(users);

			return GetUsersQueryResult.Succeeded(usersDTO);
		}
		catch (Exception e)
		{
			return  GetUsersQueryResult.Failed(Error.FromException(e));
		}
	}
}

