using BD2.API.Database.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BD2.API.Database.Contracts
{
    public record CreateAuthorDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Surname { get; init; }
        public ICollection<Book> Books { get; set; }
    }
}
