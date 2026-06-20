using FluentValidation;
using LMSPro.Api.Dtos;

namespace LMSPro.Api.Validators;

public class ExamCreateDtoValidator : AbstractValidator<ExamCreateDto>
{
    public ExamCreateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.PassingScorePercentage)
            .InclusiveBetween(0, 100)
            .WithMessage("Passing score percentage must be between 0 and 100.");

        RuleFor(x => x.LessonId)
            .GreaterThan(0)
            .WithMessage("LessonId must be greater than 0.");
    }
}