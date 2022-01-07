using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Notifications
{
    public class PostCommentNotification
    {
        public Guid PostId { get; set; }
        public Guid? GroupId { get; set; }
        public string PostTitle { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
