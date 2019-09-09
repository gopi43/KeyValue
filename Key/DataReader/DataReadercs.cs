using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Key.Models;

namespace Key
{
    public class DataReadercs
    {
        Dictionary<string, object> dataValues = new Dictionary<string, object>();
        string json = File.ReadAllText(@"c:\temp\New.json");
        string path = @"c:\temp\New.json";




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