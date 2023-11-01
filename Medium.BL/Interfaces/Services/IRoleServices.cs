using Medium.BL.Features.Accounts.Request;
using Medium.BL.ResponseHandler;

namespace Medium.BL.Interfaces.Services
{
    public interface IRoleServices
    {
        Task<ApiResponse<string>> AddRoleToUser(AddRoleUserRequest request);
        Task<ApiResponse<string>> UpdateUserRoles(UpdateRoleToUserRequest request);
        //        Task<ApiResponse<CreateRoleResponse>> CreateRoleAsync(AddRoleRequest request);
        //        Task<ApiResponse<DeleteRoleResponse>> DeleteRoleAsync(DeleteRoleRequest request);
        //        Task<ApiResponse<List<GetAllRoleResponse>>> GetAllRolesAsync();
        //        Task<ApiResponse<GetRoleResponse>> GetRoleByNameAsync(GetRoleRequest request);
        //        Task<ApiResponse<UpdateRoleResponse>> UpdateRoleAsync(UpdateRoleRequest request);






    }
}
