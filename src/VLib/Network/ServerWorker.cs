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
    public abstract class ServerWorker : CenterThread<ServerSystem>
    {
    }
    public class ServerReceiverWorker : ServerWorker
    {
        public int Port { get; set; }
        public UdpClient? Client;

        public void SendMessage(IMessage message, IPEndPoint? remoteIP = null)
        {
            SendData(BinaryUtils.ObjectToByteArray(message), remoteIP);
        }

        public void SendMessage<TMessage, TEnity>(TEnity data, IPEndPoint? remoteIP = null)
                where TMessage : IMessage, new()
                where TEnity : IEnity, new()
        {
            var message = new TMessage();
            message.SetEnity<TEnity>(data);
            SendMessage(message, remoteIP);
        }
        public void SendMessage<TMessage, TEnity>(Action<TEnity> action, IPEndPoint? remoteIP = null)
               where TMessage : IMessage, new()
               where TEnity : IEnity, new()
        {
            var message = new TMessage();
            TEnity enity = new();
            action(enity);
            message.SetEnity<TEnity>(enity);
            SendMessage(message, remoteIP);
        }
        public void SendData(byte[]? data, IPEndPoint? remoteIP=null)
        {
            if (data == null) return;

            this.Client?.Send(this.Center?.Crypt.Encrypt(data), remoteIP);
        }
        protected override void DoWork()
        {
            var remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Center?.ReceiveData(Client?.Receive(ref remoteIPEndPoint), new ServerReceiveDataInfo()
            {
                Current = this,
                IP= remoteIPEndPoint
            });
        }
        protected override void OnPrepare(object sender)
        {
            Client = new(Port);
            
            this.ShowMessage("Start Service: 127.0.0.1:" + Port);
        }
    }
}
