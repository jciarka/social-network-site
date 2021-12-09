using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.GroupAccount
{
    public class GroupAccountModel
    {
        public Guid AccountId { get; set; }
        public Guid GroupId { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public bool IsAdmin { get; set; }
        public DateTime? LastViewDate { get; set; }
    }
}
