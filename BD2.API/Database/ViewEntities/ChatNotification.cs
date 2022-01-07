using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Notifications
{
    public class ChatNotification
    {
        public class PostCommentNotification
        {
            public Guid ChatId { get; set; }
            public string ChatName { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Text { get; set; }
            public DateTime PostDate { get; set; }
        }
    }
}
