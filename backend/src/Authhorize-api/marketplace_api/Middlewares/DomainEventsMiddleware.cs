using marketplace_api.Common.Persistence;
using marketplace_api.Models;
using MediatR;
using System;

namespace marketplace_api.Middlewares;

public class DomainEventsMiddleware
{
  private readonly RequestDelegate _next;

  public DomainEventsMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(
      HttpContext httpContext,
      AuthorizeDbContext dbContext, 
      IMediator mediator)
  {
    await _next(httpContext);

    var entitiesWithEvents = dbContext.ChangeTracker
        .Entries<BaseEntity>()
        .Where(e => e.Entity.DomainEvents.Any())
        .Select(e => e.Entity)
        .ToList();

    foreach (var entity in entitiesWithEvents)
    {
      var events = entity.DomainEvents.ToList();
      entity.ClearDomainEvents();

      foreach (var domainEvent in events)
      {
        await mediator.Publish(domainEvent);
      }
    }
  }
}
