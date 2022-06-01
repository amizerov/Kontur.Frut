namespace Frut1Cv2
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName + "  v." + Application.ProductVersion;

            am.DB.DBManager.Instance.Init(FrutDB.ConnectionString);

            Com1C.OnConnect += Log;
            Com1C.OnError += Log2;

            Loader.OnProgress += Log1;
            Loader.OnComplete += Log;
            Loader.OnError += Log2;

            timer1.Start();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            txtLog.Text = "";
            btnStart.Text = "Идет загрузка ...";
            btnStart.Enabled = false;

            if (!Com1C.IsConnected)
            {
                Log("Подключение к 1С ... ... ...");
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
                    btnStart.Text = "Начать загрузку";
                    btnStart.Enabled = true;
                }
            }));
        }
        void Log1(string s, int i)
        {
            Invoke(new Action(() => {
                txtLog.Text = DateTime.Now.ToString("hh:mm:ss") + " - " + s + "\r\n" + txtLog.Text;
                if(i > 0)
                    txtLog1.Text = DateTime.Now.ToString("hh:mm:ss") + " - " + s + "\r\n" + txtLog1.Text;
            }));
        }
        void Log2(string s)
        {
            Invoke(new Action(() => {
                txtLog.Text = DateTime.Now.ToString("hh:mm:ss") + " - " + s + "\r\n" + txtLog.Text;
                txtLog2.Text = DateTime.Now.ToString("hh:mm:ss") + " - " + s + "\r\n" + txtLog2.Text;
            }));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnStart_Click(sender, e);
        }
    }
}