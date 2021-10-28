using BD2.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Interfaces
{
    public interface IImagesRepository
    {
        public Task<Image> FindAsync(Guid id);
        public Task<bool> DeleteAsync(Guid id);
    }
}
