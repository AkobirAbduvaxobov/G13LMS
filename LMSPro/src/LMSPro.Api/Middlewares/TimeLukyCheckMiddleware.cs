namespace LMSPro.Api.Middlewares;
   

public class TimeLukyCheckMiddleware
{
    private readonly RequestDelegate _next;

    public TimeLukyCheckMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        int hour = DateTime.Now.Hour;

        if (hour % 2 == 0)
        {
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Omadsiz");
        }
    }
}