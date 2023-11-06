using FluentValidation;
using Medium.BL.Features.Accounts.Request;
using Medium.Core.Interfaces.Bases;

namespace Medium.BL.Features.Accounts.Validators
{
    public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateRoleRequestValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            RuleFor(r => r.RoleName).NotNull().
    WithMessage("{PropertyName} Must be not Null")
    .NotEmpty().WithMessage("{PropertyName} Must be valid");
            RuleFor(r => r.OldRoleName).NotNull().
    WithMessage("{PropertyName} Must be not Null")
    .NotEmpty().WithMessage("{PropertyName} Must be valid");
        }
    }
}
