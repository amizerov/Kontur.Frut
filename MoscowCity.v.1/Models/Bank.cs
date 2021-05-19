using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using am.BL;
using System.Data;

namespace Frut.Models
{
    public class CBank
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public CBank() { }
        public CBank(int id)
        {
            DataTable dt = G.db_select($"select * from Bank where ID = {id}");
            foreach (DataRow r in dt.Rows)
            {
                ID = G._I(r["ID"]);
                Name = G._S(r["Name"]);
            }
        }
    }
    public class CBanks : List<CBank>
    {
        public CBanks()
        {
            DataTable dt = G.db_select("select * from Bank");
            foreach (DataRow r in dt.Rows)
            {
                CBank o = new CBank();
                o.ID = G._I(r["ID"]);
                o.Name = G._S(r["Name"]);
                Add(o);
            }
        }
    }
}
