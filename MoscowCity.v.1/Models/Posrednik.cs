using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using am.BL;
using System.Data;

namespace Frut.Models
{
    public class CPosrednik
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Procent { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public CPosrednik() { }
        public CPosrednik(int id)
        {
            DataTable dt = G.db_select($"select * from Posrednik where ID = {id}");
            foreach (DataRow r in dt.Rows)
            {
                ID = G._I(r["ID"]);
                Name = G._S(r["Name"]);
                Procent = G._Double(r["Procent"]);
            }
        }
    }
    public class CPosredniks : List<CPosrednik>
    {
        public CPosredniks()
        {
            DataTable dt = G.db_select("select * from Posrednik");
            foreach (DataRow r in dt.Rows)
            {
                CPosrednik p = new CPosrednik();
                p.ID = G._I(r["ID"]);
                p.Name = G._S(r["Name"]);
                Add(p);
            }
        }
    }
}
