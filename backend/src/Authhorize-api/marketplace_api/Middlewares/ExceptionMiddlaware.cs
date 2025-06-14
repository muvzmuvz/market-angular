using marketplace_api.Exceptions;

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
      _logger.LogWarning(ex.Message);
      context.Response.StatusCode = StatusCodes.Status400BadRequest;
      await context.Response.WriteAsJsonAsync(new
      {
        error = ex.Message
      });
    }
    catch (Exception ex)
    {
      context.Response.StatusCode = StatusCodes.Status500InternalServerError;
      await context.Response.WriteAsJsonAsync(new
      {
        error = ex.Message
      });

      _logger.LogWarning(ex.Message, "Не обработаные до конца ошибки");
    }
  }
}
