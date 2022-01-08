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
        public virtual void DoCommand()
        {

        }
        public virtual void SetMessage(object mess)
        {

        }
        public virtual void SetCenter(ICenter? Center)
        {

        }
    }
    public class Command<TMessage> : Command
            where TMessage:Message,new()
    {
       protected TMessage? CommandMessage { get; set; }
        protected ICenter? Center { get;  set; }

        public override void SetMessage(object mess)
        {
            this.CommandMessage = (TMessage)mess;
        }
        public override void SetCenter(ICenter? Center)
        {
            this.Center = Center;
        }


    }
}
