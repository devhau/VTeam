using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLib.Common
{
    public abstract class Messagable
    {
        public event Action<string>? Message;

        public virtual void ShowMessage(string msg)
        {
            Message?.Invoke(msg);
        }
    }
}
