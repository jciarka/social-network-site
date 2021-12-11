using BD2.API.Models.Chat
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
        private readonly IChatAccountRepository _arepo;

        public ChatController(IChatRepository repo, IChatAccountRepository arepo)
        {
            _repo = repo;
            _arepo = arepo;
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

            // TO DO: model 

            return Ok(new
            {
                Model = chat,
                Success = true
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("list/user/{id}")]
        public async Task<IActionResult> UserChats(Guid id)
        {
            var chats = await _repo.FindUserChats(id);

            if (chats == null)
            {
                return NotFound(new
                {
                    Success = false,
                    Errors = new List<string> { $"Nie znaleziono postów użytkownika o id = {id}" }
                });
            }
            
            return Ok(new
            {
                Model = chats,
                Success = true,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUpdateChatModel model)
        {
            if (UserId == null)
            {
                return Unauthorized(new
                {
                    Success = false,
                    Errors = new List<string> { "Błąd uwierzytelniania, zaloguj się ponownie i spróbuj jeszcze raz" }
                });
            }

            

            var result = await _repo.AddAsync(chat);

            if (!result)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało się dodać posta" }
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
                Model = chat
            });

        }
    }
}
