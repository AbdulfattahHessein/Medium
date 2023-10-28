using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.Core.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public Publisher Publisher { get; set; }
    }
}
