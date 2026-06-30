using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings;

public static class StudentMapper
{
    public static Student ToEntity(this StudentCreateDto dto)
    {
        return new Student
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            RegisteredAt = DateTime.UtcNow
        };
    }

    public static StudentGetDto ToGetDto(this Student student)
    {
        return new StudentGetDto
        {
            StudentId = student.StudentId,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            RegisteredAt = student.RegisteredAt
        };
    }

    public static void ToUpdateEntity(this StudentUpdateDto dto, Student student)
    {
        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.Email = dto.Email;
    }
}
