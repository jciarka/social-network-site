using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Dtos.Chat
{
    public record CreateChatDto
    {
        public Guid Name { get; set; }

        [JsonIgnore]
        public ICollection<Guid> MembersIds { get; set; }
    }
}
