using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Interfaces
{
    public interface ITopicsRepository
    {
        public IQueryable<string> All();
    }
}
