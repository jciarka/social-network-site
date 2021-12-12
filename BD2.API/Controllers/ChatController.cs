using AutoMapper;
using BD2.API.Database.Dtos.Chat;
using BD2.API.Database.Entities;
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
        private readonly IMapper _mapper;

        public ChatController(IChatRepository repo, IChatAccountRepository arepo, IMapper mapper)
        {
            _repo = repo;
            _arepo = arepo;
            _mapper = mapper;
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

            var chatModel = new
            {
                Chat = chat
            };

            return Ok(new
            {
                Model = chatModel,
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
                data = chats,
                Success = true,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateChatModel model)
        {
            if (UserId == null)
            {
                return Unauthorized(new
                {
                    Success = false,
                    Errors = new List<string> { "Błąd uwierzytelniania, zaloguj się ponownie i spróbuj jeszcze raz" }
                });
            }

            Chat chat = _mapper.Map<Chat>(model);

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

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateChatModel model)
        {
            if (UserId == null)
            {
                return Unauthorized(new
                {
                    Success = false,
                    Errors = new List<string> { "Błąd uwierzytelniania, zaloguj się ponownie i spróbuj jeszcze raz" }
                });
            }

            var chat = await _repo.FindAsync(id);

            if (chat == null)
            {
                return NotFound(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie znaleziono czatu o podanym id" },
                });
            }

            Chat updatedChat = _mapper.Map(model, chat);
            var success = await _repo.UpdateAsync(updatedChat);

            if (success == false)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało sie zmodyfikować posta" },
                });
            }

            return Ok(new
            {
                Model = chat,
                Success = true,
                Errors = (List<string>)null,
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (UserId == null)
            {
                return Unauthorized(new
                {
                    Success = false,
                    Errors = new List<string> { "Id użytkownika i włąściciela postu nie są zgodne" }
                });
            }

            var chat = await _repo.FindAsync(id);

            if(chat == null)
            {
                return NotFound(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie znaleziono czatu o podanym Id" }
                });
            }

            var result = await _repo.DeleteAsync(id);
            
            if (result == false)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało sie usunąć czatu" },
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
            });
        }

        //TODO: AddMember AddEntry UpdateEntry RemoveMember RemoveEntry
    }
}
