using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Key.Models
{
    public class ValueStore
    {
       public Tuple Tuple { get; set; }
        public DateTime? CreatedTime { get; set; }
        public long TimeToLive { get; set; }
    }
}