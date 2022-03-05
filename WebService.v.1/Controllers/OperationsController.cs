using am.BL;
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
    public class OperationsController : ApiController
    {
        // GET: Operations
        public JArray Get(string from = "", string to = "", int Posr_Id = 0)
        {
            DataTable dt = G.db_select($"GetOperations {Posr_Id}, '{from}', '{to}'");
            if (G.CheckDB())
                return JsonHelper.ToJson(dt);
            else
                return new JArray { "Error", G.LastError };
        }
    }
}
