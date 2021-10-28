using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories
{
    public interface ICrudRepository<T> where T : class, new()
    {
        public Task<T> FindAsync(Guid id);
        public Task<bool> AddAsync(T entity);
        public Task<int> AddRangeAsync(IEnumerable<T> entities);
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(Guid id);
        public IQueryable<T> All();
    }
}
