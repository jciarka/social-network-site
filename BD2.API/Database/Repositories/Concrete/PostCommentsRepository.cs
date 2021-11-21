﻿using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class PostCommentsRepository : IPostCommentsRepository
    {

        private readonly AppDbContext _ctx;

        public PostCommentsRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<PostComment> FindAsync(Guid postId, Guid userId)
        {
            return await _ctx.PostComments
                .Where(x => x.PostId == postId && x.AccountId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(PostComment entity)
        {
            if (entity.PostId == default || entity.AccountId == default)
            {
                return false;
            }

            _ctx.PostComments.Add(entity);

            try
            {
                return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> AddRangeAsync(IEnumerable<PostComment> entities)
        {
            if (entities.Any(x => x.PostId == default || x.AccountId == default))
            {
                return 0;
            }
            foreach (var entity in entities)
            {
                _ctx.PostComments.Add(entity);
            }
            return await _ctx.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(PostComment entity)
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
            var found = await _ctx.PostComments.FindAsync(postId, userId);

            if (found == null)
            {
                return false;
            }

            _ctx.PostComments.Remove(found);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public IQueryable<PostComment> All()
        {
            return _ctx.PostComments.AsQueryable();
        }

        public IQueryable<PostComment> AllByPost(Guid postId)
        {
            return _ctx.PostComments
                .Where(x => x.PostId == postId)
                .Include(x => x.Account);
        }

    }
}