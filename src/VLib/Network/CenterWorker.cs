using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;
using VLib.Network.Stun;

namespace VLib.Network
{
    public abstract class CenterWorker : CenterThread<CenterSystem>
    {
    }
    public class CenterReceiverWorker : CenterWorker
    {
        public int Port { get; set; }
        public UdpClient? Client;
        public void SendText(string text,IPEndPoint? remoteIP = null)
        {
            byte[] data = Encoding.Unicode.GetBytes(text);
            SendData(data, remoteIP);
        }
        public void SendData(byte[] data, IPEndPoint? remoteIP=null)
        {
            this.Client?.Send(data, remoteIP);
        }
        protected override void DoWork()
        {
            if(Client!= null) Center?.ReceiveData(Client.Receive(ref remoteIPEndPoint), this);
        }
        public IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);

        protected override void OnPrepare(object sender)
        {
            Client = new(Port);
            
            this.ShowMessage("Start Service: 127.0.0.1:" + Port);
        }
    }
}
