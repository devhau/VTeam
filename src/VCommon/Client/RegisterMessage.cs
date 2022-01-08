using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;

namespace VCommon.Client
{
    public class RegisterInfo
    {
        public string ID { get; set; }
        public string Pass { get; set; }
    }
    internal class RegisterMessage: Message
    {
    }
}
