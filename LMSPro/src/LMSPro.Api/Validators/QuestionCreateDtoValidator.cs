using FluentValidation;
using LMSPro.Api.Dtos;

namespace LMSPro.Api.Validators
{
    public class QuestionCreateDtoValidator : AbstractValidator<QuestionCreateDto>
    {
        public QuestionCreateDtoValidator()
        {
            RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Question text is required")
            .MaximumLength(500);

            RuleFor(x => x.VariantA)
                .NotEmpty()
                .WithMessage("Variant A is required");

            RuleFor(x => x.VariantB)
                .NotEmpty()
                .WithMessage("Variant B is required");

            RuleFor(x => x.VariantC)
                .NotEmpty()
                .WithMessage("Variant C is required");

            RuleFor(x => x.VariantD)
                .NotEmpty()
                .WithMessage("Variant D is required");

            RuleFor(x => x.Answer)
                .NotEmpty()
                .WithMessage("Answer is required");

            RuleFor(x => x.LessonId)
                .GreaterThan(0)
                .WithMessage("LessonId must be greater than 0");

            RuleFor(x => x.Answer)
    .Must(answer => new[] { "A", "B", "C", "D" }.Contains(answer))
    .WithMessage("Answer must be A, B, C, or D");
        }
    }
}
