namespace LMSPro.Api.Dtos;

public class QuestionUpdateDto
{
    public string Text { get; set; }
    public string VariantA { get; set; }
    public string VariantB { get; set; }
    public string VariantC { get; set; }
    public string VariantD { get; set; }
    public string Answer { get; set; }
    public long LessonId { get; set; }
}
