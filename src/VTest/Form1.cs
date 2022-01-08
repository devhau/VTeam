using System.Diagnostics;
using VLib.Network;
using VLib.Network.Stun;

namespace VTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            CenterSystem.Instance = new CenterSystem();
            CenterSystem.Instance.Start();
            CenterSystem.Instance.Message += Center_Message;
        }

        private void Center_Message(string obj)
        {
            WriteLog(String.Format("Center:{0}", obj));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CenterSystem.Instance?.Stop();
            ClientSystem.Instance?.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ClientSystem.Instance == null)
            {
                ClientSystem.Instance = new ClientSystem();
                ClientSystem.Instance.Message += Client_Message;
                ClientSystem.Instance.Connect("167.179.95.220", 2515);
                ClientSystem.Instance.Start();
            }
            if (ClientSystem.Instance != null)
            {
                ClientSystem.Instance.SendText("Hello");
            }
        }

        private void Client_Message(string obj)
        {
            WriteLog(String.Format("Client:{0}", obj));
        }
        private void WriteLog(string log)
        {
            textBox1.Invoke(() =>
            {
                textBox1.AppendText(String.Format("[{1}]{0}\r\n", log, DateTime.Now));
            });

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = String.Format("{0}", StunClient.GetEndPoint()?.External);
            WriteLog(Text);
        }
    }
}