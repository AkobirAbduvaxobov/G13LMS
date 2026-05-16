namespace LMSPro.Api.Entities;

public class Lesson
{
    public long LessonId { get; set; }
    
    public string Title { get; set; }

    public string Content { get; set; }

    public int Order { get; set; }

    public TimeSpan Duration { get; set; }

    // Navigation Properties
    public Course Course { get; set; }
    public long CourseId { get; set; }

    public ICollection<Question> Questions { get; set; }

    public ICollection<Homework> Homeworks { get; set; }

    public ICollection<Resource> Resources { get; set; }
    public ICollection<Exam> Exams { get; set; }
}
