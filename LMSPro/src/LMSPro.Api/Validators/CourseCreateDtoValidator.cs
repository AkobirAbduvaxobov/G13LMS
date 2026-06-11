using FluentValidation;
using LMSPro.Api.Dtos;
using System.Text.RegularExpressions;

namespace LMSPro.Api.Validators;

public class CourseCreateDtoValidator : AbstractValidator<CourseCreateDto>
{
    public CourseCreateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .Must(BeValidTitle).WithMessage("Title must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value.");
        RuleFor(x => x.DurationDays)
            .GreaterThan(0).WithMessage("DurationDays must be greater than 0.");
        RuleFor(x => x.AccessPeriodDays)
            .GreaterThan(0).WithMessage("AccessPeriodDays must be greater than 0.");
    }


    // Titlega bunday validation yozilmaydi, Misol tariqasida ko'rsatilgan
    private bool BeValidTitle(string title)
    {
        var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,50}$";
        return Regex.IsMatch(title, pattern);
    }
}
