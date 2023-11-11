namespace Medium.BL.Features.Accounts.Response
{
    public record GetAllUsersWithRolesResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public List<string> Roles { get; set; }
    }
}
