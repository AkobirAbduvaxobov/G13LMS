using LMSPro.Api.Configurations.Settings;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Configurations
{
    public static class CacheConfigurations
    {
        public static void ConfigureCache(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();

            var absoluteExpirationMinutes =
                builder.Configuration["CacheSettings:Courses:AbsoluteExpirationMinutes"];

            var slidingExpirationMinutes =
                builder.Configuration["CacheSettings:Courses:SlidingExpirationMinutes"];

            var cacheSettings = new CacheSettings
            {
                Courses = new CourseCacheSettings
                {
                    AbsoluteExpirationMinutes = int.Parse(absoluteExpirationMinutes),
                    SlidingExpirationMinutes = int.Parse(slidingExpirationMinutes)
                }
            };

            builder.Services.AddSingleton(cacheSettings);
        }
    }
}
