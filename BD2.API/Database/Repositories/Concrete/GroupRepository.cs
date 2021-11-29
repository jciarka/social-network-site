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

        public async Task<bool> AddAsync(Group entity, Guid ownerId)
        {
            if (entity.Id != default || ownerId == default)
            {
                return false;
            }

            using var transaction = _ctx.Database.BeginTransaction();
            {
                try
                {
                    _ctx.Groups.Add(entity);

                    if ((await _ctx.SaveChangesAsync()) <= 0)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }

                    var groupAccount = new GroupAccount()
                    {
                        GroupId = entity.Id,
                        AccountId = ownerId,
                        IsAdmin = true,
                    };

                    _ctx.GroupAccounts.Add(groupAccount);

                    if ((await _ctx.SaveChangesAsync()) <= 0)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                await transaction.CommitAsync();
            }
            return true;
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
