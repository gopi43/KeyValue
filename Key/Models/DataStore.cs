using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Key.Models
{
    public class DataStore
    {
        public string Key { get; set; }        
        public string Name { get; set; }
        public string Age { get; set; }
        public DateTime CreatedTime { get; set; }
        public long TimeToLive { get; set; }
    }
}