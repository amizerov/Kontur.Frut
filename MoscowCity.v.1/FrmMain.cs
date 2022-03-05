using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using am.BL;
using RestSharp;
using RestSharp.Authenticators;

namespace Frut
{
    public partial class FrmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmMain()
        {
            InitializeComponent();
            //am.DB.DBManager.Instance.Init("ProgerX.svr.vc", "Frut", "city", "!QAZ1qaz");
            am.DB.DBManager.Instance.Init("progerx.svr.vc", "MoscowCity", "city", "!QAZ1qaz");
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            bsiVersion.Caption = "Версия: " + Application.ProductVersion;
            bsiDatabase.Caption = "База: " + am.DB.DBManager.Instance.Database;
            bsiUser.Caption = "Юзер: " + Environment.UserName;

            deTo.EditValue = DateTime.Now;
            deFrom.EditValue = DateTime.Now.AddDays(-30);
            btnReload_ItemClick(null, null);
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmEditOplata f = new FrmEditOplata();
            f.ShowDialog(this);
            btnReload_ItemClick(null, null);
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string from = ((DateTime)deFrom.EditValue).ToString("yyyyMMdd");
            string to = ((DateTime)deTo.EditValue).ToString("yyyyMMdd");
            string sql = $"GetOplatas '{from}', '{to}'";

            DataTable dt = G.db_select(sql);
            gcMain.DataSource = dt;

            gvMain.Columns["Дата"].Summary.Clear();
            gvMain.Columns["Дата"].Summary.Add(DevExpress.Data.SummaryItemType.Count);

            gvMain.Columns["Сумма"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvMain.Columns["Сумма"].DisplayFormat.FormatString = "{0:c2}";
            gvMain.Columns["Сумма"].Summary.Clear();
            gvMain.Columns["Сумма"].Summary.Add(DevExpress.Data.SummaryItemType.Sum).DisplayFormat = "{0:c2}";

            gvMain.Columns["Штраф"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvMain.Columns["Штраф"].DisplayFormat.FormatString = "{0:c2}";
            gvMain.Columns["Штраф"].Summary.Clear();
            gvMain.Columns["Штраф"].Summary.Add(DevExpress.Data.SummaryItemType.Sum).DisplayFormat = "{0:c2}";

            gvMain.Columns["Зачислено"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvMain.Columns["Зачислено"].DisplayFormat.FormatString = "{0:c2}";
            gvMain.Columns["Зачислено"].Summary.Clear();
            gvMain.Columns["Зачислено"].Summary.Add(DevExpress.Data.SummaryItemType.Sum).DisplayFormat = "{0:c2}";

            gvMain.Columns[0].Visible = false;
            gvMain.BestFitColumns(true);
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int h = gvMain.FocusedRowHandle;
            int Oplata_ID = G._I(gvMain.GetDataRow(h)[0]);
            FrmEditOplata f = new FrmEditOplata();
            f.Oplata = new Models.COplata(Oplata_ID);
            f.ShowDialog(this);
            btnReload_ItemClick(null, null);
        }

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_ItemClick(null, null);
        }

        private void gvMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnEdit_ItemClick(null, null);
        }

        private void btnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int h = gvMain.FocusedRowHandle;
            int Oplata_ID = G._I(gvMain.GetDataRow(h)[0]);
            G.db_exec($"update Oplata set IsDeleted = 1 where ID = {Oplata_ID}");
            btnReload_ItemClick(null, null);
        }

        private void deFrom_EditValueChanged(object sender, EventArgs e)
        {
            if ((DateTime)deFrom.EditValue > (DateTime)deTo.EditValue)
                deTo.EditValue = (DateTime)deFrom.EditValue;

            btnReload_ItemClick(null, null);
        }

        private void deTo_EditValueChanged(object sender, EventArgs e)
        {
            if ((DateTime)deFrom.EditValue > (DateTime)deTo.EditValue)
                deFrom.EditValue = (DateTime)deTo.EditValue;

            btnReload_ItemClick(null, null);
        }

