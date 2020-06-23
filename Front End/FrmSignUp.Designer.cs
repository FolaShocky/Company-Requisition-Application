namespace DissertWindowsFormApplication
{
    partial class FrmSignUp
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
            this.btnSignUp = new System.Windows.Forms.Button();
            this.cBoxCompanyName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNewCompany = new System.Windows.Forms.Label();
            this.txtNewCompany = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFirstNameLength = new System.Windows.Forms.Label();
            this.lblSurnameLength = new System.Windows.Forms.Label();
            this.lblUsernameLength = new System.Windows.Forms.Label();
            this.lblPasswordLength = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCompanyNameLength = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSignUp
            // 
            this.btnSignUp.Location = new System.Drawing.Point(338, 447);
            this.btnSignUp.Name = "btnSignUp";
            this.btnSignUp.Size = new System.Drawing.Size(160, 56);
            this.btnSignUp.TabIndex = 0;
            this.btnSignUp.Text = "Sign Up";
            this.btnSignUp.UseVisualStyleBackColor = true;
            this.btnSignUp.Click += new System.EventHandler(this.btnSignUp_Click_1);
            // 
            // cBoxCompanyName
            // 
            this.cBoxCompanyName.FormattingEnabled = true;
            this.cBoxCompanyName.Location = new System.Drawing.Point(357, 38);
            this.cBoxCompanyName.Name = "cBoxCompanyName";
            this.cBoxCompanyName.Size = new System.Drawing.Size(196, 24);
            this.cBoxCompanyName.TabIndex = 1;
            this.cBoxCompanyName.SelectedIndexChanged += new System.EventHandler(this.cBoxCompanyName_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(239, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Company Name:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(376, 293);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(177, 22);
            this.txtPassword.TabIndex = 7;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(296, 296);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(73, 17);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(376, 239);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(177, 22);
            this.txtUsername.TabIndex = 5;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(296, 242);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(77, 17);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Username:";
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(376, 182);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(177, 22);
            this.txtSurname.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(296, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Surname:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(376, 131);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(177, 22);
            this.txtFirstName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(293, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "FirstName:";
            // 
            // lblNewCompany
            // 
            this.lblNewCompany.AutoSize = true;
            this.lblNewCompany.Location = new System.Drawing.Point(290, 385);
            this.lblNewCompany.Name = "lblNewCompany";
            this.lblNewCompany.Size = new System.Drawing.Size(102, 17);
            this.lblNewCompany.TabIndex = 4;
            this.lblNewCompany.Text = "New Company:";
            // 
            // txtNewCompany
            // 
            this.txtNewCompany.Location = new System.Drawing.Point(398, 382);
            this.txtNewCompany.Name = "txtNewCompany";
            this.txtNewCompany.Size = new System.Drawing.Size(168, 22);
            this.txtNewCompany.TabIndex = 5;
            this.txtNewCompany.TextChanged += new System.EventHandler(this.txtNewCompany_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemLogin,
            this.menuItemRefresh});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(941, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // menuItemLogin
            // 
            this.menuItemLogin.Name = "menuItemLogin";
            this.menuItemLogin.Size = new System.Drawing.Size(62, 24);
            this.menuItemLogin.Text = "Log In";
            this.menuItemLogin.Click += new System.EventHandler(this.menuItemLogin_Click);
            // 
            // menuItemRefresh
            // 
            this.menuItemRefresh.Name = "menuItemRefresh";
            this.menuItemRefresh.Size = new System.Drawing.Size(70, 24);
            this.menuItemRefresh.Text = "Refresh";
            this.menuItemRefresh.Click += new System.EventHandler(this.menuItemRefresh_Click);
            // 
            // lblFirstNameLength
            // 
            this.lblFirstNameLength.AutoSize = true;
            this.lblFirstNameLength.Location = new System.Drawing.Point(580, 134);
            this.lblFirstNameLength.Name = "lblFirstNameLength";
            this.lblFirstNameLength.Size = new System.Drawing.Size(46, 17);
            this.lblFirstNameLength.TabIndex = 8;
            this.lblFirstNameLength.Text = "label4";
            // 
            // lblSurnameLength
            // 
            this.lblSurnameLength.AutoSize = true;
            this.lblSurnameLength.Location = new System.Drawing.Point(580, 187);
            this.lblSurnameLength.Name = "lblSurnameLength";
            this.lblSurnameLength.Size = new System.Drawing.Size(46, 17);
            this.lblSurnameLength.TabIndex = 9;
            this.lblSurnameLength.Text = "label4";
            // 
            // lblUsernameLength
            // 
            this.lblUsernameLength.AutoSize = true;
            this.lblUsernameLength.Location = new System.Drawing.Point(580, 242);
            this.lblUsernameLength.Name = "lblUsernameLength";
            this.lblUsernameLength.Size = new System.Drawing.Size(46, 17);
            this.lblUsernameLength.TabIndex = 10;
            this.lblUsernameLength.Text = "label4";
            // 
            // lblPasswordLength
            // 
            this.lblPasswordLength.AutoSize = true;
            this.lblPasswordLength.Location = new System.Drawing.Point(583, 296);
            this.lblPasswordLength.Name = "lblPasswordLength";
            this.lblPasswordLength.Size = new System.Drawing.Size(46, 17);
            this.lblPasswordLength.TabIndex = 11;
            this.lblPasswordLength.Text = "label4";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(376, 340);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(177, 22);
            this.txtConfirmPassword.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(244, 343);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(125, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Confirm Password:";
            // 
            // lblCompanyNameLength
            // 
            this.lblCompanyNameLength.AutoSize = true;
            this.lblCompanyNameLength.Location = new System.Drawing.Point(580, 385);
            this.lblCompanyNameLength.Name = "lblCompanyNameLength";
            this.lblCompanyNameLength.Size = new System.Drawing.Size(162, 17);
            this.lblCompanyNameLength.TabIndex = 14;
            this.lblCompanyNameLength.Text = "lblCompanyNameLength";
            // 
            // FrmSignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 524);
            this.Controls.Add(this.lblCompanyNameLength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.lblPasswordLength);
            this.Controls.Add(this.txtNewCompany);
            this.Controls.Add(this.lblUsernameLength);
            this.Controls.Add(this.lblNewCompany);
            this.Controls.Add(this.lblSurnameLength);
            this.Controls.Add(this.lblFirstNameLength);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.cBoxCompanyName);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.btnSignUp);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.txtSurname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmSignUp";
            this.Text = "FrmSignUp";
            this.Load += new System.EventHandler(this.FrmSignUp_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSignUp;
        private System.Windows.Forms.ComboBox cBoxCompanyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblNewCompany;
        private System.Windows.Forms.TextBox txtNewCompany;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemLogin;
        private System.Windows.Forms.Label lblSurnameLength;
        private System.Windows.Forms.Label lblFirstNameLength;
        private System.Windows.Forms.Label lblUsernameLength;
        private System.Windows.Forms.Label lblPasswordLength;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCompanyNameLength;
        private System.Windows.Forms.ToolStripMenuItem menuItemRefresh;
    }
}