using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Groups
{
    public class GroupFilter
    {
        public string Topic { get; set; }
        public bool? UseSubcription { get; set; }
        public string Name { get; set; }
        public bool? IsOpen { get; set; }
        public bool? IsMember { get; set; }
        public bool? IsOwner { get; set; }
    }
}
