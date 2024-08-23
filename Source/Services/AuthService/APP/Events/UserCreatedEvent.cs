using RabbitMq.Settings.Extensions;
using RabbitMq.Settings.QueueRoutes.EventSourcerer;

namespace AuthService.APP.Events;

/// <summary>
/// Represents an event that is raised when a new user is created.
/// </summary>
public class UserCreatedEvent: INotification
{
	public string UserId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string UserName { get; set; }
	public string RoleId { get; set; }
	public string RoleName { get; set; }
	public string Email { get; set; }
	public DateTime CreatedAt { get; set; }
}




public class UserCreatedEventHandler: INotificationHandler<UserCreatedEvent>
{
	readonly IBus _bus;
	readonly RabbitMqSettings _rabbitMqSettings;
	readonly AuthServiceRabbitMqEndpointsOptions _authServiceRabbitMqEndPointsOptions;

	public UserCreatedEventHandler(IBus bus, IOptions<RabbitMqSettings> rabbitMqOptions, IOptions<AuthServiceRabbitMqEndpointsOptions> authServiceRabbitMqEndPointsOptions)
	{
		_bus                                 = bus;
		_rabbitMqSettings                     = rabbitMqOptions.Value;
		_authServiceRabbitMqEndPointsOptions = authServiceRabbitMqEndPointsOptions.Value;
	}

	public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
	{
		var message = new UserCreatedMessage
		{
			EventId   = Guid.NewGuid(),
			EventName = "UserCreatedEvent",

			Id        = notification.UserId,
			FirstName = notification.FirstName,
			LastName  = notification.LastName,
			UserName  = notification.UserName,
			RoleId    = notification.RoleId,
			RoleName  = notification.RoleName,

			UserId    = notification.UserId,
			Email    = notification.Email,
			CreatedAt = notification.CreatedAt
		};

		// var queueName = _rabbitMqOptions.HostName + "/" + AuthServiceQueues.User.UserCreatedQueue;
		// var queueToPublishTo = new Uri(queueName);
		// var endPoint = await _bus.GetSendEndpoint(queueToPublishTo);
		// await endPoint.Send(message, cancellationToken);


		// var electionServiceUserCreatedQueueName     = _rabbitMqSettings.HostName + "/" + ElectionServiceQueues.User.UserCreatedQueue;
		// var electionServiceUserCreatedQueue         = new Uri(electionServiceUserCreatedQueueName);
		// var electionServiceUserCreatedQueueEndPoint = await _bus.GetSendEndpoint(electionServiceUserCreatedQueue);
		var electionServiceUserCreatedQueueEndPoint = _bus.GetEndpoint( _rabbitMqSettings, ElectionServiceQueues.User.UserCreatedQueue);


		// var authServiceEventSourcererUserCreatedEventsQueueName = _rabbitMqSettings.HostName + "/" + AuthServiceEventSourcererQueues.UserCreatedEventQueue;
		// var authServiceEventSourcererUserEventsQueue = new Uri(authServiceEventSourcererUserCreatedEventsQueueName);
		// var authServiceEventSourcererUserEventsQueueEndPoint = await _bus.GetSendEndpoint(authServiceEventSourcererUserEventsQueue);
		var authServiceEventSourcererUserEventsQueueEndPoint = _bus.GetEndpoint( _rabbitMqSettings, AuthServiceEventSourcererQueues.UserCreatedEventQueue);


		await electionServiceUserCreatedQueueEndPoint.Send(message, cancellationToken);
		await authServiceEventSourcererUserEventsQueueEndPoint.Send(message, cancellationToken);
	}
}