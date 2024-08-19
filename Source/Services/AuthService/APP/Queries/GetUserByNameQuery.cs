using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.APP.Queries;

public record GetUserByNameQueryResultDTO
{
	public string Id { get; set; }
	public string UserName { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string? ImageUrl { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}

public class GetUserByNameQueryResult : AppQueryResult<GetUserByNameQueryResultDTO, GetUserByNameQueryResult>
{
	public GetUserByNameQueryResult(GetUserByNameQueryResultDTO? value) : base(value)
	{
	}

	public GetUserByNameQueryResult(Error error) : base(error)
	{
	}
}

public class GetUserByNameQueryMappingProfile : Profile
{
	public GetUserByNameQueryMappingProfile()
	{
		CreateMap<AppUser, GetUserByNameQueryResultDTO>();
	}
}


/// <summary>
/// Represents a query to get user by name (username).
/// </summary>
public class GetUserByNameQuery : AppQuery<GetUserByNameQuery, GetUserByNameQueryResult>
{
	public string UserName { get; set; }


	public GetUserByNameQuery(string userName)
	{
		UserName = userName;
	}
}

public class GetUserByNameQueryHandler : BaseAppQueryHandler<GetUserByNameQuery, GetUserByNameQueryResult>
{
	public GetUserByNameQueryHandler(IMapper mapper, IMediator mediator, AppDbContext dbContext) : base(mapper, mediator, dbContext)
	{
	}

	public override async Task<GetUserByNameQueryResult> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);

			if (user is null)
			{
				return GetUserByNameQueryResult.Failed(new Error("User not found"));
			}

			var userDTO = _mapper.Map<GetUserByNameQueryResultDTO>(user);

			return GetUserByNameQueryResult.Succeeded(userDTO);
		}
		catch (Exception e)
		{
			return GetUserByNameQueryResult.Failed(e);
		}
	}
}
