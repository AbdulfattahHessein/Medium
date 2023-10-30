using Medium.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Medium.BL.Features.Accounts.Validators
{
    public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : ApplicationUser
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var errors = new List<IdentityError>();

            // Add your custom password validation rules here

            return Task.FromResult(IdentityResult.Success);
        }
    }

}
