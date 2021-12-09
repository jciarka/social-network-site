using BD2.API.Configuration;
using BD2.API.Database;
using BD2.API.Database.Entities;
using BD2.API.Models.Auth;
using Microsoft.AspNetCore.Identity;
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
    public class AccountsDataProvider
    {

        public static async Task SeedRoles(AppDbContext ctx)
        {
            using var transaction = ctx.Database.BeginTransaction();
            {
                var roles = await ctx.Roles
                    .Select(x => x.Name)
                    .ToListAsync();

                foreach (var role in Enum.GetNames<AppRole>())
                {
                    if (!roles.Contains(role))
                    {
                        ctx.Roles.Add(new Role { Name = role, NormalizedName = role });
                    }
                }

                                try
                {
                    await ctx.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        public static async Task Seed(AppDbContext ctx, UserManager<Account> uManager)
        {
            using var transaction = ctx.Database.BeginTransaction();
            {
                try
                {
                    var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var filePath = buildDir + $"/Seed/accounts.json";
                    var json = System.IO.File.ReadAllText(filePath);
                    IEnumerable<UserModel> accounts = JsonSerializer.Deserialize<List<UserModel>>(json);

                    foreach (var account in accounts)
                    {
                        var user = new Account
                        {
                            Firstname = account.Firstname,
                            Lastname = account.Lastname,
                            Email = stripText(account.Firstname) + "." + stripText(account.Lastname) + "@bd2.pl",
                            UserName = stripText(account.Firstname) + "." + stripText(account.Lastname) + "@bd2.pl",
                            EmailConfirmed = true
                        };

                        if (!ctx.Users.Any(x => x.Email == user.Email))
                        {
                            var success = await uManager.CreateAsync(user, "DB@db2");
                            success = await uManager.AddToRoleAsync(user, AppRole.USER.ToString());
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

        private static string stripText(string title)
        {
            string input = title.Trim().ToLower();
            string tmp = input.Replace("ą", "a");
            tmp = tmp.Replace("ć", "c");
            tmp = tmp.Replace("ę", "e");
            tmp = tmp.Replace("ł", "l");
            tmp = tmp.Replace("ń", "n");
            tmp = tmp.Replace("ó", "o");
            tmp = tmp.Replace("ś", "s");
            tmp = tmp.Replace("ź", "z");
            tmp = tmp.Replace("ż", "z");
            return tmp;
        }
    }
}
