using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using BD2.API.Database.ViewEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class PostReactionsRepository : IPostReactionsRepository
    {
        private readonly AppDbContext _ctx;

        public PostReactionsRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<PostReaction> FindAsync(Guid postId, Guid userId)
        {
            return await _ctx.PostReactions
                .Where(x => x.PostId == postId && x.AccountId == userId).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(PostReaction entity)
        {
            if (entity.PostId == default || entity.AccountId == default)
            {
                return false;
            }

            _ctx.PostReactions.Add(entity);

            try
            {
                return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> AddRangeAsync(IEnumerable<PostReaction> entities)
        {
            if (entities.Any(x => x.PostId == default || x.AccountId == default))
            {
                return 0;
            }
            foreach (var entity in entities)
            {
                _ctx.PostReactions.Add(entity);
            }
            return await _ctx.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(PostReaction entity)
        {
            var found = await FindAsync(entity.PostId, entity.AccountId);

            if (found == null)
            {
                return false;
            }

            _ctx.Entry(found).CurrentValues.SetValues(entity);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public async Task<bool> DeleteAsync(Guid postId, Guid userId)
        {
            var found = await _ctx.PostReactions.FindAsync(postId, userId);

            if (found == null)
            {
                return false;
            }

            _ctx.PostReactions.Remove(found);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public IQueryable<PostReaction> All()
        {
            return _ctx.PostReactions.AsQueryable();
        }

        public IQueryable<PostReaction> AllByPost(Guid postId)
        {
            return _ctx.PostReactions
                .Where(x => x.PostId == postId)
                .Include(x => x.Account);
        }

        public IQueryable<PostReactionNotification> GetNotifications(Guid userId)
        {
            return _ctx.PostReactionNotifications.FromSqlRaw($"exec GetReactionsNotifications {userId}");
        }
    }
}
