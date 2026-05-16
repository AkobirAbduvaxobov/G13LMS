namespace LMSPro.Api.Entities;

public class TeacherCourse
{
    public long TeacherCourseId { get; set; }
    public long TeacherId { get; set; }
    public long CourseId { get; set; }
    public Teacher Teacher { get; set; }
    public Course Course { get; set; }
}
