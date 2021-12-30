using BD2.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Interfaces
{
    public interface IChatEntryRepository : ICrudRepository<ChatEntry>
    {
        public Task<IEnumerable<ChatEntry>> FindChatsEntries(Guid entryId);
    }
}
