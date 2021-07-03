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
using DevExpress.XtraEditors;

namespace Frut
{
    public partial class FrmHistory : XtraForm
    {
        public int Oplata_ID = 0;
        public FrmHistory()
        {
            InitializeComponent();
        }

        private void FrmHistory_Load(object sender, EventArgs e)
        {
            gc.DataSource = G.db_select(
                $@"
                    select [FieldName] Поле
                          ,[OldValue] Старое
                          ,[NewValue] Новое
                          ,[usr] Кто
                          ,[dtc] Когда
                    from ActionLog
                    where Oplata_ID = {Oplata_ID}
                    order by 1 desc
                ");
        }
    }
}
