using am.BL;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;

namespace Frut1Cv2
{
    public class Остатки
    {
        public class A
        {
            public string? broker;
            public B[]? data;
        }
        public class B
        {
            public string? organization;
            public double balance;
            public string? currency;
        }
        public static void Load()
        {
            // Пишет в таблицы Posrednik и RestDate
            // Остатки у посредников
            var clt = new RestClient("http://progerx.svr.vc/Sales/hs/RCB/Report");
            clt.Authenticator = new HttpBasicAuthenticator("mobile", "1cultrazoom21");
            var req = new RestRequest("/Stocks");
            var result = clt.ExecuteAsync(req).Result;
            string js = result.Content!;

            A[] jo = JsonConvert.DeserializeObject<A[]>(js)!;
            //dynamic jo = JsonConvert.DeserializeObject<dynamic>(js)!;

            foreach (var o in jo)
            {
                double s = 0;
                bool r = true;
                var n = G._S(o.broker);
                foreach (var d in o.data!)
                {
                    s += G._Double(d.balance);
                    r = G._S(d.currency) == "RUB";
                }
                string sum = s.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                G.db_exec($"SetPosred '{n}', {(r ? sum : "0")}, {(r ? "0" : sum)}");
            }
        }
    }
}
