namespace LMSPro.Api.Entities;

public class Teacher
{
    public long TeacherId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<TeacherCourse> TeacherCourses { get; set; }
}
