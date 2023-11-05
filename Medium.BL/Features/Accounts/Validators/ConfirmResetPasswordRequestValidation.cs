using FluentValidation;
using Medium.BL.Interfaces.Services;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Accounts.Validators
{
    public class ConfirmResetPasswordRequestValidation : AbstractValidator<ConfirmResetPasswordRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public ConfirmResetPasswordRequestValidation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            RuleFor(r => r.Email).NotNull().
    WithMessage("{PropertyName} Must be not Null")
    .NotEmpty().WithMessage("{PropertyName} Must be valid");
            RuleFor(r => r.Code).NotNull().
    WithMessage("{PropertyName} Must be not Null")
    .NotEmpty().WithMessage("{PropertyName} Must be valid");

        }

    }
}