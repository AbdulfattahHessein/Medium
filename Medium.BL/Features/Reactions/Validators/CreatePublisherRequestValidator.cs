using FluentValidation;
using Medium.BL.Interfaces.Services;

namespace Medium.BL.Features.Reactions.Validators
{
    public class CreateReactionRequestValidator : AbstractValidator<CreateReactionRequest>
    {
        public CreateReactionRequestValidator()
        {
            RuleFor(p => p.Name).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName}Must be not empty");
        }
    }
}
