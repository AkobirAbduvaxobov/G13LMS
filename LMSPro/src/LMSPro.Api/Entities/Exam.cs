namespace LMSPro.Api.Entities;

public class Exam
{
    public long ExamId { get; set; }
    public string Title { get; set; }
    public int PassingScorePercentage { get; set; }
    public Lesson Lesson { get; set; }
    public long LessonId { get; set; }
}
