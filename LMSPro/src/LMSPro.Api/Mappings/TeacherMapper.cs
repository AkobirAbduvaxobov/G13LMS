using System.Linq;
using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings;

public static class TeacherMapper
{
    public static Teacher ToEntity(this TeacherCreateDto dto)
    {
        return new Teacher
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };
    }

    public static void ToUpdateEntity(this TeacherUpdateDto dto, Teacher teacher)
    {
        teacher.FirstName = dto.FirstName;
        teacher.LastName = dto.LastName;
    }

    public static TeacherGetDto ToGetDto(this Teacher teacher)
    {
        var teacherGetDto = new TeacherGetDto
        {
            TeacherId = teacher.TeacherId,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName
        };

        if (teacher.TeacherCourses != null)
        {
            teacherGetDto.CourseGetDtos = teacher.TeacherCourses.Select(tc => new CourseGetDto
            {
                CourseId = tc.CourseId,
                Title = tc.Course.Title,
                Description = tc.Course.Description,
                Price = tc.Course.Price,
                CreatedAt = tc.Course.CreatedAt,
                DurationDays = tc.Course.DurationDays,
                AccessPeriodDays = tc.Course.AccessPeriodDays,
                IsActive = tc.Course.IsActive
            }).ToList();
        }

        return teacherGetDto;
    }
}

