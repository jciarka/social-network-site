using BD2.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class AuthorsRepository
    {
        private readonly AppDbContext _ctx;

        public AuthorsRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Author> FindAsync(Guid id)
        {
            return await _ctx.Authors.FindAsync(id);
        }

        public async Task<bool> AddAsync(Author author)
        {
            if (author.Id != default)
            {
                return false;
            }

            _ctx.Authors.Add(author);
            try
            {
                return (await _ctx.SaveChangesAsync()) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddRangeAsync(IEnumerable<Author> authors)
        {
            if (authors.Any(x => x.Id != default))
            {
                return false;
            }

            foreach (var author in authors)
            {
                _ctx.Authors.Add(author);
            }
            return (await _ctx.SaveChangesAsync()) > 0;
        }



    }
}
