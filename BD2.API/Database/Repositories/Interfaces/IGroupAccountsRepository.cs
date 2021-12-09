using BD2.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Interfaces
{
    public interface IGroupAccountsRepository
    {
        public Task<GroupAccount> FindAsync(Guid AccountId, Guid GroupId);
        public Task<IEnumerable<GroupAccount>> FindByGroupAsync(Guid groupId);
        public Task<bool> AddAsync(GroupAccount entity);
        public Task<bool> UpdateAsync(GroupAccount entity);
        public Task<bool> DeleteAsync(Guid AccountId, Guid GroupId);
        public IQueryable<GroupAccount> All();
        Task<int> AvailableSlotsCount(Guid subcriptionId);
    }
}
