using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Frut.Models;
using am.BL;

namespace Frut
{
    public partial class FrmEditOplata : DevExpress.XtraEditors.XtraForm
    {
        CFirmas _firms = new CFirmas();
        public COplata Oplata;
        public FrmEditOplata()
        {
            InitializeComponent();
        }

        private void FrmEditOplata_Load(object sender, EventArgs e)
        {
            leOrgan.Properties.DataSource = new COrganizations();
            leOrgan.Properties.PopulateColumns();
            leOrgan.Properties.Columns[0].Visible = false;
            leOrgan.Properties.ValueMember = "ID";
            leOrgan.Properties.DisplayMember = "Name";

            lePosr.Properties.DataSource = new CPosredniks();
            lePosr.Properties.PopulateColumns();
            lePosr.Properties.Columns[0].Visible = false;
            lePosr.Properties.ValueMember = "ID";
            lePosr.Properties.DisplayMember = "Name";

            leFirma.Properties.DataSource = _firms;
            leFirma.Properties.PopulateColumns();
            leFirma.Properties.Columns[0].Visible = false;
            leFirma.Properties.DisplayMember = "Name";
            leFirma.Properties.ValueMember = "INN";

            foreach (CNaznachen n in new CNaznachens()) cbNaznachen.Properties.Items.Add(n);
            deDataPP.EditValue = DateTime.Now;

            if (Oplata == null)
            {
                Oplata = new COplata();
                leFirma.EditValue = "000000000000";
                cbNaznachen.SelectedIndex = 0;
            }
            else
            {
                Text = "Изменение оплаты";
                leOrgan.EditValue = Oplata.Organization.ID;
                lePosr.EditValue = Oplata.Posrednik.ID;
                txtNomerPP.Text = Oplata.NomerPP + "";
                deDataPP.EditValue = Oplata.DataPP;
                leFirma.Text = Oplata.Firma.Name;
                c = 1; txtINN.Text = Oplata.INN;
                txtSumma.Text = Oplata.SummaPP + "";
                txtPenyProc.Text = Oplata.PenyProc + "";
                txtPenySumma.Text = Oplata.PenySumma + "";
                txtSummaZ.Text = Oplata.SummaZ + "";
                cbNaznachen.EditValue = Oplata.Naznachen;
                chPaied.Checked = Oplata.IsPaied;
                chRecieved.Checked = Oplata.IsRecieved;
                chVidano.Checked = Oplata.IsVidano;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                //MessageBox.Show("Не все поля заполнены", "Сохранение оплаты",
                //                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Oplata.Firma = leFirma.EditValue == null ? 
                new CFirma(txtINN.Text, leFirma.Text) : new CFirma(leFirma.EditValue.ToString());

            if (G.LastError.Length > 0)
            {
                MessageBox.Show("Ошибка в БД \n" + G.LastError, 
                                "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Oplata.Organization = new COrganization((int)leOrgan.EditValue);
            Oplata.Posrednik = new CPosrednik((int)lePosr.EditValue);

            Oplata.NomerPP = G._I(txtNomerPP.Text);
            Oplata.DataPP = deDataPP.DateTime;
            Oplata.INN = Oplata.Firma.INN;

            Oplata.SummaPP = G._Dec(txtSumma.EditValue.ToString().Replace('.', ','));
            Oplata.PenyProc = G._Dec(txtPenyProc.EditValue.ToString().Replace('.', ','));
            Oplata.PenySumma = G._Dec(txtPenySumma.EditValue.ToString().Replace('.', ','));
            Oplata.SummaZ = G._Dec(txtSummaZ.EditValue.ToString().Replace('.', ','));
            
            Oplata.Naznachen = (CNaznachen)cbNaznachen.EditValue;
            Oplata.IsPaied = chPaied.Checked;
            Oplata.IsRecieved = chRecieved.Checked;
            Oplata.IsVidano = chVidano.Checked;

            if (Oplata.ID == 0)
            {
                string posr = Oplata.Exists();
                if (posr.Length > 0)
                {
                    if (MessageBox.Show("Такая оплата уже есть от посредника " + posr +
                                        "!\nХотите все равно сохранить?", "Двойная оплата!",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            Oplata.Save();
            if (G.LastError.Length > 0)
            {
                MessageBox.Show("Ошибка в БД \n" + G.LastError, 
                                "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Close();
        }

        private void cbFirma_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbFirma.SelectedIndex == -1)
            {
                //txtINN.Text = "";
            }
            //else
            {
                //txtINN.Text = ((CFirma)cbFirma.EditValue).INN;
            }
        }

        private void txtSumma_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            decimal nv = G._Dec(e.NewValue.ToString().Replace('.', ','));
            decimal pr = G._Dec(txtPenyProc.EditValue.ToString().Replace('.', ','));
            txtPenySumma.EditValue = nv * pr / 100;
            txtSummaZ.EditValue = nv - nv * pr/100;
        }

        private void txtPenyProc_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            decimal pr = G._Dec(e.NewValue.ToString().Replace('.', ','));
            decimal su = G._Dec(txtSumma.EditValue.ToString().Replace('.', ','));
            txtPenySumma.EditValue = su * pr / 100;
            txtSummaZ.EditValue = su - su * pr / 100;
        }

        bool IsValid()
        {
            bool r = true;
            double d = Convert.ToDouble(txtSumma.EditValue, txtSumma.Properties.Mask.Culture);
            if (leOrgan.EditValue == null)
            {
                leOrgan.BackColor = Color.LightSalmon;
                r = false;
            }
            if (lePosr.EditValue == null)
            { 
                lePosr.BackColor = Color.LightSalmon;
                r = false;
            }
            //G._I(txtNomerPP.Text) == 0 ||
            if (d == 0.0)
            {
                txtSumma.BackColor = Color.LightSalmon;
                r = false;
            }

            return r;
        }

        private void leFirma_QueryCloseUp(object sender, CancelEventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }
        int c = 0;
        private void txtINN_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (c == 1) { c = 0; return; }
            if (e.NewValue.ToString().Length == 0) return;

            leFirma.Properties.DataSource = _firms.FindAll(f => f.INN.StartsWith(e.NewValue.ToString()));
            leFirma.ShowPopup();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (c == 11)
            {
                leFirma.Text = s; c = 0; timer1.Stop(); return;
            }

            c = 1;    
            
            if (leFirma.EditValue == null)
                txtINN.Text = "";
            else
                txtINN.Text = leFirma.EditValue.ToString();


            timer1.Stop();
        }

        string s = "";
        private void leFirma_Leave(object sender, EventArgs e)
        {
            c = 11;
            timer1.Start();
        }

        private void leFirma_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) || ",.\"".Contains(e.KeyChar))
                s = leFirma.Text + e.KeyChar;
        }

        private void leOrgan_EditValueChanged(object sender, EventArgs e)
        {
            leOrgan.BackColor = leFirma.BackColor;
        }

        private void lePosr_EditValueChanged(object sender, EventArgs e)
        {
            lePosr.BackColor = leFirma.BackColor;
        }

        private void txtSumma_EditValueChanged(object sender, EventArgs e)
        {
            txtSumma.BackColor = leFirma.BackColor;
        }

        private void chPaied_CheckedChanged(object sender, EventArgs e)
        {
            if (chPaied.Checked) chVidano.Checked = true;
        }
    }
}