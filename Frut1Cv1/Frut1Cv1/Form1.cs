using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using V83;

namespace Frut1Cv1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Com1C.OnConnect += Log;
            Com1C.OnError += Log;

            Loader.OnProgress += Log;
            Loader.OnComplete += Log;

            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtLog.Text = "";
            button1.Text = "Идет загрузка ...";
            button1.Enabled = false;

            if (!Com1C.IsConnected)
            {
                string ConnectionString = "File='D:\\Sales';Usr='mobile';Pwd='1cultrazoom21'";
                Com1C.ConnectTo1C(ConnectionString);
            }
            else
                Log("start");
        }

        void Log(string s)
        {
            Invoke(new Action(() => {
                txtLog.Text = DateTime.Now.ToString("hh:mm:ss") + " - " + s + "\r\n" + txtLog.Text;
                if (s.Contains("start"))
                {
                    Loader.Start();
                }
                if (s == "done")
                {
                    button1.Text = "Начать загрузку";
                    button1.Enabled = true;
                }
            }));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
    }
}
