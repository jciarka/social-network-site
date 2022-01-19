using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _ctx;
        public ChatRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> AddAsync(Chat entity)
        {
            _ctx.Chats.Add(entity);

            try
            {
                return (await _ctx.SaveChangesAsync()) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddRangeAsync(IEnumerable<Chat> entities)
        {
            foreach (var entity in entities)
            {
                _ctx.Chats.Add(entity);
            }
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Chat entity)
        {
            var found = await _ctx.Chats.FindAsync(entity.Id);

            if (found == null)
            {
                return false;
            }

            _ctx.Entry(found).CurrentValues.SetValues(entity);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<Chat> FindAsync(Guid id)
        {
            return await _ctx.Chats.FindAsync(id);
        }

        Task<int> ICrudRepository<Chat>.AddRangeAsync(IEnumerable<Chat> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _ctx.Chats.FindAsync(id);

            if (found == null)
            {
                return false;
            }

            var chatAccounts = _ctx.ChatAccounts.Where(x => x.ChatId == id);

            foreach (var chatAccount in chatAccounts)
            {
                _ctx.ChatAccounts.Remove(chatAccount);
            }

            _ctx.Chats.Remove(found);
            return (await _ctx.SaveChangesAsync()) > 0;
        }

        public IQueryable<Chat> All()
        {
            return _ctx.Chats.AsQueryable();
        }

        public async Task<IEnumerable<Chat>> FindUserChats(Guid userId, Guid? watcherId = null)
        {
            var chats = await _ctx.Chats
                .Where(x => x.Members.Any(y => y.AccountId == userId))
                .ToListAsync();

            return chats;
        }

    }

}

