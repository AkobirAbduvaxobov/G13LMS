namespace LMSPro.Api.Middlewares;

public class UnluckyOdderMiddleware
{
    private readonly RequestDelegate Next;
    private readonly ILogger<UnluckyOdderMiddleware> Logger;

    public UnluckyOdderMiddleware(RequestDelegate next, ILogger<UnluckyOdderMiddleware> logger)
    {
        Next = next;
        Logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        if (DateTime.Now.Minute % 2 == 1)
        {
            Logger.LogWarning("Unlucky Mf! Blocking request.");
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(new
            {
                Message = "Haha unlucky! Access is denied."
            });
        }
        else
        {
            await Next(context);
        }
    }

}
