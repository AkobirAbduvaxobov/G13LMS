using FluentValidation;
using LMSPro.Api.Dtos;

namespace LMSPro.Api.Validators;

public class TeacherCourseCreateDtoValidator : AbstractValidator<TeacherCourseCreateDto>
{
    public TeacherCourseCreateDtoValidator()
    {
        RuleFor(x => x.TeacherId)
            .GreaterThan(0).WithMessage("TeacherId must be greater than 0.");

        RuleFor(x => x.CourseId)
            .GreaterThan(0).WithMessage("CourseId must be greater than 0.");
    }
}
