using AutoMapper;
using BD2.API.Database.Repositories.Interfaces;
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
    public class TopicsController : ExtendedControllerBase
    {
        private readonly ITopicsRepository _repo;

        public TopicsController(ITopicsRepository repo)
        {
            _repo = repo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _repo.All().ToListAsync();
            return Ok(new { success = true, data });
        }
    }
}
   
