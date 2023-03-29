namespace Server.Forms
{
    partial class FormConnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.korgiButton1 = new Server.Drawing.KorgiButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.id = new System.Windows.Forms.ColumnHeader();
            this.Name = new System.Windows.Forms.ColumnHeader();
            this.IP = new System.Windows.Forms.ColumnHeader();
            this.hwid = new System.Windows.Forms.ColumnHeader();
            this.OS = new System.Windows.Forms.ColumnHeader();
            this.ActivedTime = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // korgiButton1
            // 
            this.korgiButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.korgiButton1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.korgiButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(65)))), ((int)(((byte)(153)))));
            this.korgiButton1.BorderRadius = 15;
            this.korgiButton1.BorderSize = 2;
            this.korgiButton1.FlatAppearance.BorderSize = 0;
            this.korgiButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.korgiButton1.ForeColor = System.Drawing.Color.Gray;
            this.korgiButton1.Location = new System.Drawing.Point(141, 124);
            this.korgiButton1.Name = "korgiButton1";
            this.korgiButton1.Size = new System.Drawing.Size(105, 39);
            this.korgiButton1.TabIndex = 3;
            this.korgiButton1.Text = "Connect";
            this.korgiButton1.TextColor = System.Drawing.Color.Gray;
            this.korgiButton1.UseVisualStyleBackColor = false;
            this.korgiButton1.Click += new System.EventHandler(this.korgiButton1_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(65)))), ((int)(((byte)(153)))));
            this.textBox1.Location = new System.Drawing.Point(40, 74);
            this.textBox1.Name = "textBox1";
            this.textBox1.PlaceholderText = "Connection Host";
            this.textBox1.Size = new System.Drawing.Size(135, 15);
            this.textBox1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(65)))), ((int)(((byte)(153)))));
            this.panel1.Location = new System.Drawing.Point(40, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(135, 2);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(65)))), ((int)(((byte)(153)))));
            this.panel2.Location = new System.Drawing.Point(242, 91);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(80, 2);
            this.panel2.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(65)))), ((int)(((byte)(153)))));
            this.textBox2.Location = new System.Drawing.Point(242, 74);
            this.textBox2.Name = "textBox2";
            this.textBox2.PlaceholderText = "Port";
            this.textBox2.Size = new System.Drawing.Size(80, 15);
            this.textBox2.TabIndex = 6;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.Name,
            this.IP,
            this.hwid,
            this.OS,
            this.ActivedTime});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(285, 115);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(78, 48);
            this.listView1.TabIndex = 11;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // id
            // 
            this.id.Text = "ID";
            this.id.Width = 90;
            // 
            // Name
            // 
            this.Name.Text = "Name";
            this.Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Name.Width = 100;
            // 
            // IP
            // 
            this.IP.Text = "IP";
            this.IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP.Width = 140;
            // 
            // hwid
            // 
            this.hwid.Text = "HWID";
            this.hwid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.hwid.Width = 160;
            // 
            // OS
            // 
            this.OS.Text = "OS Version";
            this.OS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OS.Width = 160;
            // 
            // ActivedTime
            // 
            this.ActivedTime.Text = "Actived Time";
            this.ActivedTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ActivedTime.Width = 150;
            // 
            // FormConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 186);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.korgiButton1);
            //this.Name = "FormConnection";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Connection";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.FormConnection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Drawing.KorgiButton korgiButton1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader Name;
        private System.Windows.Forms.ColumnHeader IP;
        private System.Windows.Forms.ColumnHeader hwid;
        private System.Windows.Forms.ColumnHeader OS;
        private System.Windows.Forms.ColumnHeader ActivedTime;
    }
}