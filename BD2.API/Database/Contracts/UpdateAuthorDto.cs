using BD2.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Contracts
{
    public class UpdateAuthorDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Surname { get; init; }
        public ICollection<Book> Books { get; set; }
    }
}
