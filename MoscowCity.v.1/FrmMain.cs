using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using am.BL;

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
            if(e.KeyCode == Keys.Enter)
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
    }
}
