namespace RabbitMq.Settings.QueueRoutes;

public class ElectionServiceQueues
{
    /// <summary>
    /// Contains constants for the user-related RabbitMQ queues used in the ElectionService.
    /// </summary>
    public static class User
    {
        public const string UserCreatedQueue = "ElectionService-UserCreatedEventQueue";
        public const string UserUpdatedQueue = "ElectionService-UserUpdatedEventQueue";
    }
}
