using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.ChatEntry
{
    public class CreateChatEntryModel
    {
        public Guid AccountId { get; set; }
        public Guid ChatId { get; set; }
        public string Text { get; set; }
    }
}
