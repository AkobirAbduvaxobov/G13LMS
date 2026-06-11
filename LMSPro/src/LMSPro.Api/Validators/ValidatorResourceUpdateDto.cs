using FluentValidation;
using LMSPro.Api.Dtos;
using System.Text.RegularExpressions;

namespace LMSPro.Api.Validators
{
    public class ValidatorResourceUpdateDto : AbstractValidator<ResourceUpdateDto>
    {
        public ValidatorResourceUpdateDto()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .Must(BeValidTitle).WithMessage("Title must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.LessonId)
                .GreaterThan(0).WithMessage("DurationDays must be greater than 0.");
            
        }


        // Titlega bunday validation yozilmaydi, Misol tariqasida ko'rsatilgan
        private bool BeValidTitle(string title)
        {
            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,50}$";
            return Regex.IsMatch(title, pattern);
        }
    }
}
