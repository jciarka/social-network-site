using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models.Abusements
{
    public class PostAbusementModel
    {
        public Guid AccountId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public Guid PostId { get; set; }
        public string PostTitle { get; set; }

        public string Text { get; set; }
        public DateTime AbusementDate { get; set; }

        public Guid? CheckedById { get; set; }
        public DateTime? CheckedDate { get; set; }
        public bool? Status { get; set; }
    }
}