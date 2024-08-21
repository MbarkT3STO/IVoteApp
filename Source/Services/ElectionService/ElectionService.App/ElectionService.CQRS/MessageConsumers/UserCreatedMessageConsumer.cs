namespace ElectionService.CQRS.MessageConsumers;

public class UserCreatedMessageConsumer: IConsumer<UserCreatedMessage>
{
	readonly AppDbContext _dbContext;

	public UserCreatedMessageConsumer(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task Consume(ConsumeContext<UserCreatedMessage> context)
	{
		var message = context.Message;

		var user = new User
		{
			Id        = message.Id,
			UserName  = message.UserName,
			FirstName = message.FirstName,
			LastName  = message.LastName,
			RoleId    = message.RoleId,
			RoleName  = message.RoleName,
		};

		_dbContext.Users.Add(user);
		await _dbContext.SaveChangesAsync();
	}
}
