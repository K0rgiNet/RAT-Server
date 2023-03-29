using Server.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Server.Forms
{
    public partial class FormT2s : MetroFramework.Forms.MetroForm
    {
        Client client;
        Socket sck;
        byte[] data;
        string text;

        public FormT2s(int id)
        {
            InitializeComponent();
            client = Connection.Listener.clients[id - 1];
            sck = client.sck;
        }

        private void korgiTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (!string.IsNullOrEmpty(korgiTextBox1.Text))
                    {
                        data = Encoding.UTF8.GetBytes(korgiTextBox1.Text);
                        data = Connection.Listener.MyDataPacker("T2S", data);
                        client.Send(data);
                    }
                }
                catch (Exception) { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(korgiTextBox1.Text))
                {
                    data = Encoding.UTF8.GetBytes(korgiTextBox1.Text);
                    data = Connection.Listener.MyDataPacker("T2S", data);
                    client.Send(data);
                }
            }
            catch (Exception) { }
        }
    }
}
