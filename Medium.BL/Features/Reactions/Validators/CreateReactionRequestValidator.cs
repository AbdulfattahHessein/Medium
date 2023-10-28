using FluentValidation;
using Medium.BL.Features.Reactions.Request;
using Medium.BL.Interfaces.Services;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Reactions.Validators
{
    public class CreateReactionRequestValidator : AbstractValidator<CreateReactionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateReactionRequestValidator(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

            RuleFor(p => p.Name).NotNull().
                WithMessage("{PropertyName} Must be not Null")
                .NotEmpty().WithMessage("{PropertyName}Must be not empty");
        }
    }
}
