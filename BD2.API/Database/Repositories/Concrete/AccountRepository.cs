using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Repositories.Concrete
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _ctx;

        public AccountRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Account> FindAsync(Guid id)
        {
            return await _ctx.Accounts.FindAsync(id);
        }


        public IQueryable<Account> All()
        {
            return _ctx.Users.AsQueryable();
        }
        public IQueryable<Account> AllByPost(string namePhrase)
        {
            return _ctx.Users.Where(x => x.Firstname.Contains(namePhrase) || x.Lastname.Contains(namePhrase) || (x.Firstname + " " + x.Lastname).Contains(namePhrase));
        }
    }
}
