namespace LMSPro.Api.Dtos;

public class QuestionCreateDto
{
    public string Text { get; set; } = string.Empty;

    public string VariantA { get; set; } = string.Empty;
    public string VariantB { get; set; } = string.Empty;
    public string VariantC { get; set; } = string.Empty;
    public string VariantD { get; set; } = string.Empty;

    public string Answer { get; set; }

    public long LessonId { get; set; }
}