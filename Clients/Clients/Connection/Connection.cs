using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Clients.Connection
{
    class Connection
    {
        public static Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        string ip = "127.0.0.1";
        int port = 6606;
        private byte[] dataByte = new byte[8192]; // 1024 * 8
        private int blockSize = 8192;
        MemoryStream memos = new MemoryStream();

        public Connection()
        {
            sock.ReceiveBufferSize = int.MaxValue; sock.SendBufferSize = int.MaxValue;
            sock.ReceiveTimeout = -1; sock.SendTimeout = -1;
            sock.NoDelay = true;
        }

        public void Connect()
        {
            int attempts = 0;
            while (!sock.Connected)
            {
                try
                {
                    attempts++;
                    Console.WriteLine("Connection attempts " + attempts);
                    sock.Connect(ip, port);
                }
                catch (SocketException)
                {
                    Console.Clear();
                }
            }
            Console.WriteLine("Client Connected to " + sock.RemoteEndPoint.ToString());

            RequestLoop();
        }

        private void RequestLoop()
        {
            while (true)
            {
                ReceiveLoop();
            }
        }

        /**   - Receive Data From CLient -   **/
        private void ReceiveLoop()
        {
            int rec;
            while (true)
            {
                rec = sock.Receive(dataByte, 0, blockSize, SocketFlags.None);
                if(rec > 0)
                {
                    if(memos != null)
                    {
                        memos.Write(dataByte, 0, rec);
                        UnPacker(memos);
                    }
                }
            }
        }



        public static void Send(byte[] data)
        {
            sock.Send(data, 0, data.Length, SocketFlags.None);
        }





        // Packer and Unpacker
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

        private void UnPacker(MemoryStream ms)
        {
            //This unpacker coded by qH0sT' - 2021 - AndroSpy.
            //string letter = "qwertyuıopğüasdfghjklşizxcvbnmöç1234567890<>|";
            Regex regex = new Regex(@"<[A-Z]+>\|[0-9]+\|.*>");

            byte[][] filebytes = Separate(ms.ToArray(), System.Text.Encoding.UTF8.GetBytes("SUFFIX"));
            for (int k = 0; k < filebytes.Length; k++)
            {
                if (!(filebytes[k].Length <= 0))
                {
                    try
                    {
                        string ch = System.Text.Encoding.UTF8.GetString(new byte[1] { filebytes[k][filebytes[k].Length - 1] });// >
                        string f = System.Text.Encoding.UTF8.GetString(new byte[1] { filebytes[k][filebytes[k].Length - 2] });// F>
                        string o = System.Text.Encoding.UTF8.GetString(new byte[1] { filebytes[k][filebytes[k].Length - 3] });// OF>
                        string e = System.Text.Encoding.UTF8.GetString(new byte[1] { filebytes[k][filebytes[k].Length - 4] });// EOF>
                        string ch_ = System.Text.Encoding.UTF8.GetString(new byte[1] { filebytes[k][filebytes[k].Length - 5] });// <EOF>

                        bool isContainsEof = (ch_ + e + o + f + ch) == "<EOF>";
                        if (isContainsEof)
                        {
                            List<byte> mytagByte = new List<byte>();
                            string temp = "";
                            for (int p = 0; p < filebytes[k].Length; p++)
                            {
                                temp += System.Text.Encoding.UTF8.GetString(new byte[1] { filebytes[k][p] });
                                mytagByte.Add(filebytes[k][p]);
                                if (regex.IsMatch(temp))
                                {
                                    break;
                                }
                            }
                            string whatsTag = System.Text.Encoding.UTF8.GetString(mytagByte.ToArray());

                            MemoryStream tmpMemory = new MemoryStream();
                            tmpMemory.Write(filebytes[k], 0, filebytes[k].Length);
                            tmpMemory.Write(System.Text.Encoding.UTF8.GetBytes("SUFFIX"), 0, System.Text.Encoding.UTF8.GetBytes("SUFFIX").Length);
                            ms.Flush();
                            ms.Close();
                            ms.Dispose();
                            ms = new MemoryStream(RemoveBytes(ms.ToArray(), tmpMemory.ToArray()));
                            memos = new MemoryStream();
                            ms.CopyTo(memos);
                            tmpMemory.Flush();
                            tmpMemory.Close();
                            tmpMemory.Dispose();
                            filebytes[k] = RemoveBytes(filebytes[k], mytagByte.ToArray());
                            filebytes[k] = RemoveBytes(filebytes[k], System.Text.Encoding.UTF8.GetBytes("<EOF>"));


                            CommandReaderClient.DataProcess(whatsTag, filebytes[k]); // Process our datas as tag and buffer data.

                        }
                    }
                    catch (Exception) { }
                }
            }
        }

        private byte[][] Separate(byte[] source, byte[] separator)
        {
            var Parts = new List<byte[]>();
            var Index = 0;
            byte[] Part;
            for (var I = 0; I < source.Length; ++I)
            {
                if (Equals(source, separator, I))
                {
                    Part = new byte[I - Index];
                    Array.Copy(source, Index, Part, 0, Part.Length);
                    Parts.Add(Part);
                    Index = I + separator.Length;
                    I += separator.Length - 1;
                }
            }
            Part = new byte[source.Length - Index];
            Array.Copy(source, Index, Part, 0, Part.Length);
            Parts.Add(Part);
            return Parts.ToArray();
        }
        private bool Equals(byte[] source, byte[] separator, int index)
        {
            for (int i = 0; i < separator.Length; ++i)
                if (index + i >= source.Length || source[index + i] != separator[i])
                    return false;
            return true;
        }
        private byte[] RemoveBytes(byte[] input, byte[] pattern)
        {
            if (pattern.Length == 0) return input;
            var result = new List<byte>();
            for (int i = 0; i < input.Length; i++)
            {
                var patternLeft = i <= input.Length - pattern.Length;
                if (patternLeft && (!pattern.Where((t, j) => input[i + j] != t).Any()))
                {
                    i += pattern.Length - 1;
                }
                else
                {
                    result.Add(input[i]);
                }
            }
            return result.ToArray();
        }

    }
}
