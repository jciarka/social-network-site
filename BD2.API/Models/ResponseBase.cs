using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Models
{
    public class ResponseBase
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; } 
    }
}
