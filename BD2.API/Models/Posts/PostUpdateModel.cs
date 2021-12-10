using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Posts
{
    public class PostUpdateModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid? GroupId { get; set; }
    }
}
