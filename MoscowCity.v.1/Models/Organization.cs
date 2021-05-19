using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using am.BL;
using System.Data;

namespace Frut.Models
{
    public class COrganization
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public COrganization() { }
        public COrganization(int id)
        {
            DataTable dt = G.db_select($"select * from Organization where ID = {id}");
            foreach (DataRow r in dt.Rows)
            {
                ID = G._I(r["ID"]);
                Name = G._S(r["Name"]);
            }
        }
    }

    public class COrganizations: List<COrganization>
    {
        public COrganizations()
        {
            DataTable dt = G.db_select("select * from Organization");
            foreach (DataRow r in dt.Rows)
            {
                COrganization o = new COrganization();
                o.ID = G._I(r["ID"]);
                o.Name = G._S(r["Name"]);
                Add(o);
            }
        }
    }
}
