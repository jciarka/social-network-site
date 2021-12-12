using BD2.API.Database;
using BD2.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
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
                    ctx.ChangeTracker.Clear();

                    var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var filePath = buildDir + $"/Seed/themas.json";
                    var json = System.IO.File.ReadAllText(filePath);
                    IEnumerable<string> themas = JsonSerializer.Deserialize<List<string>>(json);

                    var topics = themas.Select(x => new GroupTopic { Topic = x });

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
