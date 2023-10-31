namespace Medium.BL.Features.Accounts.Request
{
    public record UpdateRoleToUserRequest(string userId, List<string> newRoles);

}
