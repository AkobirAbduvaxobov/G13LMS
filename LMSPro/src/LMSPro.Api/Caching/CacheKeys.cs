using LMSPro.Api.Entities;

namespace LMSPro.Api.Caching;

public static class CacheKeys
{
    public const string CoursesAll = "courses_all";
    public const string StudentsAll = "students_all";
    public const string QuestionsAll = "questions_all_skip_{0}_take_{1}";
    public const string ExamsAll = "exams_all";

    public static string CourseById(long courseId)
    {
        return $"course_{courseId}";
    }
    public static string QuestionById(long questionId)
    {
        return $"question_{questionId}";
    }
    public static string StudentById(long studentId)
    {
        return $"student_{studentId}";
    }

    public static string ExamById(long examId)
    {
        return $"exam_{examId}";
    }
}
