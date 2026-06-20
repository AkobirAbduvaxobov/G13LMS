using LMSPro.Api.Data;
using LMSPro.Api.Filters;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Configurations;

public static class FilterConfigurations
{
    public static void ConfigureFilters(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<LoggingActionFilter>();
        });

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<CustomExceptionFilter>();
        });
    }
}
