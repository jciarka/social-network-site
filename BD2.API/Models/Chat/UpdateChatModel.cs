using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Dtos.Chat
{
    public record UpdateChatModel
    {
        public string Name { get; set; }
    }
}
