using LMSPro.Api.Configurations.Settings;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Configurations
{
    public static class CacheConfigurations
    {
        public static void ConfigureCache(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();

            var coursesAbsoluteExpiration =
                builder.Configuration["CacheSettings:Courses:AbsoluteExpirationMinutes"];

            var coursesSlidingExpiration =
                builder.Configuration["CacheSettings:Courses:SlidingExpirationMinutes"];

            var questionsAbsoluteExpiration =
                builder.Configuration["CacheSettings:Questions:AbsoluteExpirationMinutes"];

            var questionsSlidingExpiration =
                builder.Configuration["CacheSettings:Questions:SlidingExpirationMinutes"];

            var cacheSettings = new CacheSettings
            {
                Courses = new CourseCacheSettings
                {
                    AbsoluteExpirationMinutes = int.Parse(coursesAbsoluteExpiration),
                    SlidingExpirationMinutes = int.Parse(coursesSlidingExpiration)
                },
                Questions = new QuestionCacheSettings
                {
                    AbsoluteExpirationMinutes = int.Parse(questionsAbsoluteExpiration),
                    SlidingExpirationMinutes = int.Parse(questionsSlidingExpiration)
                }
            };

            builder.Services.AddSingleton(cacheSettings);
        }
    }
}