namespace RabbitMq.Settings.QueueRoutes;

/// <summary>
/// Contains the queue names for events related to the Auth Service.
/// </summary>
public static class AuthServiceQueues
{
    /// <summary>
    /// Contains constants for the user-related RabbitMQ queues used in the AuthService.
    /// </summary>
    public static class User
    {
        public const string UserCreatedQueue = "AuthService-UserCreatedEventQueue";
        public const string UserUpdatedQueue = "AuthService-UserUpdatedEventQueue";
    }
}
