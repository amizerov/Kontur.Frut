using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using am.BL;
using System.Data;

namespace Frut.Models
{
    public class CNaznachen
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public override string ToString()
        {
            return Text;
        }
        public CNaznachen() { }
        public CNaznachen(int id)
        {
            DataTable dt = G.db_select($"select * from Naznachen where ID = {id}");
            foreach (DataRow r in dt.Rows)
            {
                ID = G._I(r["ID"]);
                Text = G._S(r["Text"]);
            }
        }
    }
    public class CNaznachens : List<CNaznachen>
    {
        public CNaznachens()
        {
            DataTable dt = G.db_select("select * from Naznachen");
            foreach (DataRow r in dt.Rows)
            {
                CNaznachen o = new CNaznachen();
                o.ID = G._I(r["ID"]);
                o.Text = G._S(r["Text"]);
                Add(o);
            }
        }
    }
}
