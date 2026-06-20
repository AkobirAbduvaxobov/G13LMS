namespace LMSPro.Api.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var start = DateTime.Now;

        var routeParams = context.Request.RouteValues.Any()
            ? string.Join(", ", context.Request.RouteValues.Select(x => $"{x.Key}: {x.Value}"))
            : "No route params";

        _logger.LogInformation(
            "Request: {Time} {Method} {Path} RouteParams: {RouteParams}",
            start,
            context.Request.Method,
            context.Request.Path,
            routeParams);

        await _next(context);

        var time = DateTime.Now - start;

        _logger.LogInformation(
            "Response: {Time} {StatusCode} {Method} {Path} DurationMs: {Duration}",
            DateTime.Now,
            context.Response.StatusCode,
            context.Request.Method,
            context.Request.Path,
            time.TotalMilliseconds);
    }
}

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestLoggingMiddleware>();
    }
}