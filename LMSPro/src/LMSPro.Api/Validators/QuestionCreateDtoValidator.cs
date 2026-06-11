using FluentValidation;
using LMSPro.Api.Dtos;

namespace LMSPro.Api.Validators
{
    public class QuestionCreateDtoValidator : AbstractValidator<QuestionCreateDto>
    {
        public QuestionCreateDtoValidator()
        {
            RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(1000).WithMessage("Text must not exceed 1000 characters.");

            RuleFor(x => x.VariantA)
                .NotEmpty().WithMessage("VariantA is required.")
                .MaximumLength(500).WithMessage("VariantA must not exceed 500 characters.");

            RuleFor(x => x.VariantB)
                .NotEmpty().WithMessage("VariantB is required.")
                .MaximumLength(500).WithMessage("VariantB must not exceed 500 characters.");

            RuleFor(x => x.VariantC)
                .NotEmpty().WithMessage("VariantC is required.")
                .MaximumLength(500).WithMessage("VariantC must not exceed 500 characters.");

            RuleFor(x => x.VariantD)
                .NotEmpty().WithMessage("VariantD is required.")
                .MaximumLength(500).WithMessage("VariantD must not exceed 500 characters.");

            RuleFor(x => x.Answer)
                .NotEmpty().WithMessage("Answer is required.")
                .MaximumLength(500).WithMessage("Answer must not exceed 500 characters.");

            RuleFor(x => x.LessonId)
                .GreaterThan(0).WithMessage("LessonId must be greater than 0.");
        }
    }
}
