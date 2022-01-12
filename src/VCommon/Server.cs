using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Network;
using VLib.Pattern;

namespace VCommon
{
    public class IServer: ServerSystem
    {
        public Dictionary<string, ServerReceiveDataInfo> Clients  = new Dictionary<string, ServerReceiveDataInfo>();
        public Dictionary<string, List<ServerReceiveDataInfo>> ShareRemote = new Dictionary<string, List<ServerReceiveDataInfo>>();
        public IServer()
        {
            this.AddMapping<Mapping>();
          //  this.AddCrypt<Aes256Crypt>();
        }
    }
    public class Server : Singleton<IServer> { 
        
    }

}
