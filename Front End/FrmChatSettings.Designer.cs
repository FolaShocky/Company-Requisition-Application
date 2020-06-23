namespace DissertWindowsFormApplication
{
    partial class FrmChatSettings
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
            this.btnCreateChat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtChatName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemUsername = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemActivityMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChat = new System.Windows.Forms.ToolStripMenuItem();
            this.lblChatNameLength = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateChat
            // 
            this.btnCreateChat.Location = new System.Drawing.Point(350, 80);
            this.btnCreateChat.Name = "btnCreateChat";
            this.btnCreateChat.Size = new System.Drawing.Size(117, 38);
            this.btnCreateChat.TabIndex = 0;
            this.btnCreateChat.Text = "Create Chat";
            this.btnCreateChat.UseVisualStyleBackColor = true;
            this.btnCreateChat.Click += new System.EventHandler(this.btnCreateChat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(241, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chat Name:";
            // 
            // txtChatName
            // 
            this.txtChatName.Location = new System.Drawing.Point(329, 31);
            this.txtChatName.Name = "txtChatName";
            this.txtChatName.Size = new System.Drawing.Size(218, 22);
            this.txtChatName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(553, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "(Letters Only)";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemUsername,
            this.menuItemLogOut,
            this.menuItemActivityMenu,
            this.menuItemChat});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(890, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItemUsername
            // 
            this.menuItemUsername.Name = "menuItemUsername";
            this.menuItemUsername.Size = new System.Drawing.Size(109, 24);
            this.menuItemUsername.Text = "Logged in as:";
            // 
            // menuItemLogOut
            // 
            this.menuItemLogOut.Name = "menuItemLogOut";
            this.menuItemLogOut.Size = new System.Drawing.Size(74, 24);
            this.menuItemLogOut.Text = "Log Out";
            this.menuItemLogOut.Click += new System.EventHandler(this.menuItemLogOut_Click);
            // 
            // menuItemActivityMenu
            // 
            this.menuItemActivityMenu.Name = "menuItemActivityMenu";
            this.menuItemActivityMenu.Size = new System.Drawing.Size(111, 24);
            this.menuItemActivityMenu.Text = "Activity Menu";
            this.menuItemActivityMenu.Click += new System.EventHandler(this.menuItemActivityMeny_Click);
            // 
            // menuItemChat
            // 
            this.menuItemChat.Name = "menuItemChat";
            this.menuItemChat.Size = new System.Drawing.Size(51, 24);
            this.menuItemChat.Text = "Chat";
            this.menuItemChat.Click += new System.EventHandler(this.menuItemChat_Click);
            // 
            // lblChatNameLength
            // 
            this.lblChatNameLength.AutoSize = true;
            this.lblChatNameLength.Location = new System.Drawing.Point(654, 34);
            this.lblChatNameLength.Name = "lblChatNameLength";
            this.lblChatNameLength.Size = new System.Drawing.Size(46, 17);
            this.lblChatNameLength.TabIndex = 5;
            this.lblChatNameLength.Text = "label3";
            // 
            // FrmChatSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 137);
            this.Controls.Add(this.lblChatNameLength);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtChatName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCreateChat);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmChatSettings";
            this.Text = "FrmChatSettings";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateChat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtChatName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemLogOut;
        private System.Windows.Forms.ToolStripMenuItem menuItemActivityMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemUsername;
        private System.Windows.Forms.ToolStripMenuItem menuItemChat;
        private System.Windows.Forms.Label lblChatNameLength;
    }
}