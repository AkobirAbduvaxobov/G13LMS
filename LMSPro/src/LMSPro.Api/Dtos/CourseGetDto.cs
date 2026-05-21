using LMSPro.Api.Entities;

namespace LMSPro.Api.Dtos;

public class CourseGetDto
{
    public long CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public int DurationDays { get; set; }
    public int AccessPeriodDays { get; set; }
    public bool IsActive { get; set; }
    public int EnrollmentCount { get; set; }

    // Navigation Properties
    public ICollection<TeacherGetDto>? TeacherGetDtos { get; set; }
    public ICollection<LessonGetDto>? LessonGetDtos { get; set; }
}
