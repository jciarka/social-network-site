using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class ChatEntryRepository : IChatEntryRepository
    {
        private readonly AppDbContext _ctx;
        public ChatEntryRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> AddAsync(ChatEntry entity)
        {
            _ctx.ChatEntries.Add(entity);

            try
            {
                return (await _ctx.SaveChangesAsync()) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> AddRangeAsync(IEnumerable<ChatEntry> entities)
        {
            foreach (var entity in entities)
            {
                _ctx.ChatEntries.Add(entity);
            }
            return (await _ctx.SaveChangesAsync() > 0).CompareTo(1);
        }

        public IQueryable<ChatEntry> All()
        {
            return _ctx.ChatEntries.AsQueryable();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _ctx.ChatEntries.FindAsync(id);

            if (found == null)
            {
                return false;
            }

            _ctx.ChatEntries.Remove(found);
            return (await _ctx.SaveChangesAsync()) > 0;
        }

        public async Task<ChatEntry> FindAsync(Guid id)
        {
            return await _ctx.ChatEntries.FindAsync(id);
        }

        public async Task<IEnumerable<ChatEntry>> FindChatsEntries(Guid chatId)
        {
            var entries = await _ctx.ChatEntries
                .Where(x => x.ChatId == chatId)
                .ToListAsync();

            return entries;
        }

        public async Task<bool> UpdateAsync(ChatEntry entity)
        {
            var found = await _ctx.ChatEntries.FindAsync(entity.Id);

            if (found == null)
            {
                return false;
            }

            _ctx.Entry(found).CurrentValues.SetValues(entity);
            return await _ctx.SaveChangesAsync() > 0;
        }

    }
}
