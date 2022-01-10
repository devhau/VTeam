using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;

namespace VLib.Common
{
    public class Command
    {
        protected IMessage? Message { get; set; } = null;
        public virtual void DoCommand()
        {

        }
        public virtual void SetMessage(object mess)
        {
            this.Message = (IMessage?)mess;
        }
      
    }
}
