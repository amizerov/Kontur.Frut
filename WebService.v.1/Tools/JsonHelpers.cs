using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebService.v._1.Tools
{
    public static class JsonHelpers
    {

        public static JArray ToJson(DataTable source)
        {
            JArray result = new JArray();
            JObject row;
            foreach (DataRow dr in source.Rows)
            {
                row = new JObject();
                foreach (DataColumn col in source.Columns)
                {
                    row.Add(col.ColumnName.Trim(), JToken.FromObject(dr[col]));
                }
                result.Add(row);
            }
            return result;
        }

    }
}