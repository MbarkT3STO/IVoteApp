using MassTransit;

namespace EventSourcererService.MessageConsumers.AuthService;

public class AuthServiceUserCreatedMessageConsumer: BaseConsumer, IConsumer<UserCreatedMessage>
{
    public AuthServiceUserCreatedMessageConsumer(AppDbContext dbContext): base(dbContext)
    {
    }

    public async Task Consume(ConsumeContext<UserCreatedMessage> context)
    {
        var message   = context.Message;
        var eventData = new AuthServiceUserEventJsonData(
                message.Id,
                message.FirstName,
                message.LastName,
                message.UserName,
                message.Email,
                message.RoleId,
                message.CreatedAt
            );

        var userEvent = new AuthServiceUserEvent(message.EventId, "Create", DateTime.UtcNow, message.UserId, eventData);


        await _dbContext.AuthService_UserEvents.AddAsync(userEvent);
        await _dbContext.SaveChangesAsync();
    }
}
