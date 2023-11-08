using FluentValidation;
using Medium.BL.Features.Accounts.Request;
using Medium.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Medium.BL.Features.Accounts.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public LoginRequestValidator(UserManager<ApplicationUser> userManager)
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
                     var user = await userManager.FindByNameAsync(r.UserName);
                     var rightPassword = await userManager.CheckPasswordAsync(user, r.Password);
                     return rightPassword;
                 })
                 .WithMessage("Invalid Username or Password")
                 .MustAsync(async (r, i, c) =>
                 {
                     var user = await userManager.FindByNameAsync(r.UserName);
                     var rightPassword = await userManager.CheckPasswordAsync(user, r.Password);
                     return rightPassword;
                 })
                 .WithMessage("Invalid Username or Password");

        }
    }
}
