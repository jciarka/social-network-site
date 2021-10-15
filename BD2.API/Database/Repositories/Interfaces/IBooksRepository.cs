using BD2.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Interfaces
{
    public interface IBooksRepository : ICrudRepository<Book> // Dziedziczymy metody interfejsu bazowego
    {
        // Tu możemy dodać definicje nietypowych metod od których chcemy mieć dostęp w kontrolerach 
        public Task<IEnumerable<Book>> getTest(Guid id); 
    }
}
