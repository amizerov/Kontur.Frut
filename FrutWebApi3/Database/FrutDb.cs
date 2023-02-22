using am.BL;
using System.Data;

namespace FrutWebApi3.Database
{
    public class FrutDb
    {
        public static void Init()
        {
            am.DB.DBManager.Instance.Init("server=.;database=MoscowCity;uid=city;pwd=!QAZ1qaz");
        }
        public static string GetDeviceInfo(string uuid)
        {
            string last_lgn = "";
            string sql = $"select last_lgn from iPhone where uuid = '{uuid}'";
            DataTable dt = G.db_select(sql);
            if (dt.Rows.Count > 0)
            {
                last_lgn = G._S(dt);
            }
            else
            {
                sql = $"insert iPhone(uuid) values('{uuid}')";
                G.db_exec(sql);
            }
            return last_lgn;
        }
        public static void SetDeviceInfo(string uuid, string lgn)
        {
            string sql = $"select last_lgn from iPhone where uuid = '{uuid}'";
            DataTable dt = G.db_select(sql);
            if (dt.Rows.Count > 0)
            {
                G.db_exec($"update iPhone set last_lgn = '{lgn}', dtu = getdate() where uuid = '{uuid}'");
            }
        }
    }
}
