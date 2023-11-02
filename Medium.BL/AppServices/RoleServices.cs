using AutoMapper;
using FluentValidation;
using Medium.BL.Features.Accounts.Request;
using Medium.BL.Features.Accounts.Response;
using Medium.BL.Features.Accounts.Validators;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
//using System.ComponentModel.DataAnnotations;
using static Medium.BL.ResponseHandler.ApiResponseHandler;
//using ValidationException = FluentValidation.ValidationException;

namespace Medium.BL.AppServices

{
    public class RoleServices : AppService, IRoleServices
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RoleServices(RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApiResponse<string>> AddRoleToUser(AddRoleUserRequest request)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return NotFound<string>("User not found");
            }

            var result = await userManager.AddToRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                return Success($"Role '{request.RoleName}' added to user '{user.UserName}'");
            }

            return BadRequest<string>("Failed to add role");
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
                return BadRequest<string>("Failed to remove current roles");
            }

            // Add the new roles to the user
            var addResult = await userManager.AddToRolesAsync(user, request.newRoles);

            if (addResult.Succeeded)
            {
                return Success($"User roles updated for '{user.UserName}'");
            }

            return BadRequest<string>("Failed to update user roles");
        }

        public async Task<ApiResponse<GetRoleResponse>> GetRoleByNameAsync(GetRoleRequest request)
        {
            var validator = new GetRoleRequestValidator(UnitOfWork);
            var validateResult = await validator.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
            var role = await _roleManager.FindByNameAsync(request.Name ?? "");
            if (role == null)
            {
                return NotFound<GetRoleResponse>("Role not found");
            }
            var response = Mapper.Map<GetRoleResponse>(role);
            return Success(response);


        }

        public async Task<List<IdentityRole<int>>> GetAllRolesAsync()
        {
            return await Task.Run(() => _roleManager.Roles.ToList());
        }

        public async Task<IdentityResult> UpdateRoleAsync(UpdateRoleRequest request)
        {
            var validator = new UpdateRoleRequestValidator(UnitOfWork);
            var validateResult = await validator.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
            var role = await _roleManager.FindByNameAsync(request.OldRoleName);
            if (role == null)
                throw new Exception($"Role '{request.RoleName}' not found.");

            role.Name = request.RoleName;
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<ApiResponse<DeleteRoleResponse>> DeleteRoleAsync(DeleteRoleRequest request)
        {
            var validator = new DeleteRoleRequestValidator(UnitOfWork);
            var validateResult = await validator.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role == null)
                throw new Exception($"Role '{request.RoleName}' not found.");

            var roleDeleted = await _roleManager.DeleteAsync(role);
            if (!roleDeleted.Succeeded)
            {
                return BadRequest<DeleteRoleResponse>("Deleteted Role is Failed");
            }
            return Success<DeleteRoleResponse>("Deleteted Role is Successed");
        }

        public async Task<ApiResponse<CreateRoleResponse>> CreateRoleAsync(AddRoleRequest request)
        {
            var validator = new CreateRoleRequestValidator(UnitOfWork);
            var validateResult = await validator.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
            var role = new IdentityRole<int> { Name = request.RoleName };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Success(new CreateRoleResponse(request.RoleName), "Role created succefully");
            }

            return BadRequest<CreateRoleResponse>("Craete Role is Failed");
        }

    }
}

