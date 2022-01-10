using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLib.Pattern
{
    public class Singleton<TEnity> where TEnity : class, new()
    {
        private static readonly Lazy<TEnity> lazy = new(() => new TEnity());
        public static TEnity Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}
