using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;

namespace VLib.Network
{
    public class ReceiveDataInfo<TReceiverWorker> where TReceiverWorker:class
    {
        public TReceiverWorker? Current { get; set; } = null;
        public IPEndPoint? IP { get; set; }
    }
    public class ServerReceiveDataInfo : ReceiveDataInfo<ServerReceiverWorker> {

    }
    public class ClientReceiveDataInfo : ReceiveDataInfo<ClientReceiverWorker>
    {

    }

}
