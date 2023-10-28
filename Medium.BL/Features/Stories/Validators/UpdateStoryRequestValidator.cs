using FluentValidation;
using Medium.BL.Features.Stories.Requests;

namespace Medium.BL.Features.Stories.Validators
{
    public class UpdateStoryRequestValidator : AbstractValidator<UpdateStoryRequest>
    {
        public UpdateStoryRequestValidator()
        {
            RuleFor(s => s.Id)
              .GreaterThan(0).WithMessage("Id must be greater than 0")
              .NotNull().WithMessage("Id Must be not null")
              .NotEmpty().WithMessage("Id Must be not empty");

            RuleFor(s => s.Title)
                .NotNull().WithMessage("{PropertyName}Must be not null")
                .NotEmpty().WithMessage("{PropertyName}Must be not empty");

            RuleFor(s => s.Content)
    .NotNull().WithMessage("{PropertyName}Must be not null")
    .NotEmpty().WithMessage("{PropertyName}Must be not empty");
        }
    }
}
