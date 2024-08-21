namespace RabbitMq.Settings.QueueRoutes;

/// <summary>
/// Contains the queue names for events related to the Expense Service.
/// </summary>
public static class ExpenseServiceQueues
{
    /// <summary>
    /// Contains the names of the RabbitMQ queues used for user-related events in the ExpenseService.
    /// </summary>
    public static class User
    {
        public const string UserCreatedQueue = "ExpenseService-UserCreatedEventQueue";
        public const string UserUpdatedQueue = "ExpenseService-UserUpdatedEventQueue";
    }
}
