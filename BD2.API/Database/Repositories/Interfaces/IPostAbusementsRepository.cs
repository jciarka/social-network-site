using BD2.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Interfaces
{
    public interface IPostAbusementsRepository
    {
        public Task<PostAbusement> FindAsync(Guid PostId, Guid UserId);

        public Task<bool> AddAsync(PostAbusement entity);
        public Task<int> AddRangeAsync(IEnumerable<PostAbusement> entities);
        public Task<bool> UpdateAsync(PostAbusement entity);
        public Task<bool> DeleteAsync(Guid PostId, Guid UserId);
        public IQueryable<PostAbusement> All();
        public IQueryable<PostAbusement> AllByPost(Guid PostId);
    }
}
