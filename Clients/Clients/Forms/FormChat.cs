using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clients.Forms
{
    public partial class FormChat : Form
    {
        byte[] data;
        string testo;
        public FormChat()
        {
            InitializeComponent();
        }

        private void FormChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool sccs = false;
            if(!sccs)
                e.Cancel = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                data = Encoding.UTF8.GetBytes(textBox1.Text);
                data = Connection.Connection.MyDataPacker("MSG", data);
                Connection.Connection.Send(data);
            }
        }

        public void DataProcess(string text)
        {
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.Text += Environment.NewLine + "Server" + " >>>  ";
            richTextBox1.SelectionColor = Color.White;
            richTextBox1.Text += text;
        }
    }
}
