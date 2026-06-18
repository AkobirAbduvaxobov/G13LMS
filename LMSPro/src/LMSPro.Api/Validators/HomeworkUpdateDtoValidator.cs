using FluentValidation;
using LMSPro.Api.Dtos;

namespace LMSPro.Api.Validators;

public class HomeworkUpdateDtoValidator : AbstractValidator<HomeworkUpdateDto>
{
    public HomeworkUpdateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(x => x.LessonId)
            .GreaterThan(0).WithMessage("Invalid LessonId.");
    }
}