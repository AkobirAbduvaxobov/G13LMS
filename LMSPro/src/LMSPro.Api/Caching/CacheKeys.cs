namespace LMSPro.Api.Caching;

public static class CacheKeys
{
    public const string CoursesAll = "courses_all";

    public static string QuestionsAll(int skip, int take)
    {
        return $"questions_{skip}_{take}";
    }

    public static string CourseById(long courseId)
    {
        return $"course_{courseId}";
    }

    public static string QuestionById(long questionId)
    {
        return $"course_{questionId}";
    }
}
