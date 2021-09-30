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
    public class BalancesController : ApiController
    {
        // GET: Balances
        public JArray Get()
        {
            DataTable dt = G.db_select($"GetBalances");
            if (G.CheckDB())
                return JsonHelper.ToJson(dt);
            else
                return new JArray { "Error", G.LastError };
        }
    }
}
