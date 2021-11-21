using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Posts
{
    public class PostCommentAddModel
    {
        [Required(ErrorMessage = "Komentarz nie moze być pusty")]
        public string Text { get; set; }
    }
}
