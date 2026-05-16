namespace LMSPro.Api.Entities;

public class Homework
{
    public long HomeworkId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    // Navigation Property
    public Lesson Lesson { get; set; }
    public long LessonId { get; set; }
}
