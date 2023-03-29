using Server.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Server.Forms
{
    public partial class FormConnection : MetroFramework.Forms.MetroForm
    {
        public FormConnection()
        {
            InitializeComponent();
        }

        private void korgiButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.PlaceholderText))
            {
                MessageBox.Show("Connection host missed", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Port missed", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int port;
                try
                {
                    port = int.Parse(textBox2.Text);
                    Properties.Settings.Default.host = textBox1.Text;
                    Properties.Settings.Default.port = textBox2.Text;
                    Properties.Settings.Default.Save();

                    Program.korgi.label2.ForeColor = Color.Green;
                    Program.korgi.label2.Text = "Server is listening on " + textBox1.Text + ":" + textBox2.Text;

                    Listener lis = new Listener(Properties.Settings.Default.host, int.Parse(Properties.Settings.Default.port));
                    lis.StartServer();

                    this.Close();

                }
                catch 
                {
                    MessageBox.Show("Invalid Port", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void FormConnection_Load(object sender, EventArgs e)
        {

        }
    }
}
