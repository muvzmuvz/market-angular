using marketplace_api.Common.Persistence;
using marketplace_api.DomainEvents;
using marketplace_api.Models;
using MediatR;
using System;

namespace marketplace_api.Middlewares;

public class DomainEventsMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<DomainEventsMiddleware> _logger; 

  public DomainEventsMiddleware(
    RequestDelegate next
    , ILogger<DomainEventsMiddleware> logger)
  {
    _next = next;
    _logger = logger;
  }

  public async Task InvokeAsync(
      HttpContext httpContext,
      AuthorizeDbContext dbContext, 
      IMediator mediator)
  {
    await _next(httpContext);

    var entitiesWithEvents = dbContext.ChangeTracker
              .Entries()
              .Where(e => e.Entity is IHasDomainEvents)
              .Select(e => e.Entity as IHasDomainEvents)
              .Where(e => e?.DomainEvents.Any() == true)
              .ToList();
    _logger.LogInformation("получение всех событий {entitiesWithEvents}", entitiesWithEvents);

    foreach (var entity in entitiesWithEvents)
    {
      if (entity is UserIdentity userIdentity)
      {
        userIdentity.SetCreatedEventId();
      }

      var events = entity.DomainEvents.ToList();
      entity.ClearDomainEvents();

      foreach (var domainEvent in events)
      {
        _logger.LogInformation("вызов события {domainEvent}", domainEvent);
        await mediator.Publish(domainEvent);
      }
    }
  }
}
