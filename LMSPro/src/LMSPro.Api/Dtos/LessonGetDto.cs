namespace LMSPro.Api.Dtos;

public class LessonGetDto
{
    public long LessonId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public int Order { get; set; }

    public TimeSpan Duration { get; set; }

    public long CourseId { get; set; }

    public ICollection<QuestionGetDto>? Questions { get; set; }

    public ICollection<HomeworkGetDto>? Homeworks { get; set; }

    public ICollection<ResourceGetDto>? Resources { get; set; }
    public ICollection<ExamGetDto>? Exams { get; set; }

}
