using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Subcriptions
{
    public class PacketSubcriptionsModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsOpen { get; set; }
        public int PeopleLimit { get; set; }
        public int GroupsLimit { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int FreeSlots { get; set; }
    }
}
