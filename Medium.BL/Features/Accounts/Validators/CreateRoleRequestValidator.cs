using FluentValidation;
using Medium.BL.Features.Accounts.Request;
using Medium.Core.Interfaces.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.BL.Features.Accounts.Validators
{
    public class CreateRoleRequestValidator : AbstractValidator<AddRoleRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateRoleRequestValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            RuleFor(r=>r.RoleName).NotNull().
    WithMessage("{PropertyName} Must be not Null")
    .NotEmpty().WithMessage("{PropertyName} Must be valid");
        }
    }
}
