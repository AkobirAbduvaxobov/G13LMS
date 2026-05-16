namespace LMSPro.Api.Entities;

public class Resource
{
    public long ResourceId { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public string Type { get; set; }

    // Navigation Property
    public Lesson Lesson { get; set; }
    public long LessonId { get; set; }
}
