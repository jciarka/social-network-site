using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class Account : IdentityUser<Guid>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [JsonIgnore]
        public ICollection<Friendship> Friendships { get; set; }
        [JsonIgnore]
        public ICollection<Invitation> Invitations { get; set; }
    }
}
