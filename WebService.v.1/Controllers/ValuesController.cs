using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using WebService.v._1.Models;
using am.BL;
 
namespace WebService.v._1.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public JArray Get(string from = "", string to = "")
        {
            DataTable dt = G.db_select($"GetOplatas '{from}', '{to}'");
            if (G.CheckDB())
                return ToJson(dt);
            else
                return new JArray { "Error", G.LastError };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] CUpdateOplataField v)
        {
            int val;
            string usr = v.usr;
            int oid = v.Oplata_ID;
            string fin = v.FieldName;

            if(fin == "Procent")
                val = v.IntVal;
            else
                val = v.BoolVal ? 1 : 0;

            G.db_exec($"OplUpd_{fin} {oid}, {val}, '{usr}'");
        }
        /*public void Post([FromBody] CRecieved v)
        {
            int oid = v.Oplata_ID;
            int val = v.IsRecieved ? 1 : 0;
            string usr = v.usr;

            G.db_exec($"OplUpd_IsRecieved {oid}, {val}, '{usr}'");
        }
        public void Post([FromBody] CVidano v)
        {
            int oid = v.Oplata_ID;
            int val = v.IsVidano ? 1 : 0;
            string usr = v.usr;

            G.db_exec($"OplUpd_IsVidano {oid}, {val}, '{usr}'");
        }*/
        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
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
