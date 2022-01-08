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
    public class ClientSystem : Center<ClientWorker, ClientSystem>
    {
        protected string IPAddress=string.Empty;
        protected int Port=0; 
        public UdpClient Client { get; private set; }
        public Guid ClientId = Guid.NewGuid();
        public ClientSystem()
        {
            Client= new UdpClient();
            this.SetInstance();
        }
        public void Connect(string ip,int port)
        {
            this.IPAddress = ip;
            this.Port = port;

            this.ShowMessage("Connect:"+ip+":" + port);
            this.Client.Connect(System.Net.IPAddress.Parse(ip), port);
            this.AddWorker<ClientReceiverWorker>();
        }
        public void SendText(string text)
        {
            this.ShowMessage(text);
            byte[] data = Encoding.Unicode.GetBytes(text);
            SendData(data);
        }
        //  byte[] data = Encoding.ASCII.GetBytes(msg);
        public void SendData(byte[] data)
        {
            this.Client.Send(data);
        }
        public void ReceiveData(byte[] data, IPEndPoint remoteIPEndPoint)
        {
            this.ShowMessage("Đang nhận dữ liệu từ Client:" + remoteIPEndPoint);
            this.ShowMessage(Encoding.Unicode.GetString(data));
        }
    }
}
