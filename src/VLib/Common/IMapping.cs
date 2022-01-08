using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;

namespace VLib.Common
{
    public class IMapping<TCenter>
        where TCenter : ICenter
    {
        public TCenter? Center { get; set; }
        Dictionary<int,Type> map = new Dictionary<int,Type>();
        public Command? GetCommand<TMessage>(TMessage m)
                where TMessage : Message
        {
            var type = map[m.MessageCode];
            if (type == null) return null;
            var objType = Activator.CreateInstance(type);
            if(objType == null) return null;
            var objCommand = (Command)objType;
            if(objCommand == null) return null;
            objCommand.SetCenter(Center);
            objCommand.SetMessage(m);
            return objCommand;
        }
    }
}
