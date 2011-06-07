namespace LabWakeUp
{
    partial class MainForm
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
            this.Shutdown_Server = new System.Windows.Forms.Button();
            this.Start_Server = new System.Windows.Forms.Button();
            this.ServerName = new System.Windows.Forms.ComboBox();
            this.LableServerName = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Shutdown_Server
            // 
            this.Shutdown_Server.Location = new System.Drawing.Point(11, 94);
            this.Shutdown_Server.Name = "Shutdown_Server";
            this.Shutdown_Server.Size = new System.Drawing.Size(115, 25);
            this.Shutdown_Server.TabIndex = 0;
            this.Shutdown_Server.Text = "Shutdown Server";
            this.Shutdown_Server.UseVisualStyleBackColor = true;
            this.Shutdown_Server.Click += new System.EventHandler(this.Shutdown_Server_Click);
            // 
            // Start_Server
            // 
            this.Start_Server.Location = new System.Drawing.Point(163, 94);
            this.Start_Server.Name = "Start_Server";
            this.Start_Server.Size = new System.Drawing.Size(115, 25);
            this.Start_Server.TabIndex = 1;
            this.Start_Server.Text = "Start Server";
            this.Start_Server.UseVisualStyleBackColor = true;
            this.Start_Server.Click += new System.EventHandler(this.Start_Server_Click);
            // 
            // ServerName
            // 
            this.ServerName.FormattingEnabled = true;
            this.ServerName.Location = new System.Drawing.Point(15, 25);
            this.ServerName.Name = "ServerName";
            this.ServerName.Size = new System.Drawing.Size(287, 21);
            this.ServerName.TabIndex = 2;
            // 
            // LableServerName
            // 
            this.LableServerName.AutoSize = true;
            this.LableServerName.Location = new System.Drawing.Point(12, 9);
            this.LableServerName.Name = "LableServerName";
            this.LableServerName.Size = new System.Drawing.Size(69, 13);
            this.LableServerName.TabIndex = 3;
            this.LableServerName.Text = "Server Name";
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(12, 68);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(145, 20);
            this.userName.TabIndex = 6;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(163, 68);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(139, 20);
            this.password.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Password";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 136);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.LableServerName);
            this.Controls.Add(this.ServerName);
            this.Controls.Add(this.Start_Server);
            this.Controls.Add(this.Shutdown_Server);
            this.Name = "MainForm";
            this.Text = "Lab Sleep/Wake up";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Shutdown_Server;
        private System.Windows.Forms.Button Start_Server;
        private System.Windows.Forms.ComboBox ServerName;
        private System.Windows.Forms.Label LableServerName;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;


    }
}

