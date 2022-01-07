using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Abusements
{
    public class AddPostAbusementModel
    {
        [Required(ErrorMessage = "Należy dostarczyć uzasadnienie")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Należy podać id postu")]
        public Guid PostId { get; set; }
    }
}
