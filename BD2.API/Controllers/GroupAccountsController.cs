using AutoMapper;
using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using BD2.API.Models.GroupAccount;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Controllers
{
    [Route("api/[controller]")]
    public class GroupAccountsController : ExtendedControllerBase
    {
        private readonly IGroupAccountsRepository _repo;
        private readonly IGroupRepository _grepo;
        private readonly IMapper _mapper;

        public GroupAccountsController(IGroupAccountsRepository repo, IMapper mapper, IGroupRepository grepo)
        {
            _repo = repo;
            _mapper = mapper;
            _grepo = grepo;
        }

        [HttpGet]
        [Route("{groupId}")]
        public async Task<IActionResult> GetMembers(Guid groupId)
        {
            var found = await _repo.FindByGroupAsync(groupId);
            var data = found.Select(x => _mapper.Map<GroupAccountModel>(x));

            return Ok(new { Success = true, data });
        }

        [HttpGet]
        [Route("{groupId}/member/{userId}")]
        public async Task<IActionResult> GetMember(Guid groupId, Guid userId)
        {
            var found = await _repo.FindAsync(userId, groupId);
            var data = _mapper.Map<GroupAccountModel>(found);

            return Ok(new { Success = true, data });
        }

        [HttpPost]
        public async Task<IActionResult> AddMember([FromBody] GroupAccountAddModel model)
        {
            if (model == null || model.AccountId == default || model.GroupId == default)
            {
                return BadRequest(new { Success = false, Errors = new List<string> { "Nieprawidłowe dane" } });
            }

            var count = await _repo.AvailableSlotsCount(model.GroupId);

            if (count < 0)
            {
                return Ok(new { Success = false, Errors = new List<String>() { "Nie znaleziono grupy" } } );
            }

            if (count == 0)
            {
                return Ok(new { Success = false, Errors = new List<String>() { "Przepraszamy, brak wolnych miejsc w grupie" } });
            }

            var entity = _mapper.Map<GroupAccount>(model);
            var success = await _repo.AddAsync(entity);

            if (success == false)
            {
                return BadRequest(new { Success = false, Errors = new List<string> { "Nie udało się dodać nowego członka. Spróbuj później." } });
            }

            return await GetMember(entity.GroupId, entity.AccountId);
        }

        [HttpDelete]
        [Route("{groupId}/member/{userId}")]
        public async Task<IActionResult> DeleteMember(Guid groupId, Guid userId)
        {
            var found = _repo.FindAsync(userId, groupId);

            if (found == null)
            {
                return NotFound(new { Success = false, Errors = new List<string> { "Nieprawidłowe dane" } });
            }

            var success = await _repo.DeleteAsync(userId, groupId);

            if (success == false)
            {
                return BadRequest(new { Success = false, Errors = new List<string> { "Nie udało się dodać nowego członka" } });
            }

            return Ok(new { Success = true });
        }
    }
}
