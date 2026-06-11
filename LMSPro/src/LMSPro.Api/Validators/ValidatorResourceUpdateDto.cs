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
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
                
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.LessonId)
                .GreaterThan(0).WithMessage("DurationDays must be greater than 0.");
            
        }


        
    }
}
