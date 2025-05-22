using System.Net;
using System.Text.Json;
using Domain.Exceptions;

namespace web.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var statusCode = exception switch
        {
            EntityNotFoundException => (int)HttpStatusCode.NotFound,
            UserEmailAlreadyExistsException => (int)HttpStatusCode.Conflict,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var result = JsonSerializer.Serialize(
            new
            {
                message = exception.Message,
                statusCode = statusCode
            });

        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(result);
    }
}