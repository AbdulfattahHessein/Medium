using FluentValidation;
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
                 })
                 .WithMessage("UserName is already used")
                .MustAsync(async (r, i, c) =>
                 {
                     var user = await userManager.FindByNameAsync(r.Email);
                     return user == null;
                 })
                 .WithMessage("Email is already used");

        }
    }
}
