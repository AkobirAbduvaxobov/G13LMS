using LMSPro.Api.Configurations.Settings;

namespace LMSPro.Api.Configurations
{
    public static class CacheConfigurations
    {
        public static void ConfigureCache(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();

            var courseAbsoluteExpiration =
                builder.Configuration["CacheSettings:Courses:AbsoluteExpirationMinutes"];
            var courseSlidingExpiration =
                builder.Configuration["CacheSettings:Courses:SlidingExpirationMinutes"];

            var questionAbsoluteExpiration =
                builder.Configuration["CacheSettings:Questions:AbsoluteExpirationMinutes"];
            var questionSlidingExpiration =
                builder.Configuration["CacheSettings:Questions:SlidingExpirationMinutes"];

            var cacheSettings = new CacheSettings
            {
                Courses = new CourseCacheSettings
                {
                    AbsoluteExpirationMinutes = int.Parse(courseAbsoluteExpiration),
                    SlidingExpirationMinutes = int.Parse(courseSlidingExpiration)
                },
                Questions = new QuestionCacheSettings
                {
                    AbsoluteExpirationMinutes = int.Parse(questionAbsoluteExpiration),
                    SlidingExpirationMinutes = int.Parse(questionSlidingExpiration)
                }
            };

            builder.Services.AddSingleton(cacheSettings);
        }
    }
}