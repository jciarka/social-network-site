using BD2.API.Database.Dtos.Chat;
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
    public class ChatController : ExtendedControllerBase
    {
        private readonly IChatRepository _repo;

        public ChatController(IChatRepository repo)
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
                    Errors = new List<string> { "Nie znaleziono czatu o podanym Id" }
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
            var chats = await _repo.All().ToListAsync();

            if (chats == null)
            {
                return BadRequest( new
                {
                    Success = false,
                    Errors = new List<string> { "Nie znaleziono czatów" }
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
                data = chats
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(CreateChatDto createChatDto)
        {
            Chat chat = new()
            {
                Id = new Guid(),

            }
            var result = await _repo.AddAsync(chat);
        }
    }
}
