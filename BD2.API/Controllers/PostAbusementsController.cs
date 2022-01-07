using AutoMapper;
using AutoMapper.QueryableExtensions;
using BD2.API.Configuration;
using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using BD2.API.Models.Abusements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Controllers
{
    [Route("api/[controller]")]
    public class PostAbusementsController : ExtendedControllerBase
    {
        private readonly IPostAbusementsRepository _repo;
        private readonly IMapper _mapper;

        public PostAbusementsController(IPostAbusementsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var found = await _repo
                .All()
                .Include(x => x.Account)
                .Include(x => x.Post)
                .Where(x => x.CheckedById == null)
                .ProjectTo<PostAbusementModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(new { Data = found, Success = true });
        }

        [HttpPatch]
        [Route("{postId}/{userId}/Block/{decision}")]
        public async Task<IActionResult> Confirm(Guid userId, Guid postId, bool decision)
        {
            if (!User.IsInRole(AppRole.MODERATOR.ToString()))
            {
                return Unauthorized(new
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Użytkownik nie ma uprawnienień do moderacji postów",
                    }
                });
            }

            var found = await _repo.FindAsync(postId, userId);

            if (found == null)
            {
                return NotFound(new
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Nie znaleziono zgłoszenia o podanych danych",
                    }
                });
            }

            found.CheckedDate = DateTime.Now;
            found.CheckedById = UserId;
            found.Status = decision;

            var success = await _repo.UpdateAsync(found);

            if (!success)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało zablokować posta" },
                });
            }

            return Ok(new { Success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddPostAbusementModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList()
                });
            }

            var found = await _repo.FindAsync(model.PostId, (Guid)UserId);

            if (found != null)
            {
                return Ok(new
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Już złożono zgłoszenie na temat wskazanego postu",
                    }
                });
            }

            var abusement = new PostAbusement()
            {
                AccountId = (Guid)UserId,
            };

            abusement = _mapper.Map(model, abusement);

            var success = await _repo.AddAsync(abusement);

            if (!success)
            {
                return BadRequest(new
                {
                    Success = false,
                    Error = new List<string>() { "Nie udało się dodać zgłoszenia. Spróbuj ponownie później." },
                });
            }

            return Ok(new { Success = true });
        }
    }
}