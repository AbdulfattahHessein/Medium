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
                .NotEmpty().WithMessage("{PropertyName} must be not empty")
                .MustAsync(async (r, i, c) =>
                 {
                     var user = await userManager.FindByNameAsync(r.UserName);
                     return user == null;
                 })
                .WithMessage("Username is already used.");

            RuleFor(t => t.Email)
               .NotEmpty().WithMessage("{PropertyName} must be not empty")
               .EmailAddress().WithMessage("Invalid email address.")
               .MustAsync(async (r, i, c) =>
               {
                   var user = await userManager.FindByEmailAsync(r.Email);
                   return user == null;
               }).WithMessage("Email is already used");

            RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
            //.Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");


            RuleFor(t => t.ConfirmPassword)
               .NotEmpty().WithMessage("{PropertyName} must be not empty")
               .Equal(model => model.Password).WithMessage("Passwords do not match.");

        }
    }
}
