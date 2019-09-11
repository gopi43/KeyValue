using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Key.Models;
using System.Dynamic;

namespace Key
{
    public class DataReadercs
    {
        Dictionary<string, object> dataValues = new Dictionary<string, object>();
        string json = File.ReadAllText(@"c:\temp\New.json");
        string path = @"c:\temp\New.json";

        public DataReadercs()
        {
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

           
            dynamic expando = new ExpandoObject();

            expando.Key = string.Empty;
            expando.Value = new ValueStore();

            expando = result;

            List<string> keys = new List<string>();
            
            foreach (var item in expando)
            {
                if((item.Value.CreatedTime-DateTime.Now)<item.Value.TimeToLive)
                {
                    keys.Add(item.Key);
                }
            }

        }



        public Dictionary<string, object> DataRead()
        {
            
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return result;
        }

        public List<object> DataRead(string KeyValue)
        {
            
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            var result1 = result.Where(o=>o.Key.ToLower()==KeyValue).Select(o=>o.Value).ToList();

            return result1;
        }

        public ResultData DataInsert(DataStore KeyValue)
        {

            ResultData resultData = new ResultData();
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            var result1 = result.Where(o => o.Key == KeyValue.Key).Select(o => o.Value).ToList();
            if(result1.Count>0)
            {
                resultData.Result = false;
                resultData.Message = "Key Already exists";
            }
            else
            {
                KeyValue.Value.CreatedTime = DateTime.Now;
                result.Add(KeyValue.Key, KeyValue.Value);
                var convertedJson = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText(path, convertedJson);
                resultData.Result = true;
                resultData.DataStore = convertedJson;
                resultData.Message = "Key added successfully";
            }

            return resultData;
        }

        public ResultData DataDelete(string KeyValue)
        {

            ResultData resultData = new ResultData();
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            var result1 = result.Where(o => o.Key.ToLower() == KeyValue).Select(o => o.Value).ToList();
            if (result1.Count == 0)
            {
                resultData.Result = false;
                resultData.Message = "Key doesnot exists";
            }
            else
            {
                result.Remove(KeyValue);
                var convertedJson = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText(path, convertedJson);
                resultData.Result = true;
                resultData.DataStore = convertedJson;
                resultData.Message = "Key Removed successfully";
            }

            return resultData;
        }

    }
}