using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Key.Models;
using System.Dynamic;
using Newtonsoft.Json.Linq;

namespace Key
{
    public class DataReadercs
    {
        Dictionary<string, object> dataValues = new Dictionary<string, object>();
        
        string path = @"c:\temp\New.json";

        public DataReadercs()
        {
            string json = File.ReadAllText(@"c:\temp\New.json");
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataStore>>(json);

            var temp = result.Where(o => o.TimeToLive > 0
                              && (DateTime.Now.Subtract(o.CreatedTime) > TimeSpan.FromTicks(o.TimeToLive)))
                            .Select(o=>o).ToList();

            

            if (temp.Count>0)
            {
                foreach(var item in temp)
                {
                    result.Remove(item);
                }

                var convertedJson = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText(path, convertedJson);
            }
        }



        public List<DataStore> DataRead()
        {
            string json = File.ReadAllText(@"c:\temp\New.json");
            var result = JsonConvert.DeserializeObject<List<DataStore>>(json);
            return result;
        }

        public ResultData DataRead(string KeyValue)
        {
            string json = File.ReadAllText(@"c:\temp\New.json");
            ResultData resultData = new ResultData();
            var result = JsonConvert.DeserializeObject<List<DataStore>>(json);

            var result1 = result.Where(o=>o.Key.ToLower()==KeyValue).Select(o=>o).ToList();
            if (result1.Count > 0)
            {
                resultData.Result = true;
                resultData.DataStore = result1;
                resultData.Message = "Key Retrived";
            }
            else
            {
                resultData.Result = false;
                resultData.Message = "Key doesnot exists";
            }
            return resultData;
        }

        public ResultData DataInsert(DataStore KeyValue)
        {
            string json = File.ReadAllText(@"c:\temp\New.json");
            ResultData resultData = new ResultData();
            var result = JsonConvert.DeserializeObject<List<DataStore>>(json);

            var result1 = result.Where(o => o.Key == KeyValue.Key).Count();
            if(result1>0)
            {
                resultData.Result = false;
                resultData.Message = "Key Already exists";
            }
            else
            {
                KeyValue.CreatedTime = DateTime.Now;
                result.Add(KeyValue);
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
            string json = File.ReadAllText(@"c:\temp\New.json");
            ResultData resultData = new ResultData();
            var result = JsonConvert.DeserializeObject<List<DataStore>>(json);

            var result1 = result.Where(o => o.Key.ToLower() == KeyValue).Select(o=>o).ToList();
            if (result1.Count() == 0)
            {
                resultData.Result = false;
                resultData.Message = "Key doesnot exists";
            }
            else
            {
                foreach (var item in result1)
                {
                    result.Remove(item);
                }
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