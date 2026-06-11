using FluentValidation;
using LMSPro.Api.Dtos;

namespace LMSPro.Api.Validators;

public class CourseUpdateDtoValidator : AbstractValidator<CourseUpdateDto>
{
    public CourseUpdateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value.");
        RuleFor(x => x.DurationDays)
            .GreaterThan(0).WithMessage("DurationDays must be greater than 0.");
        RuleFor(x => x.AccessPeriodDays)
            .GreaterThan(0).WithMessage("AccessPeriodDays must be greater than 0.");
    }
}
