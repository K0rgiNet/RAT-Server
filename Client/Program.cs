using Client.Connection;
using System;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectClient cc = new ConnectClient();
            cc.SetupClient();
            Console.ReadLine();
        }
    }
}
