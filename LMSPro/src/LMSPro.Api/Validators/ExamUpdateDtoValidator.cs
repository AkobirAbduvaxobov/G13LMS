using FluentValidation;
using LMSPro.Api.Dtos;

namespace LMSPro.Api.Validators;

public class ExamUpdateDtoValidator : AbstractValidator<ExamUpdateDto>
{
    public ExamUpdateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.LessonId)
            .GreaterThan(0).WithMessage("Invalid LessonId.");

        RuleFor(x => x.PassingScorePercentage)
            .InclusiveBetween(0, 100).WithMessage("Passing score must be between 0 and 100.");
    }
}