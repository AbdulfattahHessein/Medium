using Medium.BL.Features.Accounts.Request;
using Medium.BL.ResponseHandler;

namespace Medium.BL.Interfaces.Services
{
    public interface IRoleServices
    {
        Task<ApiResponse<string>> AddRoleToUser(AddRoleRequest request);
        Task<ApiResponse<string>> UpdateUserRoles(UpdateRoleToUserRequest request);


    }
}
