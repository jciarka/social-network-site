using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Groups
{
    public class GroupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastPostDate { get; set; }
        public int MembersCount { get; set; }
        public int PostsCount { get; set; }
        public Guid? SubscriptionId { get; set; }
        public DateTime? SubscriptionExpirationDate { get; set; }
        public Guid? PacketId { get; set; }
        public string PacketName { get; set; }
        public int? PacketPeopleLimit { get; set; }
        public bool IsOpen { get; set; }
        public string GroupTopic { get; set; }
    }
}
