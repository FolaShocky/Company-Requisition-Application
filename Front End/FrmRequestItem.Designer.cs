namespace DissertWindowsFormApplication
{
    partial class FrmRequestItem
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
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTxtReason = new System.Windows.Forms.RichTextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblReasonCharacterCount = new System.Windows.Forms.Label();
            this.lblItemNameLength = new System.Windows.Forms.Label();
            this.lblReasonLength = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cBoxAdmin = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemUsername = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(30, 100);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(65, 17);
            this.lblQuantity.TabIndex = 0;
            this.lblQuantity.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(102, 100);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(176, 22);
            this.txtQuantity.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Reason:";
            // 
            // richTxtReason
            // 
            this.richTxtReason.Location = new System.Drawing.Point(102, 155);
            this.richTxtReason.Name = "richTxtReason";
            this.richTxtReason.Size = new System.Drawing.Size(534, 208);
            this.richTxtReason.TabIndex = 4;
            this.richTxtReason.Text = "";
            this.richTxtReason.TextChanged += new System.EventHandler(this.richTxtReason_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(33, 60);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(49, 17);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(102, 60);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(176, 22);
            this.txtName.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblReasonCharacterCount);
            this.groupBox1.Controls.Add(this.lblItemNameLength);
            this.groupBox1.Controls.Add(this.lblReasonLength);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cBoxAdmin);
            this.groupBox1.Controls.Add(this.btnSubmit);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.lblQuantity);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.txtQuantity);
            this.groupBox1.Controls.Add(this.richTxtReason);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(37, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(713, 415);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lblReasonCharacterCount
            // 
            this.lblReasonCharacterCount.AutoSize = true;
            this.lblReasonCharacterCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblReasonCharacterCount.ForeColor = System.Drawing.Color.Red;
            this.lblReasonCharacterCount.Location = new System.Drawing.Point(480, 391);
            this.lblReasonCharacterCount.Name = "lblReasonCharacterCount";
            this.lblReasonCharacterCount.Size = new System.Drawing.Size(170, 17);
            this.lblReasonCharacterCount.TabIndex = 12;
            this.lblReasonCharacterCount.Text = "lblReasonCharacterCount";
            this.lblReasonCharacterCount.Click += new System.EventHandler(this.lblReasonCharacterCount_Click_1);
            // 
            // lblItemNameLength
            // 
            this.lblItemNameLength.AutoSize = true;
            this.lblItemNameLength.Location = new System.Drawing.Point(298, 63);
            this.lblItemNameLength.Name = "lblItemNameLength";
            this.lblItemNameLength.Size = new System.Drawing.Size(129, 17);
            this.lblItemNameLength.TabIndex = 11;
            this.lblItemNameLength.Text = "lblItemNameLength";
            // 
            // lblReasonLength
            // 
            this.lblReasonLength.AutoSize = true;
            this.lblReasonLength.Location = new System.Drawing.Point(480, 369);
            this.lblReasonLength.Name = "lblReasonLength";
            this.lblReasonLength.Size = new System.Drawing.Size(115, 17);
            this.lblReasonLength.TabIndex = 10;
            this.lblReasonLength.Text = "lblReasonLength";
            this.lblReasonLength.Click += new System.EventHandler(this.lblReasonCharacterCount_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Admin:";
            // 
            // cBoxAdmin
            // 
            this.cBoxAdmin.FormattingEnabled = true;
            this.cBoxAdmin.Location = new System.Drawing.Point(102, 21);
            this.cBoxAdmin.Name = "cBoxAdmin";
            this.cBoxAdmin.Size = new System.Drawing.Size(176, 24);
            this.cBoxAdmin.TabIndex = 8;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(313, 364);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(96, 39);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemUsername,
            this.menuItemMainMenu,
            this.menuItemLogOut,
            this.menuItemRefresh});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(821, 28);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // menuItemUsername
            // 
            this.menuItemUsername.Name = "menuItemUsername";
            this.menuItemUsername.Size = new System.Drawing.Size(109, 24);
            this.menuItemUsername.Text = "Logged in as:";
            this.menuItemUsername.Click += new System.EventHandler(this.menuItemUsername_Click);
            // 
            // menuItemMainMenu
            // 
            this.menuItemMainMenu.Name = "menuItemMainMenu";
            this.menuItemMainMenu.Size = new System.Drawing.Size(95, 24);
            this.menuItemMainMenu.Text = "Main Menu";
            this.menuItemMainMenu.Click += new System.EventHandler(this.menuItemMainMenu_Click);
            // 
            // menuItemLogOut
            // 
            this.menuItemLogOut.Name = "menuItemLogOut";
            this.menuItemLogOut.Size = new System.Drawing.Size(74, 24);
            this.menuItemLogOut.Text = "Log Out";
            this.menuItemLogOut.Click += new System.EventHandler(this.menuItemLogOut_Click);
            // 
            // menuItemRefresh
            // 
            this.menuItemRefresh.Name = "menuItemRefresh";
            this.menuItemRefresh.Size = new System.Drawing.Size(70, 24);
            this.menuItemRefresh.Text = "Refresh";
            this.menuItemRefresh.Click += new System.EventHandler(this.menuItemRefresh_Click);
            // 
            // FrmRequestItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 454);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmRequestItem";
            this.Text = "Request Item";
            this.Load += new System.EventHandler(this.FrmRequestItem_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTxtReason;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBoxAdmin;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemLogOut;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainMenu;
        private System.Windows.Forms.Label lblReasonLength;
        private System.Windows.Forms.ToolStripMenuItem menuItemUsername;
        private System.Windows.Forms.Label lblItemNameLength;
        private System.Windows.Forms.Label lblReasonCharacterCount;
        private System.Windows.Forms.ToolStripMenuItem menuItemRefresh;
    }
}