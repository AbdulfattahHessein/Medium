using FluentValidation;
using FluentValidation.Validators;
using Medium.BL.Features.Accounts.Request;
using Medium.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Medium.BL.Features.Accounts.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public RegisterRequestValidator(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;

            RuleFor(t => t.UserName)
                .NotEmpty()
                .WithMessage("{PropertyName} must be not empty")
                .NotNull()
                .WithMessage("{PropertyName} must be not empty")
                .MustAsync(async (r, i, c) =>
                 {
                     var user = await userManager.FindByNameAsync(r.UserName);
                     return user == null;
                 });

            RuleFor(t => t.Email)
               .NotEmpty()
               .WithMessage("{PropertyName} must be not empty")
               .NotNull()
               .WithMessage("{PropertyName} must be not empty")
               .EmailAddress()
               .MustAsync(async (r, i, c) =>
               {
                   var user = await userManager.FindByNameAsync(r.Email);
                   return user == null;
               })
                .WithMessage("Email is already used")
                .MustAsync(async (r, i, c) =>
                {
                    var user = await userManager.FindByEmailAsync(r.Email);
                    return user == null;
                })
                .WithMessage("Email is already used");

            RuleFor(t => t.Password).NotEmpty()
                .WithMessage("{PropertyName} must be not empty")
                .NotNull()
                .WithMessage("{PropertyName} must be not empty")
                .Must((r, i, c) =>
                {
                    return r.Password == r.ConfirmPassword;
                })
                .WithMessage("Password and confirm password is not matched");




        }
    }
}
