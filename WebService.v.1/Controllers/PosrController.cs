using System.Web.Http;
using System.Data;
using WebService.v._1.Models;
using am.BL;
using Newtonsoft.Json.Linq;

namespace WebService.v._1.Controllers
{
    public class PosrController : ApiController
    {
        // GET: Posr
        public JArray Get()
        {
            DataTable dt = G.db_select($"GetPosredList");
            if (G.CheckDB())
                return JsonHelper.ToJson(dt);
            else
                return new JArray { "Error", G.LastError };
        }
    }
}