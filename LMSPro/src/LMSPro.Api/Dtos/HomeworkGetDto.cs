namespace LMSPro.Api.Dtos;

public class HomeworkGetDto
{
    public long HomeworkId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public long LessonId { get; set; }

    public string? LessonTitle { get; set; }

    public string? LessonContent { get; set; }
}