using FluentValidation;
using LMSPro.Api.Dtos;

namespace LMSPro.Api.Validators;

public class EnrollmentCreateDtoValidator : AbstractValidator<EnrollmentCreateDto>
{
    public EnrollmentCreateDtoValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("StudentId must be greater than 0.");

        RuleFor(x => x.CourseId)
            .GreaterThan(0).WithMessage("CourseId must be greater than 0.");
    }
}
