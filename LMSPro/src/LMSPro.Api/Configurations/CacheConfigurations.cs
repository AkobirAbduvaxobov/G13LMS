using LMSPro.Api.Configurations.Settings;

namespace LMSPro.Api.Configurations
{
    public static class CacheConfigurations
    {
        public static void ConfigureCache(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();
            //builder.Services.AddOutputCache();

            builder.Services.AddOutputCache(options =>
            {
                options.AddPolicy("CoursesCache", policy =>
                    policy
                        .Expire(TimeSpan.FromMinutes(5))
                        .Tag("courses"));

                options.AddPolicy("QuestionsCache", policy =>
                    policy
                        .Expire(TimeSpan.FromMinutes(5))
                        .Tag("questions")
                        .SetVaryByQuery("pageNumber", "pageSize"));
            });

            var absoluteExpirationMinutes =
                builder.Configuration["CacheSettings:Courses:AbsoluteExpirationMinutes"];

            var slidingExpirationMinutes =
                builder.Configuration["CacheSettings:Courses:SlidingExpirationMinutes"];

            var questionAbsolute =
                builder.Configuration["CacheSettings:Questions:AbsoluteExpirationMinutes"];

            var questionSliding =
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
                    AbsoluteExpirationMinutes = int.Parse(questionAbsolute),
                    SlidingExpirationMinutes = int.Parse(questionSliding)
                }
            };

            builder.Services.AddSingleton(cacheSettings);
        }
    }
}