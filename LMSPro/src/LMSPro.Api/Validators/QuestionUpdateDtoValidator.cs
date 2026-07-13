using FluentValidation;
using LMSPro.Api.Dtos;

namespace LMSPro.Api.Validators;

public class QuestionUpdateDtoValidator : AbstractValidator<QuestionUpdateDto>
{
    public QuestionUpdateDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Question text is required.")
            .MaximumLength(500).WithMessage("Question text must not exceed 500 characters.");

        RuleFor(x => x.VariantA)
            .NotEmpty().WithMessage("Variant A is required.");

        RuleFor(x => x.VariantB)
            .NotEmpty().WithMessage("Variant B is required.");

        RuleFor(x => x.VariantC)
            .NotEmpty().WithMessage("Variant C is required.");

        RuleFor(x => x.VariantD)
            .NotEmpty().WithMessage("Variant D is required.");

        RuleFor(x => x.Answer)
            .NotEmpty().WithMessage("Answer is required.")
            .Must(BeValidAnswer)
            .WithMessage("Answer must be one of: A, B, C, D.");

        RuleFor(x => x.LessonId)
            .GreaterThan(0).WithMessage("LessonId must be greater than 0.");
    }

    private bool BeValidAnswer(string answer)
    {
        return answer == "A"
               || answer == "B"
               || answer == "C"
               || answer == "D";
    }
}