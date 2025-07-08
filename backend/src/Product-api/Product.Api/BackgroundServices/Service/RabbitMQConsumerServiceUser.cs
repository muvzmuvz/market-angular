using Microsoft.Extensions.Options;
using Products.Api.Settings;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Products_Api.BackgroundServices.EventModels;
using System.Text.Json;
using Products_Api.Interfaces;

namespace Products_Api.BackgroundServices.Service;

public class RabbitMQConsumerServiceUser : BackgroundService
{
  private readonly ILogger<RabbitMQConsumerServiceUser> _logger;
  public RabbitMQSettings _rabbitMQSettings { get; }
  private readonly IServiceScopeFactory _serviceScopeFactory;

  private const string EXCENGENAME = "user_events";
  private const string QUEUE_NAME = "products_service_user_events";

  public RabbitMQConsumerServiceUser(
      IOptions<RabbitMQSettings> settings,
      ILogger<RabbitMQConsumerServiceUser> logger,
       IServiceScopeFactory serviceScopeFactory)
  {
    _rabbitMQSettings = settings.Value;
    _logger = logger;
    _serviceScopeFactory = serviceScopeFactory;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var factory = new ConnectionFactory
    {
      HostName = _rabbitMQSettings.HostName,
      UserName = _rabbitMQSettings.Username,
      Password = _rabbitMQSettings.Password
    };

    var connection = await factory.CreateConnectionAsync();
    var channel = await connection.CreateChannelAsync();

    _logger.LogInformation("Starting RabbitMQ consumer for shop events...");
    await channel.ExchangeDeclareAsync(EXCENGENAME, ExchangeType.Topic, durable: true);

    var queueDeclareOk = await channel.QueueDeclareAsync(
      queue: QUEUE_NAME,
      durable: true,
      exclusive: false,
      autoDelete: false,
      arguments: null
      );
    var queueName = queueDeclareOk.QueueName;

    _logger.LogInformation("Declared queue: {QueueName}", queueName);
    await channel.QueueBindAsync(queueName, EXCENGENAME, "user.created");
    _logger.LogInformation("успешное создание очереди");

    stoppingToken.ThrowIfCancellationRequested();

    var consumer = new AsyncEventingBasicConsumer(channel);
    consumer.ReceivedAsync += async (model, ea) =>
    {
      try
      {

        using (var scope = _serviceScopeFactory.CreateScope())
        {
          var cartService = scope.ServiceProvider.GetRequiredService<ICartService>();

          var body = ea.Body.ToArray();
          var message = JsonSerializer.Deserialize<CreateUserEvent>(body);

          if (message != null)
          {
            await cartService.SetUserIdToCart(message.UserId);

            await channel.BasicAckAsync(ea.DeliveryTag, false);
            _logger.LogInformation("Received message: {Message}", message.UserId);
          }
        }
      }

      catch (Exception ex)
      {
        _logger.LogError(ex, "Error processing message");
        await channel.BasicNackAsync(ea.DeliveryTag, false, true);
      }
    };

    await channel.BasicConsumeAsync("products_service_shop_events", false, consumer);
  }
}
