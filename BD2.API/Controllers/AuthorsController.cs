using BD2.API.Database.Contracts;
using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

            if (items is null)
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

            if (item is null)
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(CreateAuthorDto authorDto)
        {
            Author author = new()
            {
                Id = Guid.NewGuid(),
                Name = authorDto.Name,
                Surname = authorDto.Surname,
                Books = authorDto.Books
            };

            var result = await _repo.AddAsync(author);

            if (!result)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało się dodać obiektu autora" }
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
                data = author
            });
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateAuthorDto authorDto)
        {
            var existingAuthor = await _repo.FindAsync(id);

            if (existingAuthor is null)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie ma autora o podanym id" }
                });
            }

            Author updatedAuthor = new()
            {
                Id = existingAuthor.Id,
                Name = authorDto.Name,
                Surname = authorDto.Surname,
                Books = authorDto.Books
            };

            var result = await _repo.UpdateAsync(updatedAuthor);
            
            if (!result)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało się zmodyfikować obiektu autora" }
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
                data = updatedAuthor
            });
        }

    }
}
