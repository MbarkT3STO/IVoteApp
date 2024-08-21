namespace RabbitMq.Settings.QueueRoutes;

public class ExpenseServiceRabbitMqEndpointOptions
{
    public class Category
    {
        public string CategoryCreatedEventQueue { get; set; }
        public string CategoryUpdatedEventQueue { get; set; }
    }
}