using BD2.API.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class TopicsRepository : ITopicsRepository
    {
        private readonly AppDbContext _ctx;

        public TopicsRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<string> All()
        {
            return _ctx.GroupTopics.Select(x => x.Topic);
        }
    }
}
