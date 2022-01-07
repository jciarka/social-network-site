using AutoMapper;
using BD2.API.Database.Repositories.Interfaces;
using BD2.API.Models.Notifications;
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
        private readonly IPostsRepository _repo;
        private readonly IPostCommentsRepository _cRepo;
        private readonly IPostReactionsRepository _rRepo;
        private readonly IChatRepository _chRepo;

        private readonly IMapper _mapper;

        public NotificationsController(IMapper mapper, IPostCommentsRepository cRepo, IPostReactionsRepository rRepo, IChatRepository chRepo, IPostsRepository repo)
        {
            _mapper = mapper;
            _cRepo = cRepo;
            _rRepo = rRepo;
            _chRepo = chRepo;
            _repo = repo;
        }

        [HttpGet]
        [Route("Counts")]
        public async Task<IActionResult> Counts()
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

            // TO DO: chat counts

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
                Data = new
                {
                    commentsCount,
                    reactionsCount
                }
            });
        }

        [HttpGet]
        [Route("comments")]
        public async Task<IActionResult> CommentsNotifications()
        {
            var found = await _cRepo
                .GetNotifications((Guid)UserId)
                .ToListAsync();

            return Ok(new
            {
                Success = true,
                Data = found
            });
        }


        [HttpGet]
        [Route("reactions")]
        public async Task<IActionResult> ReactionsNotifications()
        {
            var found = await _cRepo
                .GetNotifications((Guid)UserId)
                .ToListAsync();

            return Ok(new
            {
                Success = true,
                Data = found
            });
        }
    }
}
