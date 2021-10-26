using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class Account : IdentityUser<Guid>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
