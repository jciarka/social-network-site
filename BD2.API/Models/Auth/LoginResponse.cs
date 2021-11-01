using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Auth
{
    public class LoginResponse : ResponseBase
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
