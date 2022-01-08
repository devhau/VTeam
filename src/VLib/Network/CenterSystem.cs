using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;

namespace VLib.Network
{
    public class CenterSystem : Center<CenterWorker, CenterSystem>
    {
        public List<int> Ports = new();
        public CenterSystem():this("2515,2516,2517,2518,2519,2520")
        {

        }
        public CenterSystem(string ports)
        {
            _ = ports.Split(',').All(item =>
            {
                Ports.Add(int.Parse(item));
                this.AddWorker<CenterReceiverWorker>((t) => t.Port = int.Parse(item));
                return true;
            });
            this.SetInstance();
        }
        public void ReceiveData(byte[] data,CenterReceiverWorker centerReceiver)
        {
            this.ShowMessage("Đang nhận dữ liệu từ Port:"+centerReceiver.Port);
            this.ShowMessage("Đang nhận dữ liệu từ Client:" + centerReceiver.remoteIPEndPoint);
            this.ShowMessage(Encoding.Unicode.GetString(data));
            centerReceiver.SendText("Đã nhận được", centerReceiver.remoteIPEndPoint);
        }
    }
}
