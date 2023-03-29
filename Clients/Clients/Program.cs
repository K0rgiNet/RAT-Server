using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clients.Connection;
using Clients.Forms;

namespace Clients
{
    internal class Program
    {
        public static FormChat chat;

        static void Main(string[] args)
        {
            Connection.Connection c = new Connection.Connection();
            c.Connect();

            Console.ReadLine();
        }
    }
}
