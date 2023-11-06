using FluentValidation;
using Medium.BL.Interfaces.Services;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Accounts.Validators
{
    internal class ResetPasswordRequestValidation : AbstractValidator<ResetPasswordRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public ResetPasswordRequestValidation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            RuleFor(r => r.Email).NotNull().
    WithMessage("{PropertyName} Must be not Null")
    .NotEmpty().WithMessage("{PropertyName} Must be valid");

            RuleFor(r => r.Password).NotNull().
  WithMessage("{PropertyName} Must be not Null")
  .NotEmpty().WithMessage("{PropertyName} Must be valid");

            RuleFor(r => r.ConfirmPassword).NotNull().
  WithMessage("{PropertyName} Must be not Null")
  .NotEmpty().WithMessage("{PropertyName} Must be valid");


        }

    }
}