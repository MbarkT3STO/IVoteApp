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
}




public class UserCreatedEventHandler: INotificationHandler<UserCreatedEvent>
{
	readonly IBus _bus;
	readonly RabbitMqOptions _rabbitMqOptions;
	readonly AuthServiceRabbitMqEndpointsOptions _authServiceRabbitMqEndPointsOptions;

	public UserCreatedEventHandler(IBus bus, IOptions<RabbitMqOptions> rabbitMqOptions, IOptions<AuthServiceRabbitMqEndpointsOptions> authServiceRabbitMqEndPointsOptions)
	{
		_bus                                 = bus;
		_rabbitMqOptions                     = rabbitMqOptions.Value;
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
			RoleName  = notification.RoleName
		};

		// var queueName = _rabbitMqOptions.HostName + "/" + AuthServiceQueues.User.UserCreatedQueue;
		// var queueToPublishTo = new Uri(queueName);
		// var endPoint = await _bus.GetSendEndpoint(queueToPublishTo);
		// await endPoint.Send(message, cancellationToken);


		var electionServiceUserCreatedQueueName     = _rabbitMqOptions.HostName + "/" + ElectionServiceQueues.User.UserCreatedQueue;
		var electionServiceUserCreatedQueue         = new Uri(electionServiceUserCreatedQueueName);
		var electionServiceUserCreatedQueueEndPoint = await _bus.GetSendEndpoint(electionServiceUserCreatedQueue);


		// var authServiceEventSourcererUserEventsQueueName = _rabbitMqOptions.HostName + "/" + AuthServiceEventSourcererQueues.UserEventSourcererQueue;
		// var authServiceEventSourcererUserEventsQueue = new Uri(authServiceEventSourcererUserEventsQueueName);
		// var authServiceEventSourcererUserEventsQueueEndPoint = await _bus.GetSendEndpoint(authServiceEventSourcererUserEventsQueue);


		await electionServiceUserCreatedQueueEndPoint.Send(message, cancellationToken);
		// authServiceEventSourcererUserEventsQueueEndPoint.Send(message, cancellationToken);
	}
}