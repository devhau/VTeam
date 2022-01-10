using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;

namespace VLib.Common
{
    public class IMapping
    {
        protected bool IsLock { get; private set; }
        public void Lock()
        {
            this.IsLock = true;
        }
       protected readonly Dictionary<int,Type> map = new();
        public Command? GetCommand<TMessage>(TMessage m)
                where TMessage : IMessage
        {
            var type = map[m.MessageCode];
            if (type == null) return null;
            var objType = Activator.CreateInstance(type);
            if(objType == null) return null;
            var objCommand = (Command)objType;
            if(objCommand == null) return null;
            objCommand.SetMessage(m);
            return objCommand;
        }
        public void AddMapping<TType>(object id)
        {
            int id2 = (int)id;
            if (IsLock && id2 < 100) throw new Exception("ID >100");
            map[id2] = typeof(TType);
        }
    }
}
