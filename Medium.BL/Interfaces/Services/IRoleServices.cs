using Medium.BL.Features.Accounts.Request;
using Medium.BL.Features.Accounts.Response;
using Medium.BL.ResponseHandler;
using Microsoft.AspNetCore.Identity;

namespace Medium.BL.Interfaces.Services
{
    public interface IRoleServices
    {
        Task<ApiResponse<string>> AddRoleToUser(AddRoleUserRequest request);
        Task<ApiResponse<string>> UpdateUserRoles(UpdateRoleToUserRequest request);
        Task<ApiResponse<CreateRoleResponse>> CreateRoleAsync(AddRoleRequest request);
        Task<ApiResponse<DeleteRoleResponse>> DeleteRoleAsync(DeleteRoleRequest request);
        Task<List<IdentityRole<int>>> GetAllRolesAsync();
        Task<ApiResponse<GetRoleResponse>> GetRoleByNameAsync(GetRoleRequest request);
        Task<IdentityResult> UpdateRoleAsync(UpdateRoleRequest request);






    }
}
