namespace LMSPro.Api.Middlewares;

public class RequestMethodMiddleware
{
    private readonly RequestDelegate Next;
    private readonly ILogger<RequestMethodMiddleware> Logger;

    public RequestMethodMiddleware(RequestDelegate next, ILogger<RequestMethodMiddleware> logger)
    {
        Next = next;
        Logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var method = context.Request.Method;

        if (method == HttpMethods.Get ||
            method == HttpMethods.Post ||
            method == HttpMethods.Put ||
            method == HttpMethods.Delete)
        {
            Logger.LogInformation("HTTP Method: {Method} | Path: {Path}", method, context.Request.Path);
        }

        await Next(context);
    }
}