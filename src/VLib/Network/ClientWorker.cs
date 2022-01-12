using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;

namespace VLib.Network
{
    public abstract class ClientWorker : CenterThread<ClientSystem>
    {
    }
    public class ClientReceiverWorker : ClientWorker
    {
        protected override void DoWork()
        {
        var remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        Center?.ReceiveData(this.Center.Client.Receive(ref remoteIPEndPoint), new ClientReceiveDataInfo()
        {
            Current = this,
            IP = remoteIPEndPoint
        });
        }

    }
}
