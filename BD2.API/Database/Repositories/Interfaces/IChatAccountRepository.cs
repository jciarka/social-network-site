using BD2.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Interfaces
{
    public interface IChatAccountRepository : ICrudRepository<ChatAccount>
    {
        public Task<int> UnseenEntriesCount(Guid userId);
    }
}
