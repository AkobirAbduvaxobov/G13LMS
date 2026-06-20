
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
            Logger.LogError(ex, "Not found exception");
            context.Response.StatusCode = 404;

            await context.Response.WriteAsJsonAsync(new
            {
                Message = ex.Message
            });
        }
        catch (ValidationException ex)
        {
            Logger.LogError(ex, "Validation exception");
            context.Response.StatusCode = 422;

            await context.Response.WriteAsJsonAsync(new
            {
                Message = ex.Message,
                Detail = ex.Errors
            });
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Server error");
            context.Response.StatusCode = 500;

            await context.Response.WriteAsJsonAsync(new
            {
                Message = "Server error",
                Detail = ex.Message
            });
        }
    }
}

