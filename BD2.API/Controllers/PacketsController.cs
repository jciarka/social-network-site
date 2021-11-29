using AutoMapper;
using BD2.API.Database.Entities;
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
    [Route("api/[controller]")]
    public class PacketsController : ExtendedControllerBase
    {
        private readonly IPacketsRepository _repo;
        private readonly IMapper _mapper;

        public PacketsController(IPacketsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var found = await _repo.FindAsync(id);

            if (found == null)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie znaleziono pakietu o podanym Id" }
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
                data = found
            });
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var packets = await _repo.All().ToListAsync();

            if (packets == null)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie znaleziono pakietów" }
                });
            }

            return Ok(new
            {
                Success = true,
                data = packets
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(Packet packet) // dodawanie nowych encji
        {
            packet.Id = default;
            packet.IsValid = true;

            var result = await _repo.AddAsync(packet);

            if (!result)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało się dodać pakietu" }
                });
            }

            return Ok(new
            {
                Success = true,
                Errors = (List<string>)null,
                data = packet
            });
        }


        [AllowAnonymous]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id) // dodawanie nowych encji
        {
            var found = await _repo.FindAsync(id);
            if (found == null)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie istnieje pakiet o zadanym id" },
                });
            }

            found.IsValid = false;

            var result = await _repo.UpdateAsync(found);

            if (result == false)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = new List<string> { "Nie udało sie anulować pakietu" },
                });
            }

            return Ok(new
            {
                Success = true,
                data = found
            });
        }
    }
}
