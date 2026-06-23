using LMSPro.Api.Configurations.Settings;

namespace LMSPro.Api.Configurations
{
    public static class CacheConfigurations
    {
        public static void ConfigureCache(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();

            //var cacheSettings = builder.Configuration
            //                           .GetSection("CacheSettings")
            //                           .Get<CacheSettings>();

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
                    AbsoluteExpirationMinutes = int.Parse(courseAbsoluteExpirationMinutes),
                    SlidingExpirationMinutes = int.Parse(courseSlidingExpirationMinutes)
                }
                Questions = new QuestionCacheSettings
                {
                    AbsoluteExpirationMinutes = int.Parse(questionAbsoluteExpirationMinutes),
                    SlidingExpirationMinutes = int.Parse(questionSlidingExpirationMinutes)
                }
            };



            builder.Services.AddSingleton(cacheSettings);
        }
    }
}
