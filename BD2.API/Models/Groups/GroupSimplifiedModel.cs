using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Groups
{
    public class GroupSimplifiedModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastPostDate { get; set; }
        public int MembersCount { get; set; }
        public string GroupTopic { get; set; }
        public bool IsOpen { get; set; }
    }
}
