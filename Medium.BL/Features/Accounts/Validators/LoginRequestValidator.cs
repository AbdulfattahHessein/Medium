using FluentValidation;
using Medium.BL.Features.Accounts.Request;
using Medium.BL.Features.Topics.Request;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                 .WithMessage("Invalid Username or Password");

        }
    }
}
