using marketplace_api.DomainEvents.Events;
using marketplace_api.Exceptions;
using marketplace_api.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace marketplace_api.DomainEvents.Handlers;

public class ShopActivatedEventHandler : INotificationHandler<ShopActivatedEvent>
{
  private readonly UserManager<UserIdentity> _userManager;

  public ShopActivatedEventHandler(UserManager<UserIdentity> userManager)
  {
    _userManager = userManager;
  }

  public async Task Handle(ShopActivatedEvent notification, CancellationToken ct)
  {
    var user = await _userManager.FindByIdAsync(notification.UserId.ToString())
        ?? throw new UserNotFoundException("пользователь с данным Id не был найден");

    if (!await _userManager.IsInRoleAsync(user, Role.Seller.ToString()))
    {
      await _userManager.AddToRoleAsync(user, Role.Seller.ToString());
    }
  }
}
