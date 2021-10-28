using BD2.API.Database.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Interfaces
{
    public interface IPostsRepository : ICrudRepository<Post>
    {
        public Task<IEnumerable<Post>> FindUserPosts(Guid userId, Guid? watcherId = null);
        public Task<IEnumerable<Post>> FindGroupPosts(Guid groupId, Guid? watcherId = null);
        public Task<IEnumerable<Post>> FindFriendsPosts(Guid watcherId);

        // images
        public Task<bool> AddImageAsync(Guid postId, IFormFile image);
        public Task<IEnumerable<Guid>> FindImagesAsync(Guid postId);
        public Task<bool> DeleteImageAsync(Guid postId, Guid imageId);
    }
}
