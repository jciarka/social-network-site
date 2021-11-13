using BD2.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BD2.API.Database.Dtos.Chat
{
    public record ChatDto
    {
        public Guid Id { get; set; }
        public Guid Name { get; set; }
        public DateTime? LastPostDate { get; set; }

        [JsonIgnore]
        public ICollection<ChatAccount> Members { get; set; }
        [JsonIgnore]
        public ICollection<ChatEntry> Entries { get; set; }
    }
}
