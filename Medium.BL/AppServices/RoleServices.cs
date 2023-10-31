using AutoMapper;
using Medium.BL.Features.Accounts.Request;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Medium.BL.AppServices
{
    public class RoleServices : AppService, IRoleServices
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleServices(IConfiguration configuration, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<ApiResponse<string>> AddRoleToUser(AddRoleRequest request)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return ApiResponseHandler.NotFound<string>("User not found");
            }

            var result = await userManager.AddToRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                return ApiResponseHandler.Success($"Role '{request.RoleName}' added to user '{user.UserName}'");
            }

            return ApiResponseHandler.BadRequest<string>("Failed to add role");
        }

        public async Task<ApiResponse<string>> UpdateUserRoles(UpdateRoleToUserRequest request)
        {
            var user = await userManager.FindByIdAsync(request.userId);

            if (user == null)
            {
                return ApiResponseHandler.NotFound<string>("User not found");
            }

            // Remove the user from their current roles
            var currentRoles = await userManager.GetRolesAsync(user);
            var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!removeResult.Succeeded)
            {
                return ApiResponseHandler.BadRequest<string>("Failed to remove current roles");
            }

            // Add the new roles to the user
            var addResult = await userManager.AddToRolesAsync(user, request.newRoles);

            if (addResult.Succeeded)
            {
                return ApiResponseHandler.Success($"User roles updated for '{user.UserName}'");
            }

            return ApiResponseHandler.BadRequest<string>("Failed to update user roles");
        }
    }
}

