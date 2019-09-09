using Key.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Key
{
    public class BaseController : ApiController
    {
        DataReadercs dataReadercs = new DataReadercs();
        
        public Dictionary<string, object> Get()
        {
            var temp =dataReadercs.DataRead();
            return temp;
        }
        public List<object> Get([FromUri]string key)
        {
            var temp = dataReadercs.DataRead(key);
            return temp;
        }
        public ResultData Post([FromBody]DataStore value)
        {
            var temp = dataReadercs.DataInsert(value);
            return temp;
        }    
        public ResultData Delete([FromUri]string key)
        {
            var temp = dataReadercs.DataDelete(key);
            return temp;
        }
    }
}