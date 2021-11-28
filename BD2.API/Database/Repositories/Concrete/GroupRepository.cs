using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class GroupRepository : IGroupRepository
    {

        private readonly AppDbContext _ctx;

        public GroupRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Group> FindAsync(Guid id)
        {
            return await _ctx.Groups.FindAsync(id);
        }

        public async Task<bool> AddAsync(Group entity)
        {
            if (entity.Id != default)
            {
                return false;
            }

            _ctx.Groups.Add(entity);
            try
            {
                return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public async Task<int> AddRangeAsync(IEnumerable<Group> entities)
        {
            if (entities.Any(x => x.Id != default))
            {
                return 0;
            }
            foreach (var entity in entities)
            {
                _ctx.Groups.Add(entity);
            }
            return await _ctx.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Group entity)
        {
            var found = await _ctx.Groups.FindAsync(entity.Id);

            if (found == null)
            {
                return false;
            }

            _ctx.Entry(found).CurrentValues.SetValues(entity);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _ctx.Groups.FindAsync(id);

            if (found == null)
            {
                return false;
            }

            _ctx.Groups.Remove(found);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public IQueryable<Group> All()
        {
            return _ctx.Groups.AsQueryable();
        }
    }
}
