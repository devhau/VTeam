using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;
using VLib.Network;

namespace VCommon.Commands
{
    [Serializable]
    public class ClientInfo : IEnity
    {
        public string ClientId { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
    }
    [Serializable]
    public class ConnectToServerMessage : IMessage
    {
        public ConnectToServerMessage()
        {
            this.MessageCode = (int)CommandCode.ConnectToServer;
        }
    }
    public class ConnectToServerCommand : CommandBase
    {
        public override void DoCommand()
        {
            var clientInfo = this.Message?.GetEnity<ClientInfo>();
            if (clientInfo == null || string.IsNullOrEmpty(clientInfo.ClientId)) return;
            if (this.Status == ReceiveFrom.Client)  // Process on Client
            {
                this.ClientReceive?.Current?.GetCenter<IClient>()?.OnClientConnected(clientInfo);
            }
            if (this.Status == ReceiveFrom.Server)  // Process on Server
            {
                if (this.ServerReceive == null|| this.ServerReceive.Current==null) return;
                clientInfo.Pass =new Random().Next(1111,9999).ToString();
                this.ServerReceive.Current.SendMessage<ConnectToServer, ClientInfo>(clientInfo, this.ServerReceive.IP);
                IServer? center = this.ServerReceive.Current.GetCenter<IServer>();
                if (center != null)
                    center.Clients[clientInfo.ClientId] = this.ServerReceive;
            }
        }
    }
}

