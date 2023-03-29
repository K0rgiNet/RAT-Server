namespace Server.Forms
{
    partial class FormT2s
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
            this.korgiTextBox1 = new Server.Drawing.KorgiTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // korgiTextBox1
            // 
            this.korgiTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.korgiTextBox1.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.korgiTextBox1.BorderFocusColor = System.Drawing.Color.HotPink;
            this.korgiTextBox1.BorderRadius = 0;
            this.korgiTextBox1.BorderSize = 2;
            this.korgiTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.korgiTextBox1.ForeColor = System.Drawing.Color.DimGray;
            this.korgiTextBox1.Location = new System.Drawing.Point(23, 87);
            this.korgiTextBox1.Multiline = false;
            this.korgiTextBox1.Name = "korgiTextBox1";
            this.korgiTextBox1.Padding = new System.Windows.Forms.Padding(7);
            this.korgiTextBox1.PasswordChar = false;
            this.korgiTextBox1.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.korgiTextBox1.PlaceholderText = "";
            this.korgiTextBox1.Size = new System.Drawing.Size(250, 31);
            this.korgiTextBox1.TabIndex = 0;
            this.korgiTextBox1.Texts = "";
            this.korgiTextBox1.UnderlinedStyle = false;
            this.korgiTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.korgiTextBox1_KeyDown);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(65)))), ((int)(((byte)(153)))));
            this.button1.Location = new System.Drawing.Point(279, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 53);
            this.button1.TabIndex = 11;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormT2s
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 174);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.korgiTextBox1);
            this.Name = "FormT2s";
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Text2Speech";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);

        }

        #endregion

        private Drawing.KorgiTextBox korgiTextBox1;
        private System.Windows.Forms.Button button1;
    }
}