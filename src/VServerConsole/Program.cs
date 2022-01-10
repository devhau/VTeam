using System;
using VCommon;
using VLib.Network;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Server.Instance.Message += Instance_Message;
            Server.Instance.Start();
            Console.ReadLine();
        }

        private static void Instance_Message(string obj)
        {
            Console.WriteLine(obj);
        }
    }
}