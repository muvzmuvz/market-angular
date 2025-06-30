using marketplace_api.DomainEvents.DomainEventService;
using marketplace_api.DomainEvents.Events;
using marketplace_api.Exceptions;
using marketplace_api.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RabbitMQ.Client;

namespace marketplace_api.DomainEvents.Handlers;

public class ShopActivatedEventHandler : INotificationHandler<ShopActivatedEvent>
{
  private readonly UserManager<UserIdentity> _userManager;
  private readonly IMessageBus _rabbitMQService;
  private readonly ILogger<ShopActivatedEventHandler> _logger;

  public ShopActivatedEventHandler(
    UserManager<UserIdentity> userManager
    , IMessageBus messageBus
    , ILogger<ShopActivatedEventHandler> logger)
  {
    _userManager = userManager;
    _rabbitMQService = messageBus;
    _logger = logger;
  }

  public async Task Handle(ShopActivatedEvent notification, CancellationToken ct)
  {
    var user = await _userManager.FindByIdAsync(notification.UserId.ToString())
        ?? throw new UserNotFoundException("пользователь с данным Id не был найден");

    if (!await _userManager.IsInRoleAsync(user, Role.Seller.ToString()))
    {
      await _userManager.AddToRoleAsync(user, Role.Seller.ToString());
    }

    try
    {
      await _rabbitMQService.PublishAsync(
          notification,
          "shop_events",
          "shop.created");

      _logger.LogInformation("Shop created event published for shop {ShopId}", notification.ShopId);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Failed to publish shop created event for shop {ShopId}", notification.ShopId);
      throw;
    }
  }
}
