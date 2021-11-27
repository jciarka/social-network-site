using AutoMapper;
using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using BD2.API.Models.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Controllers
{
    [Route("api/[controller]")]
    public class PostReactionController : ExtendedControllerBase
    {
        private readonly IPostReactionsRepository _repo;
        private readonly IMapper _mapper;

        public PostReactionController(IPostReactionsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{postId}")]
        public async Task<IActionResult> Get(Guid postId)
        {
            var reactions = await _repo
                .AllByPost(postId)
                .OrderByDescending(x => x.ReactionDate)
                .ToListAsync();

            Dictionary<int, int> reactionCounts = new();
            foreach (var type in Enum.GetValues(typeof(ReactionType)))
            {
                reactionCounts[(int)type] = reactions.Count(x => x.Type == (ReactionType)type);
            }

            ReactionType? hasReacted = null;
            if (UserId != default)
            {
                hasReacted = reactions
                    .Where(x => x.AccountId == UserId)
                    .Select(x => x.Type)
                    .FirstOrDefault();
            }

            var reactionsModel = reactions.Select(x => _mapper.Map<PostReactionModel>(x));

            return Ok(new
            {
                Success = true,
                counts = reactionCounts,
                hasReacted = hasReacted,
                data = reactionsModel
            });
        }

        [AllowAnonymous]
        [HttpPost("{postId}/{userId}")]
        public async Task<IActionResult> Post(Guid postId, Guid userId, [FromBody] PostReactionAddModel model)
        {
            // user and post id mist be defined
            if (postId == default || userId == default)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie podano id użytkownika lub postu" }
                }); ;
            }

            var found = await _repo.FindAsync(postId, userId);
            bool success;
            if (found == null)
            {
                // add reaction
                var postReaction = _mapper.Map<PostReaction>(model);
                postReaction.AccountId = userId;
                postReaction.PostId = postId;
                success = await _repo.AddAsync(postReaction);
            }
            else 
            {
                var postReaction = _mapper.Map(model, found);
                success = await _repo.UpdateAsync(found);
            }

            if (!success)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało się dodać reakcji. Spróbuj później." }
                });
            }

            // fetch comment with user data
            var reactionsResult = await _repo.All()
                .Where(x => x.PostId == postId && x.AccountId == userId)
                .Include(x => x.Account)
                .FirstOrDefaultAsync();

            var data = _mapper.Map<PostReactionModel>(reactionsResult);

            return Ok(new
            {
                Success = true,
                data
            });
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("{postId, userId}")]
        public async Task<IActionResult> Delete(Guid postId, Guid userId) // dodawanie nowych encji
        {
            var result = await _repo.DeleteAsync(postId, userId);

            if (result == false)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało sie usunąć komentarza" },
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
