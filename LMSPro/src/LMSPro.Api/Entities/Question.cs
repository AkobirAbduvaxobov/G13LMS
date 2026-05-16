namespace LMSPro.Api.Entities;

public class Question
{
    public long QuestionId { get; set; }

    public string Text { get; set; }
    public string VariantA { get; set; }
    public string VariantB { get; set; }
    public string VariantC { get; set; }
    public string VariantD { get; set; }
    public string Answer { get; set; }

    // Navigation Property
    public Lesson Lesson { get; set; }
    public long LessonId { get; set; }
}
