
using Microsoft.Extensions.Options;

namespace AuthService.APP.Queries;

public record GetUserByIdQueryResultDTO
{
	public string Id { get; set; }
	public string UserName { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string? ImageUrl { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}

public class GetUserByIdQueryResult : AppQueryResult<GetUserByIdQueryResultDTO, GetUserByIdQueryResult>
{
	public GetUserByIdQueryResult(GetUserByIdQueryResultDTO? value) : base(value)
	{
	}

	public GetUserByIdQueryResult(Error error) : base(error)
	{
	}
}

public class GetUserByIdQueryMappingProfile : Profile
{
	public GetUserByIdQueryMappingProfile()
	{
		CreateMap<AppUser, GetUserByIdQueryResultDTO>();
	}
}


/// <summary>
/// Represents a query to get user by id.
/// </summary>
public class GetUserByIdQuery : AppQuery<GetUserByIdQuery, GetUserByIdQueryResult>
{
	public string Id { get; set; }


	public GetUserByIdQuery(string id)
	{
		Id = id;
	}
}

public class GetUserByIdQueryHandler : BaseAppQueryHandler<GetUserByIdQuery, GetUserByIdQueryResult>
{
	public GetUserByIdQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext) : base(mapper, mediator, dbContext)
	{
	}

	public override async Task<GetUserByIdQueryResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

			if (user is null)
			{
				return GetUserByIdQueryResult.Failed(new Error("User not found"));
			}

			var userDTO = _mapper.Map<GetUserByIdQueryResultDTO>(user);

			return GetUserByIdQueryResult.Succeeded(userDTO);
		}
		catch (Exception e)
		{
			return GetUserByIdQueryResult.Failed(e);
		}
	}
}
