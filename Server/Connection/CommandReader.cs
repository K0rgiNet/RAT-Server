using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Server.Connection
{
    class CommandReader
    {
        public static void DataProcess(Socket sck, string tag, byte[] dataBuff, Client client)
        {
            try
            {
                string text = Encoding.UTF8.GetString(dataBuff);
                switch (tag.Split('|')[0])
                {
                    case "<SCCS>":
                        HandleForm.WriteTextBox(sck, text);
                        break;
                    case "<INFO>":
                        HandleForm.Insert(text);
                        HandleForm.WriteTextBox(sck, text);
                        break;
                    case "<STMSG>":
                        HandleForm.WriteTextBox(sck, text);
                        break;
                    case "<NSCCS>":
                        HandleForm.WriteTextBox(sck, text);
                        break;
                    case "<MSG>":
                        HandleForm.WriteTextBox(sck, text);
                        HandleForm.WriteTextBoxChat(sck, text);
                        break;
                    case "<TSK>":
                        string[] tasks = text.Split(new string[] { "[KORGIZ]" }, StringSplitOptions.None);
                        HandleForm.WriteTextBox(sck, text);
                        HandleForm.Task(tasks);
                        break;
                }
            }
            catch { }
        }
    }
}
