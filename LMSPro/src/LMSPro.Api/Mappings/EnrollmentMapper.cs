using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings;

public static class EnrollmentMapper
{
    public static Enrollment ToEntity(this EnrollmentCreateDto dto)
    {
        return new Enrollment
        {
            EnrolledAt = DateTime.UtcNow,
            StudentId = dto.StudentId,
            CourseId = dto.CourseId
        };
    }

    public static EnrollmentGetDto ToGetDto(this Enrollment enrollment)
    {
        var dto = new EnrollmentGetDto
        {
            EnrollmentId = enrollment.EnrollmentId,
            EnrolledAt = enrollment.EnrolledAt,
            StudentId = enrollment.StudentId,
            CourseId = enrollment.CourseId
        };

        if (enrollment.Student != null)
        {
            dto.StudentFirstName = enrollment.Student.FirstName;
            dto.StudentLastName = enrollment.Student.LastName;
        }

        if (enrollment.Course != null)
        {
            dto.CourseTitle = enrollment.Course.Title;
        }

        return dto;
    }

    public static void ToUpdateEntity(this EnrollmentUpdateDto dto, Enrollment enrollment)
    {
        enrollment.StudentId = dto.StudentId;
        enrollment.CourseId = dto.CourseId;
    }
}