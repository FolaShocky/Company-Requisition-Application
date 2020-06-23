namespace DissertWindowsFormApplication
{
    partial class FrmChat
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
            this.listViewChats = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChangeChatName = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewChatMessages = new System.Windows.Forms.ListView();
            this.btnJoinChat = new System.Windows.Forms.Button();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.richTxtMessage = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLeaveChat = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemUsername = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelectChat = new System.Windows.Forms.Button();
            this.listViewChatParticipants = new System.Windows.Forms.ListView();
            this.btnRemoveParticipant = new System.Windows.Forms.Button();
            this.btnDeleteChat = new System.Windows.Forms.Button();
            this.lblSelectedChat = new System.Windows.Forms.Label();
            this.btnPromoteToAdmin = new System.Windows.Forms.Button();
            this.btnDemoteFromAdmin = new System.Windows.Forms.Button();
            this.lblSelectedParticipant = new System.Windows.Forms.Label();
            this.btnSelectParticipant = new System.Windows.Forms.Button();
            this.lblMessageLength = new System.Windows.Forms.Label();
            this.lblMessageMaxLength = new System.Windows.Forms.Label();
            this.txtNewChatName = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateChat
            // 
            this.btnCreateChat.Location = new System.Drawing.Point(343, 413);
            this.btnCreateChat.Name = "btnCreateChat";
            this.btnCreateChat.Size = new System.Drawing.Size(155, 38);
            this.btnCreateChat.TabIndex = 0;
            this.btnCreateChat.Text = "Create New Chat";
            this.btnCreateChat.UseVisualStyleBackColor = true;
            this.btnCreateChat.Click += new System.EventHandler(this.btnCreateChat_Click);
            // 
            // listViewChats
            // 
            this.listViewChats.Location = new System.Drawing.Point(345, 58);
            this.listViewChats.Name = "listViewChats";
            this.listViewChats.Size = new System.Drawing.Size(784, 140);
            this.listViewChats.TabIndex = 1;
            this.listViewChats.UseCompatibleStateImageBehavior = false;
            this.listViewChats.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(340, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "All Chats:";
            // 
            // btnChangeChatName
            // 
            this.btnChangeChatName.Location = new System.Drawing.Point(515, 414);
            this.btnChangeChatName.Name = "btnChangeChatName";
            this.btnChangeChatName.Size = new System.Drawing.Size(163, 37);
            this.btnChangeChatName.TabIndex = 3;
            this.btnChangeChatName.Text = "Change Chat Name";
            this.btnChangeChatName.UseVisualStyleBackColor = true;
            this.btnChangeChatName.Click += new System.EventHandler(this.btnChangeChat_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(345, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Chat Messages:";
            // 
            // listViewChatMessages
            // 
            this.listViewChatMessages.Location = new System.Drawing.Point(344, 265);
            this.listViewChatMessages.Name = "listViewChatMessages";
            this.listViewChatMessages.Size = new System.Drawing.Size(783, 143);
            this.listViewChatMessages.TabIndex = 5;
            this.listViewChatMessages.UseCompatibleStateImageBehavior = false;
            this.listViewChatMessages.SelectedIndexChanged += new System.EventHandler(this.listViewChatContent_SelectedIndexChanged);
            // 
            // btnJoinChat
            // 
            this.btnJoinChat.Location = new System.Drawing.Point(345, 205);
            this.btnJoinChat.Name = "btnJoinChat";
            this.btnJoinChat.Size = new System.Drawing.Size(128, 39);
            this.btnJoinChat.TabIndex = 6;
            this.btnJoinChat.Text = "Join Chat";
            this.btnJoinChat.UseVisualStyleBackColor = true;
            this.btnJoinChat.Click += new System.EventHandler(this.btnJoinChat_Click);
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(342, 583);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(208, 39);
            this.btnSendMessage.TabIndex = 9;
            this.btnSendMessage.Text = "Send Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // richTxtMessage
            // 
            this.richTxtMessage.Location = new System.Drawing.Point(343, 478);
            this.richTxtMessage.Name = "richTxtMessage";
            this.richTxtMessage.Size = new System.Drawing.Size(786, 99);
            this.richTxtMessage.TabIndex = 10;
            this.richTxtMessage.Tag = "";
            this.richTxtMessage.Text = "";
            this.richTxtMessage.TextChanged += new System.EventHandler(this.richTxtMessage_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(342, 458);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Message:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Participants:";
            // 
            // btnLeaveChat
            // 
            this.btnLeaveChat.Location = new System.Drawing.Point(504, 204);
            this.btnLeaveChat.Name = "btnLeaveChat";
            this.btnLeaveChat.Size = new System.Drawing.Size(114, 40);
            this.btnLeaveChat.TabIndex = 13;
            this.btnLeaveChat.Text = "Leave Chat";
            this.btnLeaveChat.UseVisualStyleBackColor = true;
            this.btnLeaveChat.Click += new System.EventHandler(this.btnLeaveChat_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemUsername,
            this.menuItemLogOut,
            this.menuItemMainMenu,
            this.menuItemRefresh});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1191, 28);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
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
            // menuItemMainMenu
            // 
            this.menuItemMainMenu.Name = "menuItemMainMenu";
            this.menuItemMainMenu.Size = new System.Drawing.Size(95, 24);
            this.menuItemMainMenu.Text = "Main Menu";
            this.menuItemMainMenu.Click += new System.EventHandler(this.menuItemActivityMenu_Click);
            // 
            // menuItemRefresh
            // 
            this.menuItemRefresh.Name = "menuItemRefresh";
            this.menuItemRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuItemRefresh.Size = new System.Drawing.Size(70, 24);
            this.menuItemRefresh.Text = "Refresh";
            this.menuItemRefresh.Click += new System.EventHandler(this.menuItemRefresh_Click);
            // 
            // btnSelectChat
            // 
            this.btnSelectChat.Location = new System.Drawing.Point(648, 204);
            this.btnSelectChat.Name = "btnSelectChat";
            this.btnSelectChat.Size = new System.Drawing.Size(154, 40);
            this.btnSelectChat.TabIndex = 15;
            this.btnSelectChat.Text = "Select Chat";
            this.btnSelectChat.UseVisualStyleBackColor = true;
            this.btnSelectChat.Click += new System.EventHandler(this.btnViewMessage_Click);
            // 
            // listViewChatParticipants
            // 
            this.listViewChatParticipants.Location = new System.Drawing.Point(5, 58);
            this.listViewChatParticipants.Name = "listViewChatParticipants";
            this.listViewChatParticipants.Size = new System.Drawing.Size(334, 419);
            this.listViewChatParticipants.TabIndex = 16;
            this.listViewChatParticipants.UseCompatibleStateImageBehavior = false;
            // 
            // btnRemoveParticipant
            // 
            this.btnRemoveParticipant.Location = new System.Drawing.Point(5, 523);
            this.btnRemoveParticipant.Name = "btnRemoveParticipant";
            this.btnRemoveParticipant.Size = new System.Drawing.Size(334, 30);
            this.btnRemoveParticipant.TabIndex = 17;
            this.btnRemoveParticipant.Text = "Remove Participant";
            this.btnRemoveParticipant.UseVisualStyleBackColor = true;
            this.btnRemoveParticipant.Click += new System.EventHandler(this.btnRemoveParticipant_Click);
            // 
            // btnDeleteChat
            // 
            this.btnDeleteChat.Location = new System.Drawing.Point(834, 205);
            this.btnDeleteChat.Name = "btnDeleteChat";
            this.btnDeleteChat.Size = new System.Drawing.Size(101, 39);
            this.btnDeleteChat.TabIndex = 18;
            this.btnDeleteChat.Text = "Delete Chat";
            this.btnDeleteChat.UseVisualStyleBackColor = true;
            this.btnDeleteChat.Click += new System.EventHandler(this.btnDeleteChat_Click);
            // 
            // lblSelectedChat
            // 
            this.lblSelectedChat.AutoSize = true;
            this.lblSelectedChat.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblSelectedChat.Location = new System.Drawing.Point(543, 38);
            this.lblSelectedChat.Name = "lblSelectedChat";
            this.lblSelectedChat.Size = new System.Drawing.Size(100, 17);
            this.lblSelectedChat.TabIndex = 19;
            this.lblSelectedChat.Text = "Selected Chat:";
            // 
            // btnPromoteToAdmin
            // 
            this.btnPromoteToAdmin.Location = new System.Drawing.Point(5, 559);
            this.btnPromoteToAdmin.Name = "btnPromoteToAdmin";
            this.btnPromoteToAdmin.Size = new System.Drawing.Size(334, 29);
            this.btnPromoteToAdmin.TabIndex = 20;
            this.btnPromoteToAdmin.Text = "Promote to Admin";
            this.btnPromoteToAdmin.UseVisualStyleBackColor = true;
            this.btnPromoteToAdmin.Click += new System.EventHandler(this.btnPromoteToAdmin_Click);
            // 
            // btnDemoteFromAdmin
            // 
            this.btnDemoteFromAdmin.Location = new System.Drawing.Point(5, 594);
            this.btnDemoteFromAdmin.Name = "btnDemoteFromAdmin";
            this.btnDemoteFromAdmin.Size = new System.Drawing.Size(334, 29);
            this.btnDemoteFromAdmin.TabIndex = 21;
            this.btnDemoteFromAdmin.Text = "Demote From Admin";
            this.btnDemoteFromAdmin.UseVisualStyleBackColor = true;
            this.btnDemoteFromAdmin.Click += new System.EventHandler(this.btnDemoteFromAdmin_Click);
            // 
            // lblSelectedParticipant
            // 
            this.lblSelectedParticipant.AutoSize = true;
            this.lblSelectedParticipant.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblSelectedParticipant.Location = new System.Drawing.Point(843, 38);
            this.lblSelectedParticipant.Name = "lblSelectedParticipant";
            this.lblSelectedParticipant.Size = new System.Drawing.Size(138, 17);
            this.lblSelectedParticipant.TabIndex = 23;
            this.lblSelectedParticipant.Text = "Selected Participant:";
            // 
            // btnSelectParticipant
            // 
            this.btnSelectParticipant.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSelectParticipant.Location = new System.Drawing.Point(5, 483);
            this.btnSelectParticipant.Name = "btnSelectParticipant";
            this.btnSelectParticipant.Size = new System.Drawing.Size(334, 34);
            this.btnSelectParticipant.TabIndex = 24;
            this.btnSelectParticipant.Text = "Select Participant";
            this.btnSelectParticipant.UseVisualStyleBackColor = true;
            this.btnSelectParticipant.Click += new System.EventHandler(this.btnSelectParticipant_Click);
            // 
            // lblMessageLength
            // 
            this.lblMessageLength.AutoSize = true;
            this.lblMessageLength.ForeColor = System.Drawing.Color.Red;
            this.lblMessageLength.Location = new System.Drawing.Point(1004, 602);
            this.lblMessageLength.Name = "lblMessageLength";
            this.lblMessageLength.Size = new System.Drawing.Size(123, 17);
            this.lblMessageLength.TabIndex = 25;
            this.lblMessageLength.Text = "lblMessageLength";
            // 
            // lblMessageMaxLength
            // 
            this.lblMessageMaxLength.AutoSize = true;
            this.lblMessageMaxLength.Location = new System.Drawing.Point(1004, 580);
            this.lblMessageMaxLength.Name = "lblMessageMaxLength";
            this.lblMessageMaxLength.Size = new System.Drawing.Size(148, 17);
            this.lblMessageMaxLength.TabIndex = 26;
            this.lblMessageMaxLength.Text = "lblMessageMaxLength";
            // 
            // txtNewChatName
            // 
            this.txtNewChatName.Location = new System.Drawing.Point(699, 421);
            this.txtNewChatName.Name = "txtNewChatName";
            this.txtNewChatName.Size = new System.Drawing.Size(198, 22);
            this.txtNewChatName.TabIndex = 27;
            // 
            // FrmChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 628);
            this.Controls.Add(this.txtNewChatName);
            this.Controls.Add(this.lblMessageMaxLength);
            this.Controls.Add(this.lblMessageLength);
            this.Controls.Add(this.btnSelectParticipant);
            this.Controls.Add(this.lblSelectedParticipant);
            this.Controls.Add(this.btnDemoteFromAdmin);
            this.Controls.Add(this.btnPromoteToAdmin);
            this.Controls.Add(this.lblSelectedChat);
            this.Controls.Add(this.btnDeleteChat);
            this.Controls.Add(this.btnRemoveParticipant);
            this.Controls.Add(this.listViewChatParticipants);
            this.Controls.Add(this.btnSelectChat);
            this.Controls.Add(this.btnLeaveChat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTxtMessage);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.btnJoinChat);
            this.Controls.Add(this.listViewChatMessages);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnChangeChatName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewChats);
            this.Controls.Add(this.btnCreateChat);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmChat";
            this.Text = "s";
            this.Load += new System.EventHandler(this.FrmChat_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateChat;
        private System.Windows.Forms.ListView listViewChats;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChangeChatName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewChatMessages;
        private System.Windows.Forms.Button btnJoinChat;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.RichTextBox richTxtMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLeaveChat;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemLogOut;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainMenu;
        private System.Windows.Forms.Button btnSelectChat;
        private System.Windows.Forms.ListView listViewChatParticipants;
        private System.Windows.Forms.Button btnRemoveParticipant;
        private System.Windows.Forms.Button btnDeleteChat;
        private System.Windows.Forms.Label lblSelectedChat;
        private System.Windows.Forms.Button btnPromoteToAdmin;
        private System.Windows.Forms.Button btnDemoteFromAdmin;
        private System.Windows.Forms.Label lblSelectedParticipant;
        private System.Windows.Forms.Button btnSelectParticipant;
        private System.Windows.Forms.ToolStripMenuItem menuItemUsername;
        private System.Windows.Forms.ToolStripMenuItem menuItemRefresh;
        private System.Windows.Forms.Label lblMessageLength;
        private System.Windows.Forms.Label lblMessageMaxLength;
        private System.Windows.Forms.TextBox txtNewChatName;
    }
}