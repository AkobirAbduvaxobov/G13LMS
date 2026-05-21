using LMSPro.Api.Entities;

namespace LMSPro.Api.Dtos;

public class TeacherGetDto
{
    public long TeacherId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<CourseGetDto>? CourseGetDtos { get; set; }
}
