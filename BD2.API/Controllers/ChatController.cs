using BD2.API.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var chat = await _repo.FindAsync(id);

            if (chat == null)
            {
                return NotFound(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie znaleziono czatu o podanym Id" }
                });
            }

            return Ok(new
            {
                Model = chat,
                Success = true,
            });
        }
    }
}
