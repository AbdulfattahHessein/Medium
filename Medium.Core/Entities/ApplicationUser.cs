using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;

namespace Medium.Core.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public Publisher Publisher { get; set; }
        [EncryptColumn]
        public string? Code { get; set; }
    }
}
