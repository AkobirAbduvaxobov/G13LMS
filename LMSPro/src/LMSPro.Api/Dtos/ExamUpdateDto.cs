namespace LMSPro.Api.Dtos;

public class ExamUpdateDto
{
    public string Title { get; set; }
    public long LessonId { get; set; }
    public int PassingScorePercentage { get; set; }
}
