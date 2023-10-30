namespace Medium.BL.Features.Accounts.Request
{
    public record RegisterRequest(string UserName, string Password, string ConfirmPassword, string Email);
}
