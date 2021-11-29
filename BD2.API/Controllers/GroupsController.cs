using AutoMapper;
using BD2.API.Database.Entities;
using BD2.API.Database.Repositories.Interfaces;
using BD2.API.Models.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Controllers
{

    namespace BD2.API.Controllers
    {
        [Route("[controller]")]
        public class GroupsController : ExtendedControllerBase
        {
            private readonly IGroupRepository _repo;
            private readonly IPacketSubscriptionsRepository _srepo;
            private readonly IMapper _mapper;

            public GroupsController(IGroupRepository repo, IMapper mapper, IPacketSubscriptionsRepository srepo)
            {
                _repo = repo;
                _mapper = mapper;
                _srepo = srepo;
            }

            [AllowAnonymous]
            [HttpGet]
            [Route("{id}")]
            public async Task<IActionResult> Get(Guid id)
            {
                var found = await _repo.All()
                    .Where(x => x.Id == id)
                    .Include(x => x.Members)
                    .Include(x => x.Posts)
                    .Include(x => x.Subscription)
                    .ThenInclude(x => x.Packet)
                    .FirstOrDefaultAsync();

                if (found == null)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Errors = new List<string> { "Nie znaleziono grupy o podanym Id" }
                    });
                }

                var group = _mapper.Map<GroupModel>(found);

                return Ok(new
                {
                    Success = true,
                    Errors = (List<string>)null,
                    data = group
                });
            }
            
            [HttpGet]
            [Route("find")]
            public async Task<IActionResult> Get(GroupFilter filter)
            {
                var groupsQuery = _repo.All()
                    .Include(x => x.Members)
                    .Include(x => x.Posts)
                    .Include(x => x.Subscription)
                    .ThenInclude(x => x.Packet)
                    .AsQueryable();

                if (filter.Topic != default)
                {
                    groupsQuery = groupsQuery.Where(x => x.GroupTopic == filter.Topic);
                }

                if (filter.UseSubcription != default)
                {
                    groupsQuery = groupsQuery.Where(x => x.Subscription != null);
                }

                if (filter.Name != default)
                {
                    groupsQuery = groupsQuery.Where(x => x.Name.Contains(filter.Name));
                }

                if (filter.IsMember != default)
                {
                    groupsQuery = groupsQuery.Where(x => x.Members.Any(x => x.AccountId == UserId));
                }

                if (filter.IsOwner != default)
                {
                    groupsQuery = groupsQuery.Where(x => x.Members.Any(x => x.IsAdmin && x.AccountId == UserId));
                }

                if (filter.IsOpen != default)
                {
                    if (filter.IsOpen == true)
                    {
                        groupsQuery = groupsQuery.Where(x => x.Subscription != null && 
                            x.Subscription.Packet != null && 
                            x.Subscription.Packet.IsOpen == filter.IsOpen);
                    }
                    else
                    {
                        groupsQuery = groupsQuery.Where(x => x.Subscription == null ||
                            ( x.Subscription.Packet != null && x.Subscription.Packet.IsOpen == filter.IsOpen));
                    }
                }

                var groups = (await groupsQuery
                    .ToListAsync())
                    .Select(x => _mapper.Map<GroupSimplifiedModel>(x));

                if (groups == null)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Errors = new List<string> { "Nie znaleziono grup" }
                    });
                }

                return Ok(new
                {
                    Success = true,
                    Errors = (List<string>)null,
                    data = groups
                });
            }

            [HttpPost]
            [Route("my")]
            /** Verbose data - for administratio purposes
             *  Only owners group returned by default       */
            public async Task<IActionResult> GetForAdministration(GroupFilter filter) 
            {
                var groupsQuery = _repo.All()
                    .Include(x => x.Members)
                    .Include(x => x.Posts)
                    .Include(x => x.Subscription)
                    .ThenInclude(x => x.Packet)
                    .Where(x => x.Members.Any(x => x.IsAdmin && x.AccountId == UserId));

                if (filter.Topic != default)
                {
                    groupsQuery = groupsQuery.Where(x => x.GroupTopic == filter.Topic);
                }

                if (filter.UseSubcription != default)
                {
                    groupsQuery = groupsQuery.Where(x => x.Subscription != null);
                }

                if (filter.Name != default)
                {
                    groupsQuery = groupsQuery.Where(x => x.Name.Contains(filter.Name));
                }

                var groups = (await groupsQuery
                    .ToListAsync())
                    .Select(x => _mapper.Map<GroupModel>(x));

                if (groups == null)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Errors = new List<string> { "Nie znaleziono grup" }
                    });
                }

                return Ok(new
                {
                    Success = true,
                    Errors = (List<string>)null,
                    data = groups
                });
            }

            [AllowAnonymous]
            [HttpPost]
            public async Task<IActionResult> Post(GroupAddModel model) 
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

                var group = _mapper.Map<Group>(model);
                var result = await _repo.AddAsync(group, (Guid)UserId);
                
                if (!result)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Errors = new List<string> { "Nie udało się dodać grupy" }
                    });
                }

                return await Get(group.Id);
            }

            [AllowAnonymous]
            [HttpPut]
            [Route("{id}")]
            public async Task<IActionResult> Put(Guid id, GroupAddModel model) // dodawanie nowych encji
            {
                var group = await _repo.FindAsync(id);

                if (group == null)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Errors = new List<string> { "Id ksiązki nie zgodny z id operacji" },
                    });
                }

                _mapper.Map(model, group);

                var result = await _repo.UpdateAsync(group);

                if (result == false)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Errors = new List<string> { "Nie udało sie zmodyfikować grupy" },
                    });
                }

                return await Get(group.Id);
            }

            [AllowAnonymous]
            [HttpPut]
            [Route("{id}/setpacket")]
            public async Task<IActionResult> Put(Guid id, SetSubcriptionModel sub) // dodawanie nowych encji
            {
                var group = await _repo.FindAsync(id);

                if (group == null || sub == null || sub.SubscriptionId == default)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Errors = new List<string> { "Nieprawidłowe dane" },
                    });
                }

                var slots = await _srepo.AvailableSlotsCount(sub.SubscriptionId);

                if (slots <= 0)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Errors = new List<string> { "Wybrany pakiet nie ma dostępnych slotów" },
                    });
                }

                group.SubscriptionId = sub.SubscriptionId;

                var result = await _repo.UpdateAsync(group);

                if (result == false)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Errors = new List<string> { "Nie udało sie zmodyfikować grupy" },
                    });
                }

                return await Get(group.Id);
            }
        }
    }
}
