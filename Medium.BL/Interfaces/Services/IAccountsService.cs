using Medium.BL.Features.Accounts.Request;
using Medium.BL.Features.Accounts.Response;
using Medium.BL.ResponseHandler;

namespace Medium.BL.Interfaces.Services
{
    public interface IAccountsService
    {
        Task<ApiResponse<RegisterResponse>> Register(RegisterRequest request);
        Task<ApiResponse<LoginResponse>> Login(LoginRequest request);
    }


}
