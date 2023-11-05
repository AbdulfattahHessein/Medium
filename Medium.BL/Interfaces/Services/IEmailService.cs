using Medium.BL.ResponseHandler;

namespace Medium.BL.Interfaces.Services
{
    public interface IEmailService
    {
        Task<ApiResponse<bool>> SendEmail(EmailSendRequest request);
        Task<ApiResponse<string>> SendResetPasswordCode(SendResetPasswordRequest request);
        Task<ApiResponse<string>> ConfirmResetPassword(ConfirmResetPasswordRequest request);
        Task<ApiResponse<string>> ResetPassword(ResetPasswordRequest request);

    }
    
    public record EmailSendRequest(string Email, string Message, string? Reason);
    public record SendResetPasswordRequest(string Email);
    public record ConfirmResetPasswordRequest(string Code, string Email);
    public record ResetPasswordRequest(string Email, string Password, string ConfirmPassword);


}