using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VLib.Network
{
    public class ServerInfo
    {
        public IPEndPoint? External { get; set; }
        public IPEndPoint? Internal { get; set; }
    }
}
