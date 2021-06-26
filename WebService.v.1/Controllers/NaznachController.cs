using am.BL;
using Newtonsoft.Json.Linq;
using static WebService.v._1.Tools.JsonHelpers;
using System.Data;
using System.Web.Http;

namespace WebService.v._1.Controllers
{
    public class NaznachController : ApiController
    {
        // GET: Naznach
        public JArray Get()
        {
            DataTable dt = G.db_select($"GetNaznachList");
            if (G.CheckDB())
                return ToJson(dt);
            else
                return new JArray { "Error", G.LastError };
        }
    }
}
