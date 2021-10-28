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
        public JArray Get(string from = "", string to = "", int Posr_Id = 0)
        {
            DataTable dt = G.db_select($"GetOplatas '{from}', '{to}', {Posr_Id}");
            if (G.CheckDB())
                return JsonHelper.ToJson(dt);
            else
                return new JArray { "Error", G.LastError };
        }

        // GET api/values/5
        public JArray Get(int id)
        {
            DataTable dt = G.db_select($"GetOplataChangesHistory {id}");
            if (G.CheckDB())
                return JsonHelper.ToJson(dt);
            else
                return new JArray { "Error", G.LastError };
        }

        // POST api/values
        public JArray Post([FromBody] CUpdateOplataField v)
        {
            int val;
            string usr = v.usr;
            int oid = v.Oplata_ID;
            string fin = v.FieldName;

            if(fin == "Procent")
                val = v.IntVal;
            else
                val = v.BoolVal ? 1 : 0;

            DataTable dt = G.db_select($"OplUpd_{fin} {oid}, {val}, '{usr}'");

            JArray res = JsonHelper.ToJson(dt);
            return res;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
