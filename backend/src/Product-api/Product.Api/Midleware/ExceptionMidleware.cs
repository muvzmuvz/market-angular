using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Products.Api.Midleware;

public class ExceptionMidleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionMidleware> _logger;

  public ExceptionMidleware(
      RequestDelegate next
      , ILogger<ExceptionMidleware> logger)
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
