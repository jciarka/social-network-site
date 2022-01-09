using AutoMapper;
using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using BD2.API.Models.ChatEntry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Controllers
{
    [Route("api/[controller]")]
    public class ChatEntryController : ExtendedControllerBase
    {
        private readonly IChatEntryRepository _repo;
        private readonly IChatRepository _crepo;
        private readonly IAccountRepository _arepo;
        private readonly IMapper _mapper;

        public ChatEntryController(IChatEntryRepository repo, IChatRepository crepo, IAccountRepository arepo, IMapper mapper)
        {
            _repo = repo;
            _crepo = crepo;
            _arepo = arepo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entry = await _repo.FindAsync(id);

            if (entry == null)
            {
                return NotFound(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie znaleziono czatu o podanym Id" }
                });
            }

            var entryModel = new
            {
                ChatEntry = entry
            };

            return Ok(new
            {
                Model = entryModel,
                Success = true
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("list/chat/{id}")]
        public async Task<IActionResult> ChatsEntries(Guid id)
        {
            var entries = await _repo.FindChatsEntries(id);

            if (entries == null)
            {
                return NotFound(new
                {
                    Success = false,
                    Errors = new List<string> { $"Nie znaleziono postów użytkownika o id = {id}" }
                });
            }

            return Ok(new
            {
                Model = entries,
                Success = true,
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateChatEntryModel model)
        {
            if (UserId == null)
            {
                return Unauthorized(new
                {
                    Success = false,
                    Errors = new List<string> { "Błąd uwierzytelniania, zaloguj się ponownie i spróbuj jeszcze raz" }
                });
            }

            ChatEntry entry = new()
            {
                Id = new Guid(),
                ChatId = model.ChatId,
                Chat = await _crepo.FindAsync(model.ChatId),
                AccountId = (Guid) UserId,
                Account = await _arepo.FindAsync((Guid) UserId),
                PostDate = DateTime.Now,
                Text = model.Text
            };

            var result = await _repo.AddAsync(entry);

            if (!result)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało się dodać wiadomości" }
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
                Model = entry
            });

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateChatEntryModel model)
        {
            if (UserId == null)
            {
                return Unauthorized(new
                {
                    Success = false,
                    Errors = new List<string> { "Błąd uwierzytelniania, zaloguj się ponownie i spróbuj jeszcze raz" }
                });
            }

            var entry = await _repo.FindAsync(id);

            if (entry == null)
            {
                return NotFound(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie znaleziono wiadomości o podanym id" },
                });
            }

            ChatEntry found = await _repo.FindAsync(id);
            found.Text = model.Text;
            found.PostDate = DateTime.Now;

            var success = await _repo.UpdateAsync(found);

            if (success == false)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało się zmodyfikować wiadomości" },
                });
            }

            return Ok(new
            {
                Model = entry,
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

            var entry = await _repo.FindAsync(id);

            if (entry == null)
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

    }
}
