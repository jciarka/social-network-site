using BD2.API.Database;
using BD2.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Seed
{
    public class GroupTopicsDataProvider
    {
        public static async Task Seed(AppDbContext ctx)
        {
            using var transaction = ctx.Database.BeginTransaction();
            {
                try
                {
                    // ctx.Database.ExecuteSqlCommand("SET IDENTITY_INSERT PacketGroupsLimits ON;");
                    // Periods
                    List<GroupTopic> topics = new()
                    {
                        new GroupTopic { Topic = "Sport" },
                        new GroupTopic { Topic = "Turystyka" },
                        new GroupTopic { Topic = "Polityka" },
                    };

                    foreach (var topic in topics)
                    {
                        if (!ctx.GroupTopics.Any(x => x.Topic == topic.Topic))
                        {
                            ctx.GroupTopics.Add(topic);
                        }
                    }

                    await ctx.SaveChangesAsync();
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
