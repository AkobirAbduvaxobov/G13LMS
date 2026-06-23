namespace LMSPro.Api.Caching;

public static class CacheKeys
{
    public const string CoursesAll = "courses_all";


    public static string CourseById(long courseId)
    {
        return $"course_{courseId}";
    }
}
