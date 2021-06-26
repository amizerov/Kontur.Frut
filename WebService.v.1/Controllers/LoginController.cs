using am.BL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.v._1.Controllers
{
    public class LoginController : ApiController
    {
        public JArray Get(string lgn = "", string pwd = "")
        {
            DataTable dt = G.db_select($"DoLogin '{lgn}', '{pwd}'");
            if (G.CheckDB())
                return ToJson(dt);
            else
                return new JArray { "Error", G.LastError };
        }

        JArray ToJson(DataTable source)
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
