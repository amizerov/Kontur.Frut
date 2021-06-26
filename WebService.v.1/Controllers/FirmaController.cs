using am.BL;
using Newtonsoft.Json.Linq;
using static WebService.v._1.Tools.JsonHelpers;
using System.Data;
using System.Web.Http;

namespace WebService.v._1.Controllers
{
    public class FirmaController : ApiController
    {
        // GET: Firma
        public JArray Get()
        {
            DataTable dt = G.db_select($"GetFirmaList");
            if (G.CheckDB())
                return ToJson(dt);
            else
                return new JArray { "Error", G.LastError };
        }
    }
}
