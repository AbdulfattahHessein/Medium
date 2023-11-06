using Medium.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Medium.BL.Features.Accounts.Validators
{
    public class CustomUserValidator<TUser> : IUserValidator<TUser>
    where TUser : ApplicationUser
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            var errors = new List<IdentityError>();

            // You can add custom validation logic here


            return Task.FromResult(IdentityResult.Success);
        }
    }

}
