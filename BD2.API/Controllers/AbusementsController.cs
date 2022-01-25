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
    public class AbusementsController : ExtendedControllerBase
    {
        private readonly IPostAbusementsRepository _repo;
        private readonly IPostsRepository _prepo;
        private readonly IAccountRepository _arepo;
        private readonly IMapper _mapper;

        public AbusementsController(IPostAbusementsRepository repo, IPostsRepository prepo, IAccountRepository arepo, IMapper mapper)
        {
            _repo = repo;
            _prepo = prepo;
            _arepo = arepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
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

        [HttpGet]
        [Route("list/user/{accountId}")]
        public async Task<IActionResult> GetUsersAbusements(Guid accountId)
        {
            var found = await _repo
                .All()
                .Where(x => x.AccountId == accountId)
                .ProjectTo<PostAbusementModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(new { Data = found, Success = true });
        }

        [HttpGet]
        [Route("list/post/{postId}")]
        public async Task<IActionResult> GetPostsAbusements(Guid postId)
        {
            var found = await _repo
                .All()
                .Where(x => x.PostId == postId)
                .ProjectTo<PostAbusementModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(new { Data = found, Success = true });
        }

        [HttpGet]
        [Route("list/posts")]
        public async Task<IActionResult> GetPosts()
        {
            var found = await _prepo
                .All()
                .Where(x => _repo.All().Select(y => y.PostId).Contains(x.Id))
                .ToListAsync();

            return Ok(new { Data = found, Success = true });
        }

        [HttpGet]
        [Route("list/users")]
        public async Task<IActionResult> GetUsers()
        {
            var found = await _arepo
                .All()
                .Where(x => _repo.All().Select(y => y.AccountId).Contains(x.Id))
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
        public async Task<IActionResult> Add([FromBody] AddPostAbusementModel model)
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