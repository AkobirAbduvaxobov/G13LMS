using LMSPro.Api.Entities;

namespace LMSPro.Api.Dtos;

public class CourseCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public int DurationDays { get; set; }
    public int AccessPeriodDays { get; set; }
}
