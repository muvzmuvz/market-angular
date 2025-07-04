using Microsoft.Extensions.Options;
using Products.Api.BackgroundServices.EventModels;
using Products.Api.Interfaces;
using Products.Api.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Threading.Channels;

namespace Products.Api.BackgroundServices;

public class RabbitMQConsumerService : BackgroundService, IDisposable
{
  private readonly IRedisShopService _shopCacheService;
  private readonly ILogger<RabbitMQConsumerService> _logger;
  private const string ExchangeName = "shop_events";
  public RabbitMQSettings _rabbitMQSettings { get; }

  public RabbitMQConsumerService(
      IOptions<RabbitMQSettings> settings,
      IRedisShopService shopCacheService,
      ILogger<RabbitMQConsumerService> logger)
  {
    _shopCacheService = shopCacheService;
    _logger = logger;
    _rabbitMQSettings = settings.Value;   
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
        await channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Topic, durable: true);

        var queueDeclareOk = await channel.QueueDeclareAsync(
          queue : "products_service_shop_events",
          durable: true,
          exclusive: false,
          autoDelete: false,
          arguments: null
          );
        var queueName = queueDeclareOk.QueueName;

        _logger.LogInformation("Declared queue: {QueueName}", queueName);
        await channel.QueueBindAsync(queueName, ExchangeName, "shop.created");
        _logger.LogInformation("успешное создание очереди ,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,");

        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<ShopUpdateEvent>(body);

                if (message != null)
                {
                    await _shopCacheService.CreateShop(message);
                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                    _logger.LogInformation("Shop created event processed for shop {ShopId}", message.ShopId);
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
