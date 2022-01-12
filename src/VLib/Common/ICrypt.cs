using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLib.Common
{
    public interface ICrypt
    {
        byte[] Decrypt(byte[] input);
        byte[] Encrypt(byte[] input);
    }
    public class NoneCrypt : ICrypt
    {
        public byte[] Decrypt(byte[] input)
        {
            return input;
        }

        public byte[] Encrypt(byte[] input)
        {
            return input;
        }
    }
}
