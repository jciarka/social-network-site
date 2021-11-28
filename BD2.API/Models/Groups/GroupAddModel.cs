using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Groups
{
    public class GroupAddModel
    {
        [Required(ErrorMessage = "Nazwa grupy musi zosatać podana")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Tematyka grupy musi zosatać podana")]
        public string GroupTopic { get; set; }
    }
}
