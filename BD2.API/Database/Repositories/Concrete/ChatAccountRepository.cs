using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class ChatAccountRepository : IChatAccountRepository
    {
        private readonly AppDbContext _ctx;
        public ChatAccountRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> AddAsync(ChatAccount entity)
        {
            _ctx.ChatAccounts.Add(entity);

            try
            {
                return (await _ctx.SaveChangesAsync()) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<int> AddRangeAsync(IEnumerable<ChatAccount> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ChatAccount> All()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Guid chatId)
        {
            var found = await _ctx.ChatAccounts.FindAsync(chatId);

            if (found == null)
            {
                return false;
            }

            _ctx.ChatAccounts.Remove(found);
            return (await _ctx.SaveChangesAsync()) > 0;
        }

        public async Task<ChatAccount> FindAsync(Guid id)
        {
            return await _ctx.ChatAccounts.FindAsync(id);
        }

        public Task<bool> UpdateAsync(ChatAccount entity)
        {
            throw new NotImplementedException();
        }
    }
}
