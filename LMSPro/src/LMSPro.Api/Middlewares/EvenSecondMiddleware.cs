namespace LMSPro.Api.Middlewares;

public class EvenSecondMiddleware
{
    private readonly RequestDelegate Next;

    public EvenSecondMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var second = DateTime.Now.Second;

        if (second % 2 == 0)
        {
            await Next(context);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Request rejected. Current second is odd.");
        }
    }
}

public static class EvenSecondMiddlewareExtensions
{
    public static IApplicationBuilder UseEvenSecondMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<EvenSecondMiddleware>();
    }
}