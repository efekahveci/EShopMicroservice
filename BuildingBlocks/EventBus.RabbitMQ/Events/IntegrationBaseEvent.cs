namespace EventBus.RabbitMQ.Events;

public class IntegrationBaseEvent
{
    public IntegrationBaseEvent()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }

    public IntegrationBaseEvent(Guid id, DateTime time)
    {
        Id = id;
        CreatedDate = time;
    }

    public Guid Id { get; private set; }
    public DateTime CreatedDate { get; private set; }
}
