using BD2.API.Database.Entities;
using BD2.API.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Posts
{
    public class PostReactionModel
    {
        public ReactionType Type { get; set; }
        public DateTime ReactionDate { get; set; }
        public UserModel Account { get; set; }
    }
}
