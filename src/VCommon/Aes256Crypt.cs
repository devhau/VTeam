using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLib.Common;
using VLib.Cryptography;

namespace VCommon
{
    internal class Aes256Crypt: ICrypt
    {
        private static readonly string key256= "1B5EDD0F-A72B-4DB2-AD3D-03D7722ABB14";
        public Aes256 aes256;
        public Aes256Crypt()
        {
            aes256 = new Aes256(key256);
        }
        /// <summary>
        /// Decrypt Data Byte[]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] input)
        {
            //return input;
            return aes256.Decrypt(input);
        }
        /// <summary>
        /// Encrypt Data Byte[]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] input)
        {
           // return input;
            return aes256.Encrypt(input);
        }
    }
}
