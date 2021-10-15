using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Auth
{
    public class LoginResponse : ResponseBase
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
