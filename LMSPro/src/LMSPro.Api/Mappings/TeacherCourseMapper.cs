using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings;

public static class TeacherCourseMapper
{
    public static TeacherCourse ToEntity(this TeacherCourseCreateDto dto)
    {
        return new TeacherCourse
        {
            TeacherId = dto.TeacherId,
            CourseId = dto.CourseId
        };
    }

    public static TeacherCourseGetDto ToGetDto(this TeacherCourse teacherCourse)
    {
        return new TeacherCourseGetDto
        {
            TeacherCourseId = teacherCourse.TeacherCourseId,
            TeacherId = teacherCourse.TeacherId,
            CourseId = teacherCourse.CourseId,
            Teacher = teacherCourse.Teacher?.ToGetDto(),
            Course = teacherCourse.Course?.ToGetDto()
        };
    }

    public static void ToUpdateEntity(this TeacherCourseUpdateDto dto, TeacherCourse teacherCourse)
    {
        teacherCourse.TeacherId = dto.TeacherId;
        teacherCourse.CourseId = dto.CourseId;
    }
}
