namespace DissertWindowsFormApplication
{
    partial class FrmMainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainMenu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemUsername = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemUpdateDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuChat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewItemRequestResponses = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.menuItemRequestItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemUsername,
            this.logOutToolStripMenuItem,
            this.menuItemUpdateDetails,
            this.menuItemRequestItem,
            this.mainMenuChat,
            this.menuItemRefresh});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // menuItemUsername
            // 
            this.menuItemUsername.Name = "menuItemUsername";
            resources.ApplyResources(this.menuItemUsername, "menuItemUsername");
            this.menuItemUsername.Click += new System.EventHandler(this.menuItemUsername_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            resources.ApplyResources(this.logOutToolStripMenuItem, "logOutToolStripMenuItem");
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // menuItemUpdateDetails
            // 
            this.menuItemUpdateDetails.Name = "menuItemUpdateDetails";
            resources.ApplyResources(this.menuItemUpdateDetails, "menuItemUpdateDetails");
            this.menuItemUpdateDetails.Click += new System.EventHandler(this.menuItemUpdateDetails_Click);
            // 
            // mainMenuChat
            // 
            this.mainMenuChat.Name = "mainMenuChat";
            resources.ApplyResources(this.mainMenuChat, "mainMenuChat");
            this.mainMenuChat.Click += new System.EventHandler(this.mainMenuChat_Click);
            // 
            // menuItemRefresh
            // 
            this.menuItemRefresh.Name = "menuItemRefresh";
            resources.ApplyResources(this.menuItemRefresh, "menuItemRefresh");
            this.menuItemRefresh.Click += new System.EventHandler(this.menuItemRefresh_Click);
            // 
            // listViewItemRequestResponses
            // 
            resources.ApplyResources(this.listViewItemRequestResponses, "listViewItemRequestResponses");
            this.listViewItemRequestResponses.Name = "listViewItemRequestResponses";
            this.listViewItemRequestResponses.UseCompatibleStateImageBehavior = false;
            this.listViewItemRequestResponses.SelectedIndexChanged += new System.EventHandler(this.listViewItemRequestResponses_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // menuItemRequestItem
            // 
            this.menuItemRequestItem.Name = "menuItemRequestItem";
            resources.ApplyResources(this.menuItemRequestItem, "menuItemRequestItem");
            this.menuItemRequestItem.Click += new System.EventHandler(this.menuItemRequestItem_Click);
            // 
            // FrmMainMenu
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewItemRequestResponses);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMainMenu";
            this.Load += new System.EventHandler(this.FrmMainMenu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ListView listViewItemRequestResponses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ToolStripMenuItem menuItemUsername;
        private System.Windows.Forms.ToolStripMenuItem menuItemUpdateDetails;
        private System.Windows.Forms.ToolStripMenuItem mainMenuChat;
        private System.Windows.Forms.ToolStripMenuItem menuItemRefresh;
        private System.Windows.Forms.ToolStripMenuItem menuItemRequestItem;
    }
}