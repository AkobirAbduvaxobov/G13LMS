using LMSPro.Api.Configurations.Settings;

namespace LMSPro.Api.Configurations;

public static class CacheConfigurations
{
    public static void ConfigureCache(this WebApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();

       
        var absoluteExpirationMinutes =
            builder.Configuration["CacheSettings:Courses:AbsoluteExpirationMinutes"];

        var slidingExpirationMinutes =
            builder.Configuration["CacheSettings:Courses:SlidingExpirationMinutes"];
                    
        
        var questionAbsoluteExpirationMinutes =
            builder.Configuration["CacheSettings:Questions:AbsoluteExpirationMinutes"];

        var questionSlidingExpirationMinutes =
            builder.Configuration["CacheSettings:Questions:SlidingExpirationMinutes"];

        var cacheSettings = new CacheSettings
        {
            Courses = new CourseCacheSettings
            {
                AbsoluteExpirationMinutes = int.Parse(absoluteExpirationMinutes),
                SlidingExpirationMinutes = int.Parse(slidingExpirationMinutes)
            },


            Questions = new QuestionCacheSettings
            {
                AbsoluteExpirationMinutes = int.Parse(questionAbsoluteExpirationMinutes),
                SlidingExpirationMinutes = int.Parse(questionSlidingExpirationMinutes)
            }
        };
    }
}
