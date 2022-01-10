using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;
using VLib.Utilities;

namespace VLib.Network
{
    public class ServerSystem : Center<ServerWorker, ServerSystem>
    {
        public List<int> Ports = new();
        public ServerSystem():this("2515,2516,2517,2518,2519,2520")
        {

        }
        public ServerSystem(string ports)
        {
            _ = ports.Split(',').All(item =>
            {
                Ports.Add(int.Parse(item));
                this.AddWorker<ServerReceiverWorker>((t) => t.Port = int.Parse(item));
                return true;
            });
        }
        public virtual void ReceiveData(byte[] data, ServerReceiveDataInfo centerReceiver)
        {
            var msg = (IMessage)BinaryUtils.ByteArrayToObject(data);
            var command = Mapping?.GetCommand(msg);
            if (command != null)
            {
                if (command is CommandBase)
                {
                    ((CommandBase)command).SetReceiveData(centerReceiver);
                }
                command.DoCommand();
            }
        }
    }
}
