namespace LMSPro.Api.Middlewares;

public class TimeGateMiddleware
{
    private readonly RequestDelegate Next;
    private readonly ILogger<TimeGateMiddleware> Logger;

    public TimeGateMiddleware(RequestDelegate next, ILogger<TimeGateMiddleware> logger)
    {
        Next = next;
        Logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var minute = DateTime.Now.Minute;

        if (minute % 2 == 0)
        {
            Logger.LogInformation("Request o'tdi: juft daqiqada keldi ({Minute})", minute);
            await Next(context);
        }
        else
        {
            Logger.LogWarning("Request rad etildi: toq daqiqada keldi ({Minute})", minute);
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            await context.Response.WriteAsJsonAsync(new
            {
                Message = "Omadsiz vaqtda keldingiz, qayta urinib ko'ring."
            });
        }
    }
}