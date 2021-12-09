using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.GroupAccount
{
    public class GroupAccountAddModel
    {
        public Guid AccountId { get; set; }
        public Guid GroupId { get; set; }
    }
}
