using am.BL;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Data;


namespace Frut1Cv2
{
    public class Операта
    {
        public static void Load()
        {
            // Операции посредника
            var clt = new RestClient("http://progerx.svr.vc/Sales/hs/RCB/Report");
            clt.Authenticator = new HttpBasicAuthenticator("mobile", "1cultrazoom21");
            DataTable dt = G.db_select("select [Name] from Posrednik");
            foreach (DataRow r in dt.Rows)
            {
                var req = new RestRequest("RP");
                DateTime now = DateTime.Now;
                req.AddParameter("StartDate", now.AddDays(-7).ToString("yyyyMMdd"));
                req.AddParameter("EndDate", now.AddDays(1).ToString("yyyyMMdd"));
                string broker = G._S(r);
                req.AddParameter("Broker", broker);
                var result = clt.ExecuteAsync(req).Result;
                string js = result.Content!;
                dynamic jo = JsonConvert.DeserializeObject<dynamic>(js)!;
                if (jo.Count > 0 && G._S(jo[0].broker) == broker)
                {
                    foreach (var o in jo[0].data)
                    {
                        var date = G._S(o.date);
                        var orga = G._S(o.organization);
                        var rems = o.remaining_start.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                        var comi = o.coming.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                        var cons = o.consumption.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                        var reme = o.remaining_end.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                        var curr = G._S(o.currency);
                        DateTime dd = DateTime.Parse(date, null, System.Globalization.DateTimeStyles.RoundtripKind);
                        date = dd.ToString("yyyyMMdd HH:mm:ss");

                        G.db_exec($"SetOper '{broker}', '{date}', '{orga}', '{curr}', {rems}, {comi}, {cons}, {reme}");
                    }
                }
            }

        }
    }
}
