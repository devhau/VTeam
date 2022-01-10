using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;

namespace VLib.Network
{
    public class CommandBase: Command
    {
        public enum ReceiveFrom
        {
            None,
            Server,
            Client
        }
        public ReceiveFrom Status { get; private set; } = ReceiveFrom.None;
        protected ClientReceiveDataInfo? ClientReceive { get; set; } = null;
        protected ServerReceiveDataInfo? ServerReceive { get; set; } = null;
        public void SetReceiveData(object data)
        {
            if (data == null) return;

            if (data is ClientReceiveDataInfo)
            {
                this.Status = ReceiveFrom.Client;
                this.ClientReceive = (ClientReceiveDataInfo)data;
            }
            else if (data is ServerReceiveDataInfo)
            {
                this.Status = ReceiveFrom.Server;
                this.ServerReceive = (ServerReceiveDataInfo)data;
            }
        }
    }
}
