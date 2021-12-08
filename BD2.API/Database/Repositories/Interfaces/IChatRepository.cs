using BD2.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Interfaces
{
    public interface IChatRepository : ICrudRepository<Chat>
    {
        //public Task<bool> AddAsync(Chat entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> AddRangeAsync(IEnumerable<Chat> entities)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<Chat> All()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> DeleteAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Chat> FindAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> UpdateAsync(Chat entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
