namespace LMSPro.Api.Middlewares;

public class TimeControl
{
    private readonly RequestDelegate _next;

    public TimeControl(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {

        var start = DateTime.Now;

        if (start.Minute % 2 == 0)
        {
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("omadsiz");
        }
    }
}