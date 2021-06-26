using am.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.v._1.Models;

namespace WebService.v._1.Controllers
{
    public class NewOplaController : ApiController
    {
        // POST api/values
        public void Post([FromBody] CNewOplata o)
        {
            G.db_exec($@"NewOplata 
                {o.nomerp},
                {o.posred}, 
                {o.summap}, 
                {o.firmap},
                {o.contra},
                {o.naznac},
                {o.procen},
                '{o.datepp}',
                '{o.usr}'
            ");
        }

    }
}