        private void ribbonControl1_SelectedPageChanged(object sender, EventArgs e)
        {
            if (ribbonControl1.SelectedPage == rpMain)
            {
                gcOstatki.Visible = gcVipiska.Visible = false;
                gcMain.Visible = true;
            }
            else if (ribbonControl1.SelectedPage == rpOstatki)
            {
                gcMain.Visible = gcVipiska.Visible = false;
                gcOstatki.Visible = true;
                gcOstatki.Dock = DockStyle.Fill;
            }
            else if (ribbonControl1.SelectedPage == rpVipiska)
            {
                gcMain.Visible = gcOstatki.Visible = false;
                gcVipiska.Visible = true;
                gcVipiska.Dock = DockStyle.Fill;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Остатки у посредников
            var clt = new RestClient("http://progerx.svr.vc/Sales/hs/RCB/Report");
            clt.Authenticator = new HttpBasicAuthenticator("mobile", "1cultrazoom21");
            var req = new RestRequest("/Stocks", DataFormat.Json);
            var result = clt.Execute(req);
            string js = result.Content;
            dynamic jo = SimpleJson.DeserializeObject(js);
            foreach (var o in jo)
            {
                var n = o.broker;
                double s = 0;
                bool r = true;
                foreach (var d in o.data)
                {
                    s += d.balance;
                    r = d.currency == "RUB";
                }
                string sum = s.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                G.db_exec($"SetPosred '{n}', {(r ? sum : "0")}, {(r ? "0" : sum)}");
            }

            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            barButtonItem3_ItemClick(null, null);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Операции посредника
            var clt = new RestClient("http://progerx.svr.vc/Sales/hs/RCB/Report");
            clt.Authenticator = new HttpBasicAuthenticator("mobile", "1cultrazoom21");
            DataTable dt = G.db_select("select [Name] from Posrednik");
            foreach (DataRow r in dt.Rows)
            {
                var req = new RestRequest("RP", DataFormat.Json);
                DateTime now = DateTime.Now;
                req.AddParameter("StartDate", now.AddDays(-7).ToString("yyyyMMdd"));
                req.AddParameter("EndDate", now.AddDays(1).ToString("yyyyMMdd"));
                string broker = G._S(r);
                req.AddParameter("Broker", broker);
                var result = clt.Execute(req);
                string js = result.Content;
                dynamic jo = SimpleJson.DeserializeObject(js);
                if (jo.Count > 0 && jo[0].broker == broker)
                {
                    foreach (var o in jo[0].data)
                    {
                        var date = o.date;
                        var orga = o.organization;
                        var rems = o.remaining_start.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                        var comi = o.coming.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                        var cons = o.consumption.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                        var reme = o.remaining_end.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                        var curr = o.currency;
                        DateTime dd = DateTime.Parse(date, null, System.Globalization.DateTimeStyles.RoundtripKind);
                        date = dd.ToString("yyyyMMdd HH:mm:ss");

                        G.db_exec($"SetOper '{broker}', '{date}', '{orga}', '{curr}', {rems}, {comi}, {cons}, {reme}");
                    }
                }
            }

            if (!timer2.Enabled)
            {
                timer2.Enabled = true;
                timer2.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            barButtonItem4_ItemClick(null, null);
            barButtonItem5_ItemClick(null, null);

            barButtonItem3_ItemClick(null, null);

        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Долларывые операции с подробностями по машинам
            var clt = new RestClient("http://progerx.svr.vc/Sales/hs/RCB/Report");
            clt.Authenticator = new HttpBasicAuthenticator("mobile", "1cultrazoom21");
            var req = new RestRequest("CarsInfo", DataFormat.Json);
            DateTime now = DateTime.Now;
            req.AddParameter("StartDate", now.AddDays(-7).ToString("yyyyMMdd"));
            req.AddParameter("EndDate", now.AddDays(1).ToString("yyyyMMdd"));
            var result = clt.Execute(req);
            string js = result.Content;
            dynamic jo = SimpleJson.DeserializeObject(js);

            if (jo.Count > 0)
            {
                foreach (var o in jo)
                {
                    string date = o.documentdate;
                    date = date.Replace("T", " ").Substring(0, 19);
                    var numb = o.documentnumber;

                    var d = o.data;
                    var n = d.carnumber;
                    var i = d.invoice;
                    var t = d.terminal;
                    var p = d.product;
                    string w = d.weight.ToString(); w = w.Replace(",", ".");
                    string s = d.checkout.ToString(); s = s.Replace(",", ".");

                    G.db_exec($"SetCar '{date}', {numb}, '{n}', '{i}', '{t}', '{p}', '{w}', '{s}'");
                }
            }

        }
    }
}
