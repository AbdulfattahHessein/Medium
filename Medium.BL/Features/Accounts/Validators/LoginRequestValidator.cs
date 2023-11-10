using FluentValidation;
using Medium.BL.Features.Accounts.Request;
using Medium.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Medium.BL.Features.Accounts.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {

            RuleFor(t => t.UserName)
                .NotEmpty().WithMessage("{PropertyName} must be not empty");
            RuleFor(t => t.Password)
               .NotEmpty().WithMessage("{PropertyName} must be not empty");
        }
    }
}
