using BD2.API.Database.Entities;
using BD2.API.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Interfaces
{
    public interface IAuthorsRepository : ICrudRepository<Author>
    {
        // pass
    }
}
