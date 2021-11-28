using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class PacketsRepository : IPacketsRepository
    {
        private readonly AppDbContext _ctx;

        public PacketsRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Packet> FindAsync(Guid id)
        {
            return await _ctx.Packets.FindAsync(id);
        }

        public async Task<bool> AddAsync(Packet entity)
        {
            if (entity.Id != default)
            {
                return false;
            }

            _ctx.Packets.Add(entity);
            try
            {
                return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> AddRangeAsync(IEnumerable<Packet> entities)
        {
            if (entities.Any(x => x.Id != default))
            {
                return 0;
            }
            foreach (var entity in entities)
            {
                _ctx.Packets.Add(entity);
            }
            return await _ctx.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Packet entity)
        {
            var found = await _ctx.Packets.FindAsync(entity.Id);

            if (found == null)
            {
                return false;
            }

            _ctx.Entry(found).CurrentValues.SetValues(entity);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _ctx.Packets.FindAsync(id);

            if (found == null)
            {
                return false;
            }

            _ctx.Packets.Remove(found);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public IQueryable<Packet> All()
        {
            return _ctx.Packets.AsQueryable();
        }
    }
}
