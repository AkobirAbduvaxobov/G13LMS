namespace LMSPro.Api.Dtos;

public class QuestionGetDto
{
    public long QuestionId { get; set; }
    public string Text { get; set; }
    public string VariantA { get; set; }
    public string VariantB { get; set; }
    public string VariantC { get; set; }
    public string VariantD { get; set; }
    public string Answer { get; set; }
    public long LessonId { get; set; }
    public string? LessonTitle { get; set; }

    public string? LessonContent { get; set; }
}
