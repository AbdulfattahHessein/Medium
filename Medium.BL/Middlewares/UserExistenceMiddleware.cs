using Medium.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Medium.BL.Middlewares
{
    public class UserExistenceMiddleware
    {

        private readonly RequestDelegate _next;

        public UserExistenceMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var userManager = (UserManager<ApplicationUser>)context.RequestServices.GetService(typeof(UserManager<ApplicationUser>))!;

            // Retrieve the JWT token from the Authorization header
            string token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (securityToken != null)
                {
                    // You can access claims and check the token's validity here

                    string userId = securityToken.Claims.First(claim => claim.Type == "sub").Value;

                    // Verify the user existence using your database or user store
                    bool userExists = userManager.FindByIdAsync(userId) != null;

                    if (userExists)
                    {
                        // User exists; continue processing the request
                        await _next(context);
                    }
                    else
                    {
                        // User doesn't exist; return a forbidden response or handle as needed
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return;
                    }
                }
            }

            await _next(context);
        }

        private bool VerifyUserExistence(string userId)
        {
            return true;
        }
    }
}