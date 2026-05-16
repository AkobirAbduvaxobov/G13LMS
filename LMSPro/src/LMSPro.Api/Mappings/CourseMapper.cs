using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings;

public static class CourseMapper
{
    public static Course ToEntity(this CourseCreateDto dto)
    {
        return new Course
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            CreatedAt = DateTime.UtcNow,
            IsActive = dto.IsActive,
            DurationDays = dto.DurationDays,
            AccessPeriodDays = dto.AccessPeriodDays
        };
    }

    public static CourseGetDto ToGetDto(this Course entity)
    {
        return new CourseGetDto
        {
            CourseId = entity.CourseId,
            Title = entity.Title,
            Description = entity.Description,
            Price = entity.Price,
            CreatedAt = entity.CreatedAt,
            IsActive = entity.IsActive,
            DurationDays = entity.DurationDays,
            AccessPeriodDays = entity.AccessPeriodDays,
            Teachers = null,
            Lessons = null
        };
    }
}
