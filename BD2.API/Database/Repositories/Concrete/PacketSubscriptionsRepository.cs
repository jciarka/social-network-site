using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class PacketSubscriptionsRepository : IPacketSubscriptionsRepository
    {
        private readonly AppDbContext _ctx;

        public PacketSubscriptionsRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<PacketSubscription> FindAsync(Guid id)
        {
            return await _ctx.PacketSubscriptions.FindAsync(id);
        }

        public async Task<bool> AddAsync(PacketSubscription entity)
        {
            if (entity.Id != default)
            {
                return false;
            }

            _ctx.PacketSubscriptions.Add(entity);
            try
            {
                return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> AddRangeAsync(IEnumerable<PacketSubscription> entities)
        {
            if (entities.Any(x => x.Id != default))
            {
                return 0;
            }
            foreach (var entity in entities)
            {
                _ctx.PacketSubscriptions.Add(entity);
            }
            return await _ctx.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(PacketSubscription entity)
        {
            var found = await _ctx.PacketSubscriptions.FindAsync(entity.Id);

            if (found == null)
            {
                return false;
            }

            _ctx.Entry(found).CurrentValues.SetValues(entity);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _ctx.PacketSubscriptions.FindAsync(id);

            if (found == null)
            {
                return false;
            }

            _ctx.PacketSubscriptions.Remove(found);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public IQueryable<PacketSubscription> All()
        {
            return _ctx.PacketSubscriptions.AsQueryable();
        }

        public async Task<int> AvailableSlotsCount(Guid subcriptionId)
        {
            var sub = await All()
                .Where(x => x.Id == subcriptionId)
                .Include(x => x.Groups)
                .Include(x => x.Packet)
                .Select(x => new
                {
                    GroupsCount = x.Groups.Count(),
                    GroupsLimit = x.Packet.GroupsLimit,
                })
                .FirstOrDefaultAsync();

            if (sub == null) return 0;

            return sub.GroupsLimit - sub.GroupsCount;
        }
    }
}
