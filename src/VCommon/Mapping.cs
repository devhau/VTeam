using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCommon.Commands;
using VLib.Common;
using VLib.Network;

namespace VCommon
{
    public class Mapping: MappingBase
    {
        public Mapping()
        {
            this.AddMapping<ConnectToServerCommand>(CommandCode.ConnectToServer);
        }
    }
}
