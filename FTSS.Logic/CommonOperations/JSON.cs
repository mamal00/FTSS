﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.CommonOperations
{
    public class JSON
    {
        public static T jsonToT<T>(string json)
        {
            var rst = JsonConvert.DeserializeObject<T>(json);
            return (rst);
        }

        public static string ObjToJson(object data)
        {
            var rst = JsonConvert.SerializeObject(data);
            return rst;
        }
    }
}
