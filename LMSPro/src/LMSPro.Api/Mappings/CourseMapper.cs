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

    public static CourseGetDto ToGetDto(this Course course)
    {
        var courseGetDto = new CourseGetDto
        {
            CourseId = course.CourseId,
            Title = course.Title,
            Description = course.Description,
            Price = course.Price,
            CreatedAt = course.CreatedAt,
            IsActive = course.IsActive,
            DurationDays = course.DurationDays,
            AccessPeriodDays = course.AccessPeriodDays,
        };

        if (course.Lessons != null)
        {
            courseGetDto.LessonGetDtos = course.Lessons.Select(lesson => new LessonGetDto
            {
                LessonId = lesson.LessonId,
                Title = lesson.Title,
                Content = lesson.Content,
                Order = lesson.Order,
                Duration = lesson.Duration
            }).ToList();
        }

        if (course.TeacherCourses != null)
        {
            courseGetDto.TeacherGetDtos = course.TeacherCourses.Select(tc => new TeacherGetDto
            {
                TeacherId = tc.TeacherId,
                FirstName = tc.Teacher.FirstName,
                LastName = tc.Teacher.LastName,
            }).ToList();
        }

        return courseGetDto;
    }

    public static void ToUpdateEntity(this CourseUpdateDto dto, Course course)
    {
        course.Title = dto.Title;
        course.Description = dto.Description;
        course.Price = dto.Price;
        course.DurationDays = dto.DurationDays;
        course.AccessPeriodDays = dto.AccessPeriodDays;
        course.IsActive = dto.IsActive;
    }
}
