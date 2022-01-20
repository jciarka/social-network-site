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
    [Route("api/[controller]")]
    public class ChatAccountController : ExtendedControllerBase
    {
        private readonly IChatAccountRepository _repo;
        private readonly IChatRepository _crepo;
        private readonly IAccountRepository _arepo;
        public ChatAccountController(IChatAccountRepository repo, IChatRepository crepo, IAccountRepository arepo)
        {
            _repo = repo;
            _crepo = crepo;
            _arepo = arepo;
        }

        [HttpPut]
        [Route("{chatId}")]
        public async Task<IActionResult> UpdateDate(Guid chatId)
        {
            if (UserId == null)
            {
                return Unauthorized(new
                {
                    Success = false,
                    Errors = new List<string> { "Błąd uwierzytelniania, zaloguj się ponownie i spróbuj jeszcze raz" }
                });
            }

            ChatAccount chatAccount = new()
            {
                AccountId = (Guid)UserId,
                Account = await _arepo.FindAsync((Guid)UserId),
                ChatId = chatId,
                Chat = await _crepo.FindAsync(chatId),
                IsAdmin = false,
                LastViewDate = DateTime.Now
            };
            var result = await _repo.UpdateAsync(chatAccount);

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
            });

        }

        //[AllowAnonymous]
        //[HttpGet]
        //[Route("unseenCount")]
        //public async Task<IActionResult> UnseenChatsEntries()
        //{
        //    if (UserId == null)
        //    {
        //        return Unauthorized(new
        //        {
        //            Success = false,
        //            Errors = new List<string> { "Błąd uwierzytelniania, zaloguj się ponownie i spróbuj jeszcze raz" }
        //        });
        //    }

        //    var count = await _repo.UnseenEntriesCount((Guid)UserId);

        //    return Ok(new
        //    {
        //        Model = count,
        //        Success = true,
        //    });
        //}
    }
}
