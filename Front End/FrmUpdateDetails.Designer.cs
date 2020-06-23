namespace DissertWindowsFormApplication
{
    partial class FrmUpdateDetails
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
            this.btnUpdateInfo = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemUsername = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.btnDeletePersonalAccount = new System.Windows.Forms.Button();
            this.cBoxRegularUsers = new System.Windows.Forms.ComboBox();
            this.btnDeleteRegularUser = new System.Windows.Forms.Button();
            this.btnPromoteToAdmin = new System.Windows.Forms.Button();
            this.lblFirstNameLength = new System.Windows.Forms.Label();
            this.lblSurnameLength = new System.Windows.Forms.Label();
            this.lblUsernameLength = new System.Windows.Forms.Label();
            this.lblPasswordLength = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUpdateInfo
            // 
            this.btnUpdateInfo.Location = new System.Drawing.Point(333, 359);
            this.btnUpdateInfo.Name = "btnUpdateInfo";
            this.btnUpdateInfo.Size = new System.Drawing.Size(133, 44);
            this.btnUpdateInfo.TabIndex = 0;
            this.btnUpdateInfo.Text = "Update Info";
            this.btnUpdateInfo.UseVisualStyleBackColor = true;
            this.btnUpdateInfo.Click += new System.EventHandler(this.btnUpdateInfo_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(297, 250);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(255, 22);
            this.txtPassword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 253);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Username:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(297, 203);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(255, 22);
            this.txtUsername.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Surname:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(213, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "First Name:";
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(297, 146);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(255, 22);
            this.txtSurname.TabIndex = 7;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(297, 94);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(255, 22);
            this.txtFirstName.TabIndex = 8;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemUsername,
            this.menuItemLogOut,
            this.mainMenuToolStripMenuItem,
            this.menuItemChat,
            this.menuItemRefresh});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1086, 28);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItemUsername
            // 
            this.menuItemUsername.Name = "menuItemUsername";
            this.menuItemUsername.Size = new System.Drawing.Size(109, 24);
            this.menuItemUsername.Text = "Logged in as:";
            this.menuItemUsername.Click += new System.EventHandler(this.menuItemUsername_Click);
            // 
            // menuItemLogOut
            // 
            this.menuItemLogOut.Name = "menuItemLogOut";
            this.menuItemLogOut.Size = new System.Drawing.Size(74, 24);
            this.menuItemLogOut.Text = "Log Out";
            this.menuItemLogOut.Click += new System.EventHandler(this.menuItemLogOut_Click);
            // 
            // mainMenuToolStripMenuItem
            // 
            this.mainMenuToolStripMenuItem.Name = "mainMenuToolStripMenuItem";
            this.mainMenuToolStripMenuItem.Size = new System.Drawing.Size(95, 24);
            this.mainMenuToolStripMenuItem.Text = "Main Menu";
            this.mainMenuToolStripMenuItem.Click += new System.EventHandler(this.mainMenuToolStripMenuItem_Click);
            // 
            // menuItemChat
            // 
            this.menuItemChat.Name = "menuItemChat";
            this.menuItemChat.Size = new System.Drawing.Size(51, 24);
            this.menuItemChat.Text = "Chat";
            this.menuItemChat.Click += new System.EventHandler(this.menuItemChat_Click);
            // 
            // menuItemRefresh
            // 
            this.menuItemRefresh.Name = "menuItemRefresh";
            this.menuItemRefresh.Size = new System.Drawing.Size(70, 24);
            this.menuItemRefresh.Text = "Refresh";
            this.menuItemRefresh.Click += new System.EventHandler(this.menuItemRefresh_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(168, 300);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Confirm Password:";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(297, 300);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(255, 22);
            this.txtConfirmPassword.TabIndex = 15;
            // 
            // btnDeletePersonalAccount
            // 
            this.btnDeletePersonalAccount.Location = new System.Drawing.Point(844, 203);
            this.btnDeletePersonalAccount.Name = "btnDeletePersonalAccount";
            this.btnDeletePersonalAccount.Size = new System.Drawing.Size(121, 57);
            this.btnDeletePersonalAccount.TabIndex = 16;
            this.btnDeletePersonalAccount.Text = "Delete Personal Account";
            this.btnDeletePersonalAccount.UseVisualStyleBackColor = true;
            this.btnDeletePersonalAccount.Click += new System.EventHandler(this.btnDeletePersonalAccount_Click);
            // 
            // cBoxRegularUsers
            // 
            this.cBoxRegularUsers.FormattingEnabled = true;
            this.cBoxRegularUsers.Location = new System.Drawing.Point(768, 94);
            this.cBoxRegularUsers.Name = "cBoxRegularUsers";
            this.cBoxRegularUsers.Size = new System.Drawing.Size(253, 24);
            this.cBoxRegularUsers.TabIndex = 17;
            // 
            // btnDeleteRegularUser
            // 
            this.btnDeleteRegularUser.Location = new System.Drawing.Point(900, 130);
            this.btnDeleteRegularUser.Name = "btnDeleteRegularUser";
            this.btnDeleteRegularUser.Size = new System.Drawing.Size(121, 49);
            this.btnDeleteRegularUser.TabIndex = 18;
            this.btnDeleteRegularUser.Text = "Delete Selected User";
            this.btnDeleteRegularUser.UseVisualStyleBackColor = true;
            this.btnDeleteRegularUser.Click += new System.EventHandler(this.btnDeleteRegularUser_Click);
            // 
            // btnPromoteToAdmin
            // 
            this.btnPromoteToAdmin.Location = new System.Drawing.Point(768, 130);
            this.btnPromoteToAdmin.Name = "btnPromoteToAdmin";
            this.btnPromoteToAdmin.Size = new System.Drawing.Size(126, 49);
            this.btnPromoteToAdmin.TabIndex = 19;
            this.btnPromoteToAdmin.Text = "Promote To Admin";
            this.btnPromoteToAdmin.UseVisualStyleBackColor = true;
            this.btnPromoteToAdmin.Click += new System.EventHandler(this.btnPromoteToAdmin_Click);
            // 
            // lblFirstNameLength
            // 
            this.lblFirstNameLength.AutoSize = true;
            this.lblFirstNameLength.Location = new System.Drawing.Point(561, 97);
            this.lblFirstNameLength.Name = "lblFirstNameLength";
            this.lblFirstNameLength.Size = new System.Drawing.Size(46, 17);
            this.lblFirstNameLength.TabIndex = 20;
            this.lblFirstNameLength.Text = "label6";
            // 
            // lblSurnameLength
            // 
            this.lblSurnameLength.AutoSize = true;
            this.lblSurnameLength.Location = new System.Drawing.Point(561, 150);
            this.lblSurnameLength.Name = "lblSurnameLength";
            this.lblSurnameLength.Size = new System.Drawing.Size(46, 17);
            this.lblSurnameLength.TabIndex = 21;
            this.lblSurnameLength.Text = "label7";
            // 
            // lblUsernameLength
            // 
            this.lblUsernameLength.AutoSize = true;
            this.lblUsernameLength.Location = new System.Drawing.Point(561, 203);
            this.lblUsernameLength.Name = "lblUsernameLength";
            this.lblUsernameLength.Size = new System.Drawing.Size(46, 17);
            this.lblUsernameLength.TabIndex = 22;
            this.lblUsernameLength.Text = "label8";
            // 
            // lblPasswordLength
            // 
            this.lblPasswordLength.AutoSize = true;
            this.lblPasswordLength.Location = new System.Drawing.Point(561, 253);
            this.lblPasswordLength.Name = "lblPasswordLength";
            this.lblPasswordLength.Size = new System.Drawing.Size(46, 17);
            this.lblPasswordLength.TabIndex = 23;
            this.lblPasswordLength.Text = "label9";
            // 
            // FrmUpdateDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 481);
            this.Controls.Add(this.lblPasswordLength);
            this.Controls.Add(this.lblUsernameLength);
            this.Controls.Add(this.lblSurnameLength);
            this.Controls.Add(this.lblFirstNameLength);
            this.Controls.Add(this.btnPromoteToAdmin);
            this.Controls.Add(this.btnDeleteRegularUser);
            this.Controls.Add(this.cBoxRegularUsers);
            this.Controls.Add(this.btnDeletePersonalAccount);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.txtSurname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnUpdateInfo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmUpdateDetails";
            this.Text = "Update Details";
            this.Load += new System.EventHandler(this.FrmUpdateDetails_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateInfo;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemLogOut;
        private System.Windows.Forms.ToolStripMenuItem menuItemUsername;
        private System.Windows.Forms.ToolStripMenuItem mainMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemChat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnDeletePersonalAccount;
        private System.Windows.Forms.ComboBox cBoxRegularUsers;
        private System.Windows.Forms.Button btnDeleteRegularUser;
        private System.Windows.Forms.Button btnPromoteToAdmin;
        private System.Windows.Forms.Label lblFirstNameLength;
        private System.Windows.Forms.Label lblSurnameLength;
        private System.Windows.Forms.Label lblUsernameLength;
        private System.Windows.Forms.Label lblPasswordLength;
        private System.Windows.Forms.ToolStripMenuItem menuItemRefresh;
    }
}