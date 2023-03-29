using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clients.Forms
{
    class HandleForm
    {

        public static void WriteTextBoxChat(string msg)
        {
            try
            {
                if (Program.chat.InvokeRequired)
                {
                    Program.chat.Invoke((MethodInvoker)(() =>
                    {
                        Program.chat.richTextBox1.SelectionColor = Color.Red;
                        Program.chat.richTextBox1.SelectedText = Environment.NewLine + "Korgi" + " : ";
                        Program.chat.richTextBox1.SelectionColor = Color.Black;
                        Program.chat.richTextBox1.SelectedText = msg;
                    }));
                }
                else
                {
                    Program.chat.richTextBox1.SelectionColor = Color.Red;
                    Program.chat.richTextBox1.SelectedText = Environment.NewLine + "Korgi" + " : ";
                    Program.chat.richTextBox1.SelectionColor = Color.Black;
                    Program.chat.richTextBox1.SelectedText = msg;
                }
            }
            catch
            {

            }
        }

    }
}
