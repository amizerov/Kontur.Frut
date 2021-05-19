using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using am.BL;

namespace Frut.Models
{
    public class COplata
    {
        public int ID { get; set; }
        public COrganization Organization { get; set; }
        public CPosrednik Posrednik { get; set; }
        public int NomerPP { get; set; }
        public DateTime DataPP { get; set; }
        public CFirma Firma { get; set; }
        public string INN { get; set; }
        public CBank Bank { get; set; }
        public decimal SummaPP { get; set; }    //Сумма в платежке
        public decimal PenyProc { get; set; }       //Штраф %
        public decimal PenySumma { get; set; }  //Штраф сумма
        public decimal SummaZ { get; set; }     //Сумма к зачислению
        public CNaznachen Naznachen { get; set; }
        public bool IsPaied { get; set; }
        public bool IsRecieved { get; set; }
        public bool IsVidano { get; set; }

        public COplata()
        {
            ID = 0;
        }
        public COplata(int oplata_id)
        {
            DataTable dt = G.db_select($"select * from Oplata where ID = {oplata_id}");
            foreach (DataRow r in dt.Rows)
            {
                ID = G._I(r["ID"]);
                Organization = new COrganization(G._I(r["Org_ID"]));
                Posrednik = new CPosrednik(G._I(r["Posr_ID"]));
                NomerPP = G._I(r["NomerPP"]);
                DataPP = G._D(r["DataPP"]);
                Firma = new CFirma(G._I(r["Firma_ID"]));
                if (Firma.ID > 0)
                {
                    INN = Firma.INN;
                }
                else
                { 
                    INN = G._S(r["INN"]);
                }
                SummaPP = G._Dec(r["SummaPP"]);
                PenyProc = G._I(r["PenyProc"]);
                PenySumma = G._Dec(r["PenySumma"]);
                SummaZ = G._Dec(r["SummaZ"]);
                Naznachen = new CNaznachen(G._I(r["Nazn_ID"]));
                IsPaied = G._B(r["IsPaied"]);
                IsRecieved = G._B(r["IsRecieved"]);
                IsVidano = G._B(r["IsVidano"]);
            }
        }
        public string Exists()
        {
            return G._S(
                        G.db_select($@"
                            select p.Name from Oplata o 
                            join Posrednik p on p.ID = o.Posr_ID
                            where INN = '{INN}' 
                            and [SummaPP] = {SummaPP.ToString().Replace(',', '.')}
                            and DataPP = '{DataPP.ToString("yyyyMMdd")}'")
                        );
        }
        public void Save()
        {
            DataTable old_dt = G.db_select($"select * from Oplata where ID = {ID}");

            DataTable new_dt = G.db_select(
                $@"SaveUpdate_Oplata {ID}
                               ,{Organization.ID}
                               ,{Posrednik.ID}
                               ,{NomerPP}
                               ,'{DataPP.ToString("yyyyMMdd")}'
                               ,{Firma.ID}
                               ,'{INN}'
                               ,{SummaPP.ToString().Replace(',', '.')}
                               ,{PenyProc.ToString().Replace(',', '.')}
                               ,{PenySumma.ToString().Replace(',', '.')}
                               ,{SummaZ.ToString().Replace(',', '.')}
                               ,{Naznachen.ID}
                               ,{(IsPaied ? 1 : 0)}
                               ,{(IsRecieved ? 1 : 0)}
                               ,{(IsVidano ? 1 : 0)}
                               ,'{Environment.UserName}'
            ");

            SaveHistory(old_dt, new_dt);
        }
        void SaveHistory(DataTable odt, DataTable ndt)
        {
            if (odt.Rows.Count == 0) return;
            foreach (DataRow nr in ndt.Rows)
            {
                DataRow or = odt.Rows[0];
                DataColumnCollection cc = ndt.Columns;
                for (int i = 1; i < cc.Count - 5; i++)
                {
                    string cn = cc[i].ColumnName;
                    string ov = or[i].ToString(); 
                    string nv = nr[i].ToString();
                    if (ov != nv)
                        SaveActionHistory(cn, ov, nv);
                }
            }
        }
        void SaveActionHistory(string cn, string ov, string nv)
        {
            G.db_exec($@"
                insert ActionLog(Oplata_ID, FieldName, OldValue, NewValue, usr)
                values({this.ID}, '{cn}', '{ov}', '{nv}', '{Environment.UserName}')
            ");
        }
    }
}
