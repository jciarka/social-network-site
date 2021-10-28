using BD2.API.Database.Entities;
using BD2.API.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Posts
{
    public class PostCommentModel
    {
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
        public UserModel Account { get; set; }
    }
}
