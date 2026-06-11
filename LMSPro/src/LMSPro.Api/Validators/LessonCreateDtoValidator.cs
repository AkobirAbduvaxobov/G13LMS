using FluentValidation;
using LMSPro.Api.Dtos;

namespace LMSPro.Api.Validators
{
    public class LessonCreateDtoValidator : AbstractValidator<LessonCreateDto>
    {
        public LessonCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be 0 or greater.");

            RuleFor(x => x.Duration)
                .GreaterThan(TimeSpan.Zero).WithMessage("Duration must be a positive length of time.");

            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("A valid Course ID is required.");
        }
    }
}