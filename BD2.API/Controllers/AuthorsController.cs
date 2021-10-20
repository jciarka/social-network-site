using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Controllers
{
    [Route("[controller]")]
    public class AuthorsController : ExtendedControllerBase
    {
        private readonly IAuthorsRepository _repo;

        public AuthorsController(IAuthorsRepository repo)
        {
            _repo = repo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.All().ToListAsync();

            if (items == null)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie znaleziono autorów" }
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>) null,
                data = items
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await _repo.FindAsync(id);

            if (item == null)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie znaleziono podanego autora" }
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>) null,
                data = item
            });
        }

    }
}
