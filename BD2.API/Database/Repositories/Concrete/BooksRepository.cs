using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly AppDbContext _ctx;

        public BooksRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Book> FindAsync(Guid id)
        {
            return await _ctx.Books.FindAsync(id);
        }

        public async Task<bool> AddAsync(Book entity)
        {
            if (entity.Id != default)
            {
                return false;
            }

            _ctx.Books.Add(entity);
            try
            {
                return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }


        public async Task<int> AddRangeAsync(IEnumerable<Book> entities)
        {
            if (entities.Any(x => x.Id != default))
            {
                return 0;
            }
            foreach (var entity in entities)
            {
                _ctx.Books.Add(entity);
            }
            return await _ctx.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Book entity)
        {
            var found = await _ctx.Books.FindAsync(entity.Id);

            if (found == null)
            {
                return false;
            }

            _ctx.Entry(found).CurrentValues.SetValues(entity);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _ctx.Books.FindAsync(id);

            if (found == null)
            {
                return false;
            }

            _ctx.Books.Remove(found);
            return (await _ctx.SaveChangesAsync()) > 0 ? true : false;
        }

        public IQueryable<Book> All()
        {
            return _ctx.Books.AsQueryable();
        }

        private async Task<IEnumerable<Book>> getTest(Guid id)
        {
            var query = All()
                .Include(x => x.Author)
                .Where(x => x.Title.Contains("abc"))
                .OrderBy(x => x.Title)
                .ThenBy(x => x.Id);

            return await query.ToListAsync();
        }

        Task<IEnumerable<Book>> IBooksRepository.getTest(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
