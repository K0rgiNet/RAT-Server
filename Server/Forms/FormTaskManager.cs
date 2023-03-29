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
    public partial class FormTaskManager : MetroFramework.Forms.MetroForm
    {
        Client client;

        public FormTaskManager(int id)
        {
            InitializeComponent();
            client = Listener.clients[id-1];
        }
    }
}
