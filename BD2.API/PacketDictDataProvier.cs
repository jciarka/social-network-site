using BD2.API.Database;
using BD2.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API
{
    public class PacketDictDataProvier
    {
        public static async Task Seed(AppDbContext ctx)
        {
            using var transaction = ctx.Database.BeginTransaction();
            {
                try
                {
                    // ctx.Database.ExecuteSqlCommand("SET IDENTITY_INSERT PacketGroupsLimits ON;");
                    // Periods
                    List<PacketPeriod> periods = new()
                    {
                        new PacketPeriod { MonthsPeriod = 3 },
                        new PacketPeriod { MonthsPeriod = 6 },
                        new PacketPeriod { MonthsPeriod = 12 },
                    };

                    foreach (var period in periods)
                    {
                        if (!ctx.PacketPeriods.Any(x => x.MonthsPeriod == period.MonthsPeriod))
                        {
                            ctx.PacketPeriods.Add(period);
                        }
                    }

                    ctx.SaveChanges();

                    List<PacketPeopleLimit> peopleLimits = new()
                    {
                        new PacketPeopleLimit { PeopleLimit = 1000 },
                        new PacketPeopleLimit { PeopleLimit = 2000 },
                        new PacketPeopleLimit { PeopleLimit = 5000 },
                        new PacketPeopleLimit { PeopleLimit = 10000 },
                        new PacketPeopleLimit { PeopleLimit = 20000 },
                    };

                    foreach (var limit in peopleLimits)
                    {
                        if (!(await ctx.PacketPeopleLimits.AnyAsync(x => x.PeopleLimit == limit.PeopleLimit)))
                        {
                            ctx.PacketPeopleLimits.Add(limit);
                        }
                    }

                    ctx.SaveChanges();

                    List<PacketGroupsLimit> groupsLimits = new()
                    {
                        new PacketGroupsLimit { GroupsLimit = 1 },
                        new PacketGroupsLimit { GroupsLimit = 3 },
                        new PacketGroupsLimit { GroupsLimit = 5 },
                    };

                    foreach (var limit in groupsLimits)
                    {
                        if (!(await ctx.PacketGroupsLimits.AnyAsync(x => x.GroupsLimit == limit.GroupsLimit)))
                        {
                            ctx.PacketGroupsLimits.Add(limit);
                        }
                    }

                    ctx.SaveChanges();

                    // Pakiety
                    List<Packet> packets = new()
                    {
                        new Packet
                        {
                            Name = "Start - brązowy",
                            GroupsLimit = groupsLimits[0].GroupsLimit,
                            PeopleLimit = peopleLimits[0].PeopleLimit,
                            PacketPeriod = periods[0].MonthsPeriod,
                            IsOpen = false,
                            IsValid = true,
                            Price = 100,
                        },

                        new Packet
                        {
                            Name = "Średnioterminowy - brązowy",
                            GroupsLimit = groupsLimits[0].GroupsLimit,
                            PeopleLimit = peopleLimits[0].PeopleLimit,
                            PacketPeriod = periods[1].MonthsPeriod,
                            IsOpen = false,
                            IsValid = true,
                            Price = 250,
                        },

                        new Packet
                        {
                            Name = "Długoterminowy - brązowy",
                            GroupsLimit = groupsLimits[0].GroupsLimit,
                            PeopleLimit = peopleLimits[0].PeopleLimit,
                            PacketPeriod = periods[2].MonthsPeriod,
                            IsOpen = false,
                            IsValid = true,
                            Price = 400,
                        },

                        new Packet
                        {
                            Name = "Start - srebrny",
                            GroupsLimit = groupsLimits[1].GroupsLimit,
                            PeopleLimit = peopleLimits[2].PeopleLimit,
                            PacketPeriod = periods[0].MonthsPeriod,
                            IsOpen = false,
                            IsValid = true,
                            Price = 1000,
                        },

                        new Packet
                        {
                            Name = "Średnioterminowy - srebrny",
                            GroupsLimit = groupsLimits[1].GroupsLimit,
                            PeopleLimit = peopleLimits[2].PeopleLimit,
                            PacketPeriod = periods[1].MonthsPeriod,
                            IsOpen = false,
                            IsValid = true,
                            Price = 2500,
                        },

                        new Packet
                        {
                            Name = "Długorerminowy - srebrny",
                            GroupsLimit = groupsLimits[1].GroupsLimit,
                            PeopleLimit = peopleLimits[2].PeopleLimit,
                            PacketPeriod = periods[2].MonthsPeriod,
                            IsOpen = false,
                            IsValid = true,
                            Price = 4000,
                        },

                        new Packet
                        {
                            Name = "Start - złoty",
                            GroupsLimit = groupsLimits[2].GroupsLimit,
                            PeopleLimit = peopleLimits[4].PeopleLimit,
                            PacketPeriod = periods[0].MonthsPeriod,
                            IsOpen = false,
                            IsValid = true,
                            Price = 2000,
                        },

                        new Packet
                        {
                            Name = "Średnioterminowy - złoty",
                            GroupsLimit = groupsLimits[2].GroupsLimit,
                            PeopleLimit = peopleLimits[4].PeopleLimit,
                            PacketPeriod = periods[1].MonthsPeriod,
                            IsOpen = false,
                            IsValid = true,
                            Price = 5000,
                        },

                        new Packet
                        {
                            Name = "Długorerminowy - złoty",
                            GroupsLimit = groupsLimits[2].GroupsLimit,
                            PeopleLimit = peopleLimits[4].PeopleLimit,
                            PacketPeriod = periods[2].MonthsPeriod,
                            IsOpen = false,
                            IsValid = true,
                            Price = 7500,
                        },

                        new Packet
                        {
                            Name = "Komercyjna - brązowy",
                            GroupsLimit = groupsLimits[0].GroupsLimit,
                            PacketPeriod = periods[0].MonthsPeriod,
                            IsOpen = true,
                            IsValid = true,
                            Price = 2000,
                        },

                        new Packet
                        {
                            Name = "Komercyjna - srebrny",
                            GroupsLimit = groupsLimits[0].GroupsLimit,
                            PacketPeriod = periods[1].MonthsPeriod,
                            IsOpen = true,
                            IsValid = true,
                            Price = 5000,
                        },

                        new Packet
                        {
                            Name = "Komercyjna - złoty",
                            GroupsLimit = groupsLimits[0].GroupsLimit,
                            PacketPeriod = periods[2].MonthsPeriod,
                            IsOpen = true,
                            IsValid = true,
                            Price = 7500,
                        },
                    };

                    foreach (var packet in packets)
                    {
                        if (!await ctx.Packets.AnyAsync(x => x.Name == packet.Name))
                        {
                            ctx.Packets.Add(packet);
                            ctx.SaveChanges();
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
