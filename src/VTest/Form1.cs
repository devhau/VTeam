using System.Diagnostics;
using VCommon;
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
            Server.Instance.Start();
            Server.Instance.Message += Center_Message;
        }

        private void Center_Message(string obj)
        {
            WriteLog(String.Format("Center:{0}", obj));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Client.Instance?.Stop();
            Server.Instance?.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                Client.Instance.Message += Client_Message;
                Client.Instance.Connect("167.179.95.220", 2515);
                Client.Instance.Start();
                Client.Instance.SendText("Hello");
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