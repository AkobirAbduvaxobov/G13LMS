namespace LMSPro.Api.Dtos;

public class TeacherCourseGetDto
{
    public long TeacherCourseId { get; set; }
    public long TeacherId { get; set; }
    public long CourseId { get; set; }
    public TeacherGetDto? Teacher { get; set; }
    public CourseGetDto? Course { get; set; }
}
