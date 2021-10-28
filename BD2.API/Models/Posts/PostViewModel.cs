using BD2.API.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Posts
{
    public class PostViewModel
    {
        public DateTime ViewDate { get; set; }
        public UserModel Account { get; set; }
    }
}
