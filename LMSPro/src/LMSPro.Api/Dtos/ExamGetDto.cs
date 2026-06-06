namespace LMSPro.Api.Dtos;

public class ExamGetDto
{
    public long ExamId { get; set; }

    public string Title { get; set; }

    public int PassingScorePercentage { get; set; }

    public long LessonId { get; set; }

    // Navigation Properties
    public LessonGetDto? Lesson { get; set; } 
}
