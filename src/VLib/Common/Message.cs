using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Utilities;

namespace VLib.Common
{
    [Serializable]
    public class IEnity
    {

    }
    [Serializable]
    public class IMessage
    {
        public IMessage() { }
        public int MessageCode { get;  set; } = 0;
        public byte[]? MessageData { get;  set; } = null;
        public void SetData(byte[]? _data)
        {
            this.MessageData = _data;
        }
        public void SetEnity<TEnity>(TEnity? enity) where TEnity : IEnity,new()
        {
            if (enity != null)
                this.SetData(BinaryUtils.ObjectToByteArray(enity));
            else this.SetData(null);
        }
        public TEnity? GetEnity<TEnity>() where TEnity : IEnity, new()
        {
            if (MessageData == null) return null;
            return (TEnity?)BinaryUtils.ByteArrayToObject(MessageData);
        }
    }
}
