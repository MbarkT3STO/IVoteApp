using System.Linq.Expressions;

namespace RabbitMq.Settings.Interfaces;

/// <summary>
/// Represents a service for deduplication of messages in RabbitMQ queues.
/// </summary>
public interface IDeduplicationService
{
    /// <summary>
    /// Checks if a message with the specified ID has been processed.
    /// </summary>
    /// <param name="messageId">The ID of the message to check.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the message has been processed.</returns>
    Task<bool> HasProcessed(Guid messageId);


    /// <summary>
    /// Processes the specified message asynchronously.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <param name="message">The message to process.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ProcessMessage<TMessage>(TMessage message);



    /// <summary>
    /// Processes a message using the specified function.
    /// </summary>
    /// <param name="processMessageFunc">The function that processes the message.</param>
    Task ProcessMessage(Expression<Func<Task>> processMessageFunc);
}