using Server.Connection;
using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Server.Forms
{
    public partial class FormChat : MetroFramework.Forms.MetroForm
    {
        Client client;
        Socket sck;
        byte[] data;
        string text;

        public FormChat(int id)
        {
            InitializeComponent();
            client = Connection.Listener.clients[id - 1];
            sck = client.sck;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                data = Encoding.UTF8.GetBytes(textBox2.Text);
                data = Connection.Listener.MyDataPacker("MSG", data);
                client.Send(data);
                DataProcess(textBox2.Text, 1);
            }
        }

        public void DataProcess(string text, int who)
        {
            if (who == 0)
            {
                /**   -Risposta dal client-   **/
                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.Text += Environment.NewLine + sck.RemoteEndPoint.ToString() + " >>>  ";
                richTextBox1.SelectionColor = Color.Gray;
                richTextBox1.Text += text;
            }
            else
            {
                /**   -Risposta del server-   **/
                richTextBox1.SelectionColor = Color.Blue;
                richTextBox1.Text += Environment.NewLine + "Korgi " + " >>>  ";
                richTextBox1.SelectionColor = Color.White;
                richTextBox1.Text += text;
            }

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (!string.IsNullOrEmpty(textBox2.Text))
                    {
                        data = Encoding.UTF8.GetBytes(textBox2.Text);
                        data = Connection.Listener.MyDataPacker("MSG", data);
                        client.Send(data);
                        DataProcess(textBox2.Text, 1);

                    }
                }
                catch (Exception) { }
            }
        }
    }
}
