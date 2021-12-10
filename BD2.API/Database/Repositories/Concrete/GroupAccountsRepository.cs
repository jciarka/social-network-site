using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class GroupAccountsRepository : IGroupAccountsRepository
    {
        private readonly AppDbContext _ctx;
        private readonly IConfiguration _configuration;

        public GroupAccountsRepository(AppDbContext ctx, IConfiguration configuration)
        {
            _ctx = ctx;
            _configuration = configuration;
        }

        public async Task<bool> AddAsync(GroupAccount entity)
        {
            if (entity.GroupId == default || entity.AccountId == default)
            {
                return false;
            }

            _ctx.GroupAccounts.Add(entity);

            try
            {
                return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IQueryable<GroupAccount> All()
        {
            return _ctx.GroupAccounts.AsQueryable();
        }

        public async Task<int> AvailableSlotsCount(Guid groupId)
        {
            var found = await _ctx.Groups.Include(x => x.Subscription).ThenInclude(x => x.Packet).FirstOrDefaultAsync();
            var count = await _ctx.GroupAccounts.Where(x => x.GroupId == groupId).CountAsync();
            if (found == null)
            {
                return -1;
            } 
            else if(found.Subscription == null)
            {
                return _configuration.GetValue<int>("AppBussinesDefault:Groups:DefaultPeopleLimit");
            }
            else if(found.Subscription.Packet == null)
            {
                return -1;
            } 
            else
            {
                return (found.Subscription.Packet.PeopleLimit ?? int.MaxValue) - count;
            }
        }

        public async Task<bool> DeleteAsync(Guid accountId, Guid groupId)
        {
            var found = await FindAsync(accountId, groupId);

            if (found == null)
            {
                return false;
            }

            _ctx.GroupAccounts.Remove(found);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public async Task<GroupAccount> FindAsync(Guid accountId, Guid groupId)
        {
            return await _ctx.GroupAccounts
                .Where(x => x.AccountId == accountId && x.GroupId == groupId)
                .Include(x => x.Account)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GroupAccount>> FindByGroupAsync(Guid groupId)
        {
            return await _ctx.GroupAccounts
                .Where(x => x.GroupId == groupId)
                .Include(x => x.Account)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(GroupAccount entity)
        {
            var found = await FindAsync(entity.AccountId, entity.GroupId);

            if (found == null)
            {
                return false;
            }

            _ctx.Entry(found).CurrentValues.SetValues(entity);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }
    }
}
