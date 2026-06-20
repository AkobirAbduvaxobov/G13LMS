using FluentValidation;
using LMSPro.Api.Dtos;

namespace LMSPro.Api.Validators;

public class LessonUpdateDtoValidator : AbstractValidator<LessonUpdateDto>
{
    public LessonUpdateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(5000).WithMessage("Content must not exceed 5000 characters.");

        RuleFor(x => x.Order)
            .GreaterThan(0).WithMessage("Order must be greater than 0.");

        RuleFor(x => x.Duration)
            .GreaterThan(TimeSpan.Zero).WithMessage("Duration must be greater than 0.");

        RuleFor(x => x.CourseId)
            .GreaterThan(0).WithMessage("CourseId must be greater than 0.");
    }
}