using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;

namespace VLib.Network
{
    public class MappingBase: IMapping
    {
        public MappingBase()
        {
            this.AddMapping<PingServerCommand>(1);
            this.Lock() ;
        }
    }
}
