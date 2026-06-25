namespace LMSPro.Api.Caching;

public static class CacheKeys
{
    public const string CoursesAll = "courses_all";

    public const string QuestionsAll = "questions_all_skip_{0}_take_{1}";

    public static string CourseById(long courseId)
    {
        return $"course_{courseId}";
    }
    public static string QuestionById(long questionId)
    {
        return $"question_{questionId}";
    }
}
