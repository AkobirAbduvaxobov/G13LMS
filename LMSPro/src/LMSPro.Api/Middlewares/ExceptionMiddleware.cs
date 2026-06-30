using LMSPro.Api.Exceptions;

namespace LMSPro.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate Next;
    private readonly ILogger<ExceptionMiddleware> Logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        Next = next;
        Logger = logger;
    }


    public async Task Invoke(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (NotFoundException ex)
        {
            Logger.LogWarning(ex, "Not found exception");
            await WriteResponse(context, 404, ex.Message);
        }
        catch (ValidationException ex)
        {
            Logger.LogWarning(ex, "Validation exception");
            await WriteResponse(context, 422, ex.Message, ex.Errors);
        }
        catch (BadRequestException ex)
        {
            Logger.LogWarning(ex, "Bad request exception");
            await WriteResponse(context, 400, ex.Message);
        }
        catch (ConflictException ex)
        {
            Logger.LogWarning(ex, "Conflict exception");
            await WriteResponse(context, 409, ex.Message);
        }
        catch (UnauthorizedException ex)
        {
            Logger.LogWarning(ex, "Unauthorized exception");
            await WriteResponse(context, 401, ex.Message);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Server error");
            await WriteResponse(context, 500, "Server error", ex.Message);
        }
    }

    private static async Task WriteResponse(HttpContext context, int statusCode, string message, object? detail = null)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            StatusCode = statusCode,
            Message = message,
            Detail = detail,
            TraceId = context.TraceIdentifier,
            Timestamp = DateTime.UtcNow
        });
    }
}

