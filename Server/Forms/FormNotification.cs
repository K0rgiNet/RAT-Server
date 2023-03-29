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
    public partial class FormNotification : Form
    {
        int i = 0;

        public FormNotification(string title, string text)
        {
            InitializeComponent();
            this.label1.Text = title;
            this.label2.Text = text;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            if (i == 3)
            {
                this.Close();
            }
        }

        private void Area()
        {
            int posx = 0, posy = 0;
            posx = Screen.GetWorkingArea(this).Width;
            posy = Screen.GetWorkingArea(this).Height;
            this.Location = new Point(posx - this.Width, posy - this.Height - 25);
        }

        private void FormNotification_Load(object sender, EventArgs e)
        {
            Area();
            timer1.Start();
        }
    }
}
