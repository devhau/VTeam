using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCommon;

namespace VTeam
{
    public partial class VMain : FormBase
    {
        public VMain()
        {
            InitializeComponent();
        }
        private void VMain_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Connecting......";
            Client.Instance.Ping += Instance_Ping;
            Client.Instance.ClientConnected += Instance_ClientConnected;
            Client.Instance.Connect("127.0.0.1", 2515);
            Client.Instance.Start();
            Client.Instance.ConnectToServer();
        }

        private void Instance_ClientConnected(object? sender, VCommon.Events.ClientConnectedEventArgs e)
        {
            txtYourId.Text = e.Client.ClientId;
            txtPass.Text = e.Client.Pass;
        }

        private void Instance_Ping(string obj)
        {
            lblStatus.Text = "Connected";
        }

        private void btnConnectRemoteID_Click(object sender, EventArgs e)
        {
        }
    }
}
