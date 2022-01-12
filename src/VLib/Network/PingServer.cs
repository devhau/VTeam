using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;

namespace VLib.Network
{
    [Serializable]
    public class PingInfo: IEnity
    {
        public string ClientId { get; set; } = string.Empty;
    }
    [Serializable]
    internal class PingServer:IMessage
    {
        public PingServer()
        {
            this.MessageCode = 1;
        }
    }
    public class PingServerCommand : CommandBase
    {
        public override void DoCommand()
        {
            var pingInfo = this.Message?.GetEnity<PingInfo>();
            if (pingInfo == null) return;
            if (this.Status== ReceiveFrom.Client)  // Process on Client
            {
                this.ClientReceive?.Current?.Center?.OnStatus("Connected");
            }
            if (this.Status == ReceiveFrom.Server)  // Process on Server
            {
                this.ServerReceive?.Current?.SendMessage<PingServer,PingInfo>(pingInfo, this.ServerReceive?.IP);
            }
        }
    }
}
