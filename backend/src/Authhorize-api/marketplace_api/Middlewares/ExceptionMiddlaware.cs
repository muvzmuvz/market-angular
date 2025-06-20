using marketplace_api.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace marketplace_api.Middlewares;

public class ExceptionMiddlaware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionMiddlaware> _logger;

  public ExceptionMiddlaware(
      RequestDelegate next
      , ILogger<ExceptionMiddlaware> logger)
  {
    _next = next;
    _logger = logger;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (UserNotFoundException ex)
    {
      _logger.LogWarning(ex, "User not found: {Message}", ex.Message);
      context.Response.StatusCode = (int)HttpStatusCode.NotFound;
      await context.Response.WriteAsJsonAsync(new
      {
        error = ex.Message,
        type = "user_not_found",
        status = context.Response.StatusCode
      });
    }
    catch (UserAlreadyExists ex)
    {
      _logger.LogWarning(ex, "User already exists: {Message}", ex.Message);
      context.Response.StatusCode = (int)HttpStatusCode.Conflict;
      await context.Response.WriteAsJsonAsync(new
      {
        error = ex.Message,
        type = "user_already_exists",
        status = context.Response.StatusCode
      });
    }
    catch (ShopNotFoundException ex)
    {
      _logger.LogWarning(ex, "Shop not found: {Message}", ex.Message);
      context.Response.StatusCode = (int)HttpStatusCode.NotFound;
      await context.Response.WriteAsJsonAsync(new
      {
        error = ex.Message,
        type = "shop_not_found",
        status = context.Response.StatusCode
      });
    }
    catch (NotPErmissionDenied ex)
    {
      _logger.LogWarning(ex, "Permission denied: {Message}", ex.Message);
      context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
      await context.Response.WriteAsJsonAsync(new
      {
        error = ex.Message,
        type = "permission_denied",
        status = context.Response.StatusCode
      });
    }
    catch (ValidationException ex)
    {
      _logger.LogWarning(ex, "Validation error: {Message}", ex.Message);
      context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
      await context.Response.WriteAsJsonAsync(new
      {
        error = ex.Message,
        type = "validation_error",
        status = context.Response.StatusCode
      });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      await context.Response.WriteAsJsonAsync(new
      {
        error = "An unexpected error occurred. Please try again later.",
        type = "internal_server_error",
        status = context.Response.StatusCode,
        detail = _logger.IsEnabled(LogLevel.Debug) ? ex.Message : null
      });
    }
  }
}
