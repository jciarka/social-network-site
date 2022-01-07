using BD2.API.Database.Entities;
using BD2.API.Database.ViewEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Interfaces
{
    public interface IPostReactionsRepository
    {
        public Task<PostReaction> FindAsync(Guid PostId, Guid UserId);

        public Task<bool> AddAsync(PostReaction entity);
        public Task<int> AddRangeAsync(IEnumerable<PostReaction> entities);
        public Task<bool> UpdateAsync(PostReaction entity);
        public Task<bool> DeleteAsync(Guid PostId, Guid UserId);
        public IQueryable<PostReaction> All();
        public IQueryable<PostReaction> AllByPost(Guid PostId);

        // Notifications
        public IQueryable<PostReactionNotification> GetNotifications(Guid UserId);
    }
}
