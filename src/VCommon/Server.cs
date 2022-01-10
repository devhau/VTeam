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
        public IServer()
        {
            this.AddMapping<Mapping>();
        }
    }
    public class Server : Singleton<IServer> { 
        
    }

}
