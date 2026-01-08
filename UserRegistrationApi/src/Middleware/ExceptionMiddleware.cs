using System.Net;
using System.Text.Json;

namespace UserRegistrationApi.src.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ha ocurrido un error no controlado");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var statusCode = exception switch
        {
            KeyNotFoundException => HttpStatusCode.NotFound,      // 404
            ArgumentException => HttpStatusCode.BadRequest,       // 400
            UnauthorizedAccessException => HttpStatusCode.Unauthorized, // 401
            _ => HttpStatusCode.InternalServerError               // 500
        };

        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
            Detailed = context.Request.Host.Host.Contains("localhost") ? exception.StackTrace : null
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}