using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class PostCommentsController : ExtendedControllerBase
    {
        private readonly IPostCommentsRepository _repo;
        private readonly IMapper _mapper;

        public PostCommentsController(IPostCommentsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{postId}")]
        public async Task<IActionResult> Get(Guid postId)
        {
            var comments = await _repo
                .AllByPost(postId)
                .OrderByDescending(x => x.CommentDate)
                .ToListAsync();

            var commentsModel = comments.Select(x => _mapper.Map<PostCommentModel>(x));

            return Ok(new
            {
                Success = true,
                data = commentsModel
            });
        }

        [AllowAnonymous]
        [HttpPost("{postId}/{userId}")]
        public async Task<IActionResult> Post(Guid postId, Guid userId, [FromBody]PostCommentAddModel model) 
        {
            // Comment can't be empty - defined in PostComment by model annotation
            if (!ModelState.IsValid)
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
            }

            // user and post id myst be defined
            if (postId == default || userId == default)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie podano id użytkownika lub postu" }
                }); ;
            }

            // add comment
            var postComment = _mapper.Map<PostComment>(model);
            postComment.AccountId = userId;
            postComment.PostId = postId;
            var success = await _repo.AddAsync(postComment);

            if (!success)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało się dodać komentarza. Spróbuj później." }
                });
            }

            // fetch comment with user data
            var commentResult = await _repo.All()
                .Where(x => x.PostId == postId && x.AccountId == userId)
                .Include(x => x.Account)
                .FirstOrDefaultAsync();

            var data = _mapper.Map<PostCommentModel>(commentResult);

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
