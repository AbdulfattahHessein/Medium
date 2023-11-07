using FluentValidation;
using Medium.BL.Interfaces.Services;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Accounts.Validators
{
    public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public ConfirmEmailRequestValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            RuleFor(r => r.UserId).NotNull().
    WithMessage("{PropertyName} Must be not Null")
    .NotEmpty().WithMessage("{PropertyName} Must be valid");
            RuleFor(r => r.Code).NotNull().
    WithMessage("{PropertyName} Must be not Null")
    .NotEmpty().WithMessage("{PropertyName} Must be valid");

        }
    }
}
