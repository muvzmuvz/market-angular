namespace marketplace_api.DomainEvents.DomainEventService;

public interface IMessageBus
{
  Task PublishAsync<T>(T @event, string exchangeName, string routingKey) where T : IDomainEvent;
  Task SubscribeAsync<T>(Func<T, Task> handler) where T : IDomainEvent;
  Task UnsubscribeAsync<T>(Func<T, Task> handler) where T : IDomainEvent;
}
