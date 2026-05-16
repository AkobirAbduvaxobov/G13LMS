using LMSPro.Api.Repositories;
using LMSPro.Api.Services;

namespace LMSPro.Api.Configurations;

public static class DependicyInjectionConfigurations
{
    public static void ConfigureDI(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICourseRepository, CourseRepository>();
        builder.Services.AddScoped<ICourseService, CourseService>();
    }
}
