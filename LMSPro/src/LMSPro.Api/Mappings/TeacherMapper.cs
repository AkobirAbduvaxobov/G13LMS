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

    public static TeacherGetDto ToGetDto(this Teacher teacher)
    {
        return new TeacherGetDto
        {
            TeacherId = teacher.TeacherId,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName
        };
    }

    public static void ToUpdateEntity(this TeacherUpdateDto dto, Teacher teacher)
    {
        teacher.FirstName = dto.FirstName;
        teacher.LastName = dto.LastName;
    }
}
