namespace LMSPro.Api.Dtos;

public class ExamCreateDto
{
    public string Title { get; set; }

    public int PassingScorePercentage { get; set; }

    public long LessonId { get; set; }
}
