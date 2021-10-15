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
    public class BooksController : ExtendedControllerBase
    {
        private readonly IBooksRepository _repo;

        public BooksController(IBooksRepository repo)
        {
            _repo = repo;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var found = await _repo.FindAsync(id);

            if (found == null)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie znaleziono książki o podanym Id" }
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
                data = found
            });
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var books = await _repo.All().ToListAsync();

            if (books == null)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie znaleziono książek" }
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
                data = books
            });
        }

        // konstrukcja new { dana1 = watość1 , dana2 = wartość2 } to tzw. obiekt typu anonimowego - fajnie nadaje się żeby wygenerować odpowiedź 
        // z kontrolera jeśli nie che nam się tworzyć oddzielnej klasy odpowiedzi dla każdej akcji

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(Book book) // dodawanie nowych encji
        {
            var result = await _repo.AddAsync(book); // przed dodaniem obiek book nie ma id (wartość domyślna 00000-00....000-000 )
                                                     // pod dodaniu obiekt ma już id i jest śledzony przez entity framework
            if (!result)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało się dodać książki" }
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
                data = book
            });
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(Guid id, Book book) // dodawanie nowych encji
        {
            if (book.Id == default) // default to wartość domyślna typów nienulowalnych np. int, guid, Datetime oraz null w pryzpadku typów nulowalnych np. klas
            {
                book.Id = id;
            }

            if (book.Id != id)
            {
                return BadRequest(new 
                {
                    Success = false,
                    Errors = new List<string> { "Id ksiązki nie zgodny z id operacji" },
                });
            }

            var result = await _repo.UpdateAsync(book); 
                                                  
            if (result == false)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało sie zmodyfikować ksiązki" },
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
                data = book
            });
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id) // dodawanie nowych encji
        {
            var result = await _repo.DeleteAsync(id); 
                                                       
            if (result == false)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało sie zmodyfikować ksiązki" },
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
            });
        }
    }
}
