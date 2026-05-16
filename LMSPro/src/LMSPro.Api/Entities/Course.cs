namespace LMSPro.Api.Entities;

public class Course
{
    public long CourseId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }
    public int DurationDays { get; set; }
    public int AccessPeriodDays { get; set; }

    // Navigation Properties
    public ICollection<Enrollment> Enrollments { get; set; }
    public ICollection<TeacherCourse> TeacherCourses { get; set; }

    public ICollection<Lesson> Lessons { get; set; }
}
    