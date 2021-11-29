using BD2.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Interfaces
{
    public interface IGroupRepository 
    {
        public Task<Group> FindAsync(Guid id);
        public Task<bool> AddAsync(Group entity, Guid ownerId);
        public Task<bool> UpdateAsync(Group entity);
        public Task<bool> DeleteAsync(Guid id);
        public IQueryable<Group> All();
    }
}
