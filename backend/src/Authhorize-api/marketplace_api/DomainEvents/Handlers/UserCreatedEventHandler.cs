using marketplace_api.DomainEvents.DomainEventService;
using marketplace_api.DomainEvents.Events;
using MediatR;

namespace marketplace_api.DomainEvents.Handlers;

public class UserCreatedEventHandler : INotificationHandler<CreateUserEvent>
{
  private readonly IMessageBus _rabbitMQService;
  private readonly ILogger<UserCreatedEventHandler> _logger;
  private const string EXCENGE_NAME = "user_events";

  public UserCreatedEventHandler(
    IMessageBus rabbitMQService
    , ILogger<UserCreatedEventHandler> logger)
  {
    _rabbitMQService = rabbitMQService;
    _logger = logger;
  }

  public async Task Handle(CreateUserEvent notification, CancellationToken cancellationToken)
  {
    _logger.LogInformation("Handling UserCreatedEvent for user {UserId}", notification.UserId);

    await _rabbitMQService.PublishAsync(
      notification
      , EXCENGE_NAME
      , "user.created");

    _logger.LogInformation("UserCreatedEvent published for user {UserId}", notification.UserId);
  }
}
