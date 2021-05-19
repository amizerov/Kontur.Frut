using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using am.BL;
using System.Data;

namespace Frut.Models
{
    public class CFirma
    {
        public int ID { get; set; }
        public string INN { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public CFirma() { ID = 0; }
        public CFirma(int id)
        {
            ID = 0;
            DataTable dt = G.db_select($"select * from Firma where ID = {id}");
            foreach (DataRow r in dt.Rows)
            {
                ID = G._I(r["ID"]);
                Name = G._S(r["Name"]);
                INN = G._S(r["INN"]);
            }
        }
        public CFirma(string inn)
        {
            ID = 0;
            DataTable dt = G.db_select($"select * from Firma where INN = '{inn}'");
            foreach (DataRow r in dt.Rows)
            {
                ID = G._I(r["ID"]);
                Name = G._S(r["Name"]);
                INN = G._S(r["INN"]);
            }
        }
        public CFirma(string inn, string name)
        { 
            ID = G._I(G.db_select($"select id from Firma where INN = '{inn}' and Name = '{name}'"));
            if (ID == 0)
                ID = G._I(G.db_select($"insert Firma(INN, Name) values('{inn}', '{name}') select @@IDENTITY"));
            Name = name;
            INN = inn;
        }
    }
    public class CFirmas : List<CFirma>
    {
        public CFirmas()
        {
            DataTable dt = G.db_select("select * from Firma");
            foreach (DataRow r in dt.Rows)
            {
                CFirma f = new CFirma();
                f.ID = G._I(r["ID"]);
                f.Name = G._S(r["Name"]);
                f.INN = G._S(r["INN"]);
                Add(f);
            }
        }
    }
}
