using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly AppDbContext _ctx;

        public ImagesRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Image> FindAsync(Guid id)
        {
            return await _ctx.Images.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = _ctx.Images.Where(x => x.Id == id);
            _ctx.RemoveRange(found);

            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
