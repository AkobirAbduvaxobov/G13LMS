using LMSPro.Api.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LMSPro.Api.Configurations;

public static class SerilogConfigurations
{
    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

        builder.Logging.ClearProviders(); // Remove default logging providers
        builder.Logging.AddSerilog(dispose: true); // Add Serilog as the logging provider

    }
}
