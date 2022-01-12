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
    public class ConnectRemoteToUser:IEnity
    {
        public string FromId { get; set; } = string.Empty;
        public string RemoteId { get; set; } = string.Empty;
        public string RemotePass { get; set; } = string.Empty;
        public bool Confirm { get; set; } = false;
    }
    [Serializable]
    public class ConnectRemoteToUserMessage : IMessage
    {
        public ConnectRemoteToUserMessage()
        {
            this.MessageCode = (int)CommandCode.ConnectToServer;
        }
    }
    public class ConnectRemoteToUserCommand : CommandBase
    {
        public override void DoCommand()
        {
            var RemoteInfo = this.Message?.GetEnity<ConnectRemoteToUser>();
            if (RemoteInfo == null || string.IsNullOrEmpty(RemoteInfo.FromId) || string.IsNullOrEmpty(RemoteInfo.RemoteId)) return;
            if (this.Status == ReceiveFrom.Client)  // Process on Client
            {
                if (this.ClientReceive == null || this.ClientReceive.Current == null) return;
            }
            if (this.Status == ReceiveFrom.Server)  // Process on Server
            {
                if (this.ServerReceive == null || this.ServerReceive.Current == null) return;
                // check Client is Online
                var @server=this.ServerReceive.Current.GetCenter<IServer>();
                if(@server != null)
                {
                    if (@server.Clients.ContainsKey(RemoteInfo.RemoteId))
                    {

                    }
                }
            }
        }
    }
}
