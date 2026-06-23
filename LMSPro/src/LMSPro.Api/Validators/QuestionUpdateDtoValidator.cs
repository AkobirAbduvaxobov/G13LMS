using FluentValidation;
using LMSPro.Api.Dtos;

public class QuestionUpdateDtoValidator : AbstractValidator<QuestionUpdateDto>
{
    private static readonly string[] AllowedAnswers = { "A", "B", "C", "D" };

    public QuestionUpdateDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.VariantA).NotEmpty();
        RuleFor(x => x.VariantB).NotEmpty();
        RuleFor(x => x.VariantC).NotEmpty();
        RuleFor(x => x.VariantD).NotEmpty();

        RuleFor(x => x.Answer)
            .NotEmpty()
            .Must(x => AllowedAnswers.Contains(x))
            .WithMessage("Correct answer must be A, B, C or D");
    }
}