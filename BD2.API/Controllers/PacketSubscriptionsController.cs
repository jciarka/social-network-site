using AutoMapper;
using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Concrete;
using BD2.API.Database.Repositories.Interfaces;
using BD2.API.Models.Packets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Controllers
{
    [Route("[controller]")]
    public class PacketSubscriptionsController : ExtendedControllerBase
    {
        private readonly IPacketSubscriptionsRepository _repo;
        private readonly IMapper _mapper;

        public PacketSubscriptionsController(PacketSubscriptionsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetByGroupId(Guid userId)
        {
            var found = await _repo
                .All()
                .Where(x => x.OwnerId == userId)
                .Include(x => x.Groups)
                .ToListAsync();

            if (found == null)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie znaleziono pakietów dla użytkonika o podanym id" }
                });
            }

            return Ok(new
            {
                Success = true,
                data = found
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(AddSubscriptionPacketModel model) // dodawanie nowych encji
        {
            var subcription = _mapper.Map<PacketSubscription>(model);
            var result = await _repo.AddAsync(subcription); 

            if (!result)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało się wykupic sybskrybcji. Spróbuj ponownie później." }
                });
            }

            return Ok(new
            {
                Success = true,
                data = subcription
            });
        }
    }
}
