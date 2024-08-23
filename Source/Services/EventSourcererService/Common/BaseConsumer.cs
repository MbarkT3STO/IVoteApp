using MassTransit;

namespace EventSourcererService.Common;

/// <summary>
/// Base class for Message Consumers.
/// </summary>
public abstract class BaseConsumer
{
    protected readonly AppDbContext _dbContext;

    protected BaseConsumer(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}


/// <summary>
/// Represents a base consumer class for handling messages of type TMessage.
/// </summary>
/// <typeparam name="TMessage">The type of the message.</typeparam>
public abstract class BaseConsumer<TMessage> : BaseConsumer, IConsumer<TMessage> where TMessage : BaseEventMessage
{
    protected readonly IDeduplicationService _deduplicationService;

    protected BaseConsumer(AppDbContext dbContext, IDeduplicationService deduplicationService) : base(dbContext)
    {
        _deduplicationService = deduplicationService;
    }

    /// <summary>
    /// Consumes a message of type TMessage.
    /// </summary>
    /// <param name="context">The context of the consumed message.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public virtual async Task Consume(ConsumeContext<TMessage> context)
    {
        try
        {
            var hasProcessed = await _deduplicationService.HasProcessed(context.Message.EventId);

            if (hasProcessed)
            {
                return;
            }

            await _deduplicationService.ProcessMessage(() => ProcessMessage(context.Message));
        }
        catch (Exception e)
        {
            if (e is DbUpdateException || (e.InnerException != null && e.InnerException.Message.Contains("duplicate key value violates unique constraint")))
            {
                return;
            }
        }
    }

    /// <summary>
    /// Processes the specified message.
    /// </summary>
    /// <param name="message">The message to be processed.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected abstract Task ProcessMessage(TMessage message);
}