using System;
using VLib.Network;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CenterSystem.Instance= new CenterSystem();
            CenterSystem.Instance.Message += Instance_Message;
            CenterSystem.Instance.Start();
            Console.ReadLine();
        }

        private static void Instance_Message(string obj)
        {
            Console.WriteLine(obj);
        }
    }
}