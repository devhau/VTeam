using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Utilities;

namespace VLib.Common
{
    public class Message
    {
        public int MessageCode { get;protected set; }
        public byte[]? MessageData { get; protected set; }
        public void SetData(byte[]? _data)
        {
            this.MessageData = _data;
        }
    }
    public class Message<TEnity> : Message where TEnity : class
    {
        public void SetEnity(TEnity enity)
        {
            if (enity != null)
                this.SetData(BinaryUtils.ObjectToByteArray(enity));
        }
        public TEnity? GetEnity()
        {
            if(MessageData==null) return null;
            return (TEnity?)BinaryUtils.ByteArrayToObject(MessageData);
        }
    }
}
