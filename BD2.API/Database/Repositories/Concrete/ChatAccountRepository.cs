using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> UpdateAsync(ChatAccount entity)
        {
            var found = _ctx.ChatAccounts.Where(x => x.AccountId == entity.AccountId && x.ChatId == entity.ChatId); 

            if (found == null)
            {
                return false;
            }

            _ctx.Entry(found.First()).CurrentValues.SetValues(entity);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<int> UnseenEntriesCount(Guid userId)
        {
            var lastViewDate = _ctx.ChatAccounts.Where(x => x.AccountId == userId).First().LastViewDate;
            var userChats = await _ctx.Chats
                .Where(x => x.Members.Any(y => y.AccountId == userId))
                .ToListAsync();
            var entries = await _ctx.ChatEntries
                .Where(x => userChats.Select(x => x.Id).Contains(x.ChatId) && x.PostDate > lastViewDate)
                .ToListAsync();

            return entries.Count;
        }
    }
}
