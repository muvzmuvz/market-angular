using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using marketplace_api.Configuration;
using Microsoft.Extensions.Options;

namespace marketplace_api.DomainEvents.DomainEventService;

public class RabbitMQMessageBus : IMessageBus
{
  private readonly RabbitMqConfiguration _config;

  public RabbitMQMessageBus(IOptions<RabbitMqConfiguration> configuration)
  {
    _config = configuration.Value;
  }

  public async Task PublishAsync<T>(T @event, string exchangeName, string routingKey)
    where T : IDomainEvent
  {
    var factory = new ConnectionFactory
    {
      HostName = _config.HostName,
      UserName = _config.UserName,
      Password = _config.Password
    };

    using var connection =  await factory.CreateConnectionAsync();
    using var channel = await connection.CreateChannelAsync();

    await channel.ExchangeDeclareAsync(
        exchange: exchangeName,
        type: ExchangeType.Topic,
        durable: true,
        autoDelete: false);

    var json = JsonSerializer.Serialize(@event);
    var body = Encoding.UTF8.GetBytes(json);

    await channel.BasicPublishAsync(
        exchange: exchangeName,
        routingKey: routingKey,
        body: body);
  }

  public Task SubscribeAsync<T>(Func<T, Task> handler) where T : IDomainEvent
  {
    throw new NotImplementedException();
  }

  public Task UnsubscribeAsync<T>(Func<T, Task> handler) where T : IDomainEvent
  {
    throw new NotImplementedException();
  }
}
