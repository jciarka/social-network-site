using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class ChatRepository
    {
        private readonly AppDbContext _ctx;
        private readonly IChatRepository _irepo;
        public ChatRepository(IChatRepository irepo, AppDbContext ctx)
        {
            _irepo = irepo;
            _ctx = ctx;
        }

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
    }
}
