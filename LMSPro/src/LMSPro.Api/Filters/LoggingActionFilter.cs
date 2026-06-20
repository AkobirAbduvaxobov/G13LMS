using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace LMSPro.Api.Filters;

public class LoggingActionFilter : IActionFilter
{
    private readonly ILogger<LoggingActionFilter> Logger;

    private Stopwatch stopwatch = null!;

    public LoggingActionFilter(
        ILogger<LoggingActionFilter> logger)
    {
        this.Logger = logger;
    }

    public void OnActionExecuting(
        ActionExecutingContext context)
    {
        stopwatch = Stopwatch.StartNew();

        var controller =
            context.RouteData.Values["controller"];

        var action =
            context.RouteData.Values["action"];

        Logger.LogInformation(
            "Started {Controller}/{Action}. Parameters: {@Parameters}",
            controller,
            action,
            context.ActionArguments);
    }

    public void OnActionExecuted(
        ActionExecutedContext context)
    {
        stopwatch.Stop();

        var controller =
            context.RouteData.Values["controller"];

        var action =
            context.RouteData.Values["action"];

        Logger.LogInformation(
            "Finished {Controller}/{Action}. Duration: {Duration} ms",
            controller,
            action,
            stopwatch.ElapsedMilliseconds);
    }

}
