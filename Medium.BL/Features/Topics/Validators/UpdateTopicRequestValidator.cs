using FluentValidation;
using Medium.BL.Features.Topics.Request;

namespace Medium.BL.Features.Topics.Validators
{
    public class UpdateTopicRequestValidator : AbstractValidator<UpdateTopicRequest>
    {
        public UpdateTopicRequestValidator()
        {
            RuleFor(t => t.Id).NotEmpty().WithMessage("{PropertyName} must be not empty")
               .NotNull().WithMessage("{PropertyName} must be not null");

            RuleFor(t => t.Name).NotEmpty().WithMessage("{PropertyName} must be not empty")
                .NotNull().WithMessage("{PropertyName} must be not null");
        }
    }
}
