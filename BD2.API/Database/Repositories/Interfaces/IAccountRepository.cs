using BD2.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public Task<Account> FindAsync(Guid id);
        public IQueryable<Account> All();
        public IQueryable<Account> AllByPost(string namePhrase);
        public Task<Account> FindChatMember(Guid chatId, Guid userId);
    }
}
