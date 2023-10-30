using Medium.BL.Features.Accounts.Request;
using Medium.BL.Features.Accounts.Response;
using Medium.BL.ResponseHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.BL.Interfaces.Services
{
    public interface IAccountsService
    {
        Task<ApiResponse<RegisterResponse>> Register(RegisterRequest request);
        Task<ApiResponse<LoginResponse>> Login(LoginRequest request);
    }


}
