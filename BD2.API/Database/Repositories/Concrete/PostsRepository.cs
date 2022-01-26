using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class PostsRepository : IPostsRepository
    {
        private readonly AppDbContext _ctx;
        private readonly IImagesRepository _irepo;

        public PostsRepository(IImagesRepository irepo, AppDbContext ctx)
        {
            _irepo = irepo;
            _ctx = ctx;
        }

        public PostsRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> AddAsync(Post entity)
        {
            if (entity.Id != default)
            {
                return false;
            }

            _ctx.Posts.Add(entity);

            try
            {
                return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> AddRangeAsync(IEnumerable<Post> entities)
        {
            if (entities.Any(x => x.Id != default))
            {
                return 0;
            }
            foreach (var entity in entities)
            {
                _ctx.Posts.Add(entity);
            }
            return await _ctx.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Post entity)
        {
            var found = await _ctx.Posts.FindAsync(entity.Id);

            if (found == null)
            {
                return false;
            }

            _ctx.Entry(found).CurrentValues.SetValues(entity);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var post = await _ctx.Posts
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            var imagesIds = await _ctx.PostImages
                .Where(x => x.PostId == id)
                .Select(x => x.ImageId)
                .ToListAsync();

            foreach (var imageId in imagesIds)
            {
                await _irepo.DeleteAsync(imageId);
            }

            _ctx.Posts.Remove(post);

            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public IQueryable<Post> All()
        {
            return _ctx.Posts.AsQueryable();
        }

        public async Task<Guid> AddImageAsync(Guid postId, IFormFile file)
        {
            if (!file.ContentType.StartsWith("image/")) return Guid.Empty;

            Image image;
            using (var transaction = _ctx.Database.BeginTransaction())
            using (MemoryStream memStream = new MemoryStream())
            {
                await file.CopyToAsync(memStream);
                image = new Image()
                {
                    Binary = memStream.ToArray(),
                    MimeType = file.ContentType
                };

                _ctx.Images.Add(image);
                var success = (await _ctx.SaveChangesAsync()) > 0 ? true : false;

                if (!success)
                {
                    transaction.Rollback();
                    return Guid.Empty;
                }

                var postImage = new PostImage()
                {
                    ImageId = image.Id,
                    PostId = postId
                };

                _ctx.PostImages.Add(postImage);

                success = (await _ctx.SaveChangesAsync()) > 0 ? true : false;

                if (!success)
                {
                    transaction.Rollback();
                    return Guid.Empty;
                }

                transaction.Commit();
            }
            return image.Id;
        }

        public async Task<bool> DeleteImageAsync(Guid postId, Guid imageId)
        {
            var postImage = await _ctx
                .PostImages
                .Where(x => x.ImageId == imageId)
                .FirstOrDefaultAsync();

            if (postImage == null) return false;

            var image = await _ctx
                .Images
                .Where(x => x.Id == imageId)
                .FirstOrDefaultAsync();

            if (image == null) return false;

            _ctx.PostImages.Remove(postImage);
            _ctx.Images.Remove(image);

            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public async Task<IEnumerable<Guid>> FindImagesAsync(Guid postId)
        {
            return await _ctx.PostImages
                .Where(x => x.PostId == postId)
                .Select(x => x.ImageId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> FindUserPosts(Guid userId, Guid? watcherId = null)
        {
            var posts = await _ctx.Posts
                .Where(x => x.OwnerId == userId)
                .Include(x => x.Images)
                .Include(x => x.Owner)
                .Include(x => x.Reactions)
                .Include(x => x.Abusements)
                .OrderByDescending(x => x.PostDate)
                .Where(x => x.Abusements.All(x => x.Status != true))
                .ToListAsync();

            await track(posts, watcherId);

            return posts;
        }

        public async Task<IEnumerable<Post>> FindGroupPosts(Guid groupId, Guid? watcherId = null)
        {
            var posts = await _ctx.Posts
                .Where(x => x.GroupId == groupId)
                .Include(x => x.Images)
                .Include(x => x.Owner)
                .Include(x => x.Reactions)
                .Include(x => x.Abusements)
                .Where(x => x.Abusements.All(x => x.Status != true))
                .ToListAsync();

            await track(posts, watcherId);

            return posts;
        }

        public async Task<IEnumerable<Post>> FindFriendsPosts(Guid watcherId)
        {
            var posts = await _ctx.Posts
             .Include(x => x.Images)
             .Include(x => x.Owner)
             .ThenInclude(x => x.Friendships)
             .Include(x => x.Reactions)
             .Include(x => x.Abusements)
             .Where(x => x.Abusements.All(x => x.Status != true))
             .Where(x =>
                x.Owner != null && x.Owner.Friendships != null && (
                x.Owner.Friendships.Select(x => x.FirstFriendId).Contains(watcherId) ||
                x.Owner.Friendships.Select(x => x.SecondFriendId).Contains(watcherId))
             )
             .ToListAsync();

            await track(posts, watcherId);

            return posts;
        }

        public async Task<Post> FindAsync(Guid postId)
        {
            return await _ctx.Posts
                .Where(x => x.Id == postId)
                .Include(x => x.Images)
                .Include(x => x.Owner)
                .Include(x => x.Group)
                .Include(x => x.Reactions)
                    .ThenInclude(x => x.Account)
                .Include(x => x.Comments)
                    .ThenInclude(x => x.Account)
                .Include(x => x.Views)
                    .ThenInclude(x => x.Account)
                .Include(x => x.Abusements)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PostView>> GetPostViews(Guid postId)
        {
            var postViews = await _ctx.PostViews.Where(x => x.PostId == postId).ToListAsync();
            return postViews;
        }

        private async Task track(IEnumerable<Post> posts, Guid? watcherId)
        {
            foreach (var post in posts)
            {
                if (watcherId == post.OwnerId)
                {
                    post.LastOwnerViewDate = DateTime.Now;
                }
                else
                {
                    post.ViewsCounts += 1;
                }
            }

            if (watcherId != null)
            {
                var newViewsQuery = posts
                    .Select(x => x.Id)
                    .Except(
                        await _ctx.PostViews
                            .Where(x => x.AccountId == watcherId)
                            .Select(x => x.PostId)
                            .ToListAsync()
                        )
                    .Select(
                        x => new PostView() { PostId = x, AccountId = (Guid)watcherId });

                _ctx.PostViews.AddRange(newViewsQuery);
            }

            await _ctx.SaveChangesAsync();
        }
    }
}
