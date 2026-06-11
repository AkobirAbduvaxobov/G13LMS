using FluentValidation;
using LMSPro.Api.Dtos;

namespace LMSPro.Api.Validators;

public class ResourceCreateDtoValidator : AbstractValidator<ResourceCreateDto>
{
    public ResourceCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url is required.")
            .MaximumLength(500).WithMessage("Url must not exceed 500 characters.");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type is required.")
            .MaximumLength(100).WithMessage("Type must not exceed 100 characters.");

        RuleFor(x => x.LessonId)
            .GreaterThan(0).WithMessage("LessonId must be greater than 0.");
    }
}
