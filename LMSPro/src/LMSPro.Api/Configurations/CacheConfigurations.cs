using LMSPro.Api.Configurations.Settings;

namespace LMSPro.Api.Configurations
{
    public static class CacheConfigurations
    {
        public static void ConfigureCache(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();

            var courseAbsoluteExpirationMinutes =
                builder.Configuration["CacheSettings:Courses:AbsoluteExpirationMinutes"];

            var courseSlidingExpirationMinutes =
                builder.Configuration["CacheSettings:Courses:SlidingExpirationMinutes"];

            var questionAbsoluteExpirationMinutes =
                builder.Configuration["CacheSettings:Questions:AbsoluteExpirationMinutes"];

            var questionSlidingExpirationMinutes =
                builder.Configuration["CacheSettings:Questions:SlidingExpirationMinutes"];

            var cacheSettings = new CacheSettings
            {
                Courses = new CourseCacheSettings
                {
                    AbsoluteExpirationMinutes =
                        int.TryParse(courseAbsoluteExpirationMinutes, out var cAbs) ? cAbs : 10,

                    SlidingExpirationMinutes =
                        int.TryParse(courseSlidingExpirationMinutes, out var cSld) ? cSld : 5
                },

                Questions = new QuestionCacheSettings
                {
                    AbsoluteExpirationMinutes =
                        int.TryParse(questionAbsoluteExpirationMinutes, out var qAbs) ? qAbs : 10,

                    SlidingExpirationMinutes =
                        int.TryParse(questionSlidingExpirationMinutes, out var qSld) ? qSld : 5
                }
            };

            builder.Services.AddSingleton(cacheSettings);
        }
    }
}