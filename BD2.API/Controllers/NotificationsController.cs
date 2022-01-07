using AutoMapper;
using BD2.API.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Controllers
{
    [Route("api/[controller]")]
    public class NotificationsController : ExtendedControllerBase
    {
        private readonly IPostDetailsRepository _repo;
        private readonly IPostCommentsRepository _cRepo;
        private readonly IPostReactionsRepository _rRepo;
        private readonly IChatRepository _chRepo;

        private readonly IMapper _mapper;

        public NotificationsController(IMapper mapper, IPostCommentsRepository cRepo, IPostReactionsRepository rRepo, IChatRepository chRepo, IPostDetailsRepository repo)
        {
            _mapper = mapper;
            _cRepo = cRepo;
            _rRepo = rRepo;
            _chRepo = chRepo;
            _repo = repo;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Counts(Guid id)
        {
            var commentsCount = await _repo
                .All()
                .Where(x => x.OwnerId == UserId)
                .Where(x => x.LastCommentDate > x.LastOwnerViewDate)
                .CountAsync();

            var reactionsCount = await _repo
                .All()
                .Where(x => x.OwnerId == UserId)
                .Where(x => x.LastReactionDate > x.LastOwnerViewDate)
                .CountAsync();

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
                data = new {
                    commentsCount,
                    reactionsCount
                }
            });
        }
    }
}
