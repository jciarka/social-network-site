using AutoMapper;
using BD2.API.Database.Repositories.Interfaces;
using BD2.API.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : ExtendedControllerBase
    {
        private readonly IAccountRepository _repo;
        private readonly IMapper _mapper;

        public AccountsController(IAccountRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("findByName/{namePhrase}")]
        public async Task<IActionResult> ListByNamePhrase(string namePhrase)
        {
            var found = await _repo.AllByPost(namePhrase).Take(10).ToListAsync();
            var data = found.Select(x => _mapper.Map<UserModel>(x));

            return Ok(new { success = true, data });
        }


    }
}
