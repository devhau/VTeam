using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;
using VLib.Network.Stun;
using VLib.Utilities;

namespace VLib.Network
{
    public class ClientSystem: Center<ClientWorker, ClientSystem>
    {
        public event Action<string> Status;
        public void OnStatus(string ID)
        {
            this.Status?.Invoke(ID);  
        }
        protected string IPAddress=string.Empty;
        protected int Port = 0; 
        public UdpClient Client { get; private set; }
        public Guid ClientId = Guid.NewGuid();
        public ClientSystem()
        {
            Client = new UdpClient();
        }
        protected override void OnStart()
        {
            base.OnStart();
            DoPing();
        }
        public void SendMessage(IMessage message)
        {
            SendData(BinaryUtils.ObjectToByteArray(message));
        }

        public void SendMessage<TMessage, TEnity>(TEnity data) 
                where TMessage : IMessage,new()
                where TEnity : IEnity, new()
        {
            var message = new TMessage();
            message.SetEnity<TEnity>(data);
            SendMessage(message);
        }
        public void SendMessage<TMessage, TEnity>(Action<TEnity> action)
               where TMessage : IMessage, new()
               where TEnity : IEnity, new()
        {
            var message = new TMessage();
            TEnity enity = new();
            action(enity);
            message.SetEnity<TEnity>(enity);
            SendMessage(message);
        }
        public void Connect(string ip,int port)
        {
            this.IPAddress = ip;
            this.Port = port;

            this.ShowMessage("Connect:"+ip+":" + port);
            this.Client.Connect(System.Net.IPAddress.Parse(ip), port);
            this.AddWorker<ClientReceiverWorker>();
           
        }
        public void DoPing()
        {
            this.SendMessage<PingServer, PingInfo>((item) =>
            {
                item.ClientId = this.ClientId.ToString();
            });
        }
        public void SendData(byte[]? data)
        {
            if (data == null) return;
            this.Client.Send(this.Crypt.Encrypt(data));
        }
        public void ReceiveData(byte[] data, ClientReceiveDataInfo clientReceiver)
        {
            var msg = (IMessage)BinaryUtils.ByteArrayToObject(this.Crypt.Decrypt(data));
            var command = Mapping?.GetCommand(msg);
            if (command != null)
            {
                if (command is CommandBase @base)
                {
                    @base.SetReceiveData(clientReceiver);
                }
                command.DoCommand();
            }
        }
    }
}
