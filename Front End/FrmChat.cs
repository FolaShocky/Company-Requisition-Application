using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using System.IO;
using WebApplication1.Models;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Diagnostics;
using static WebApplication1.Models.Constants;
using System.Text.RegularExpressions;
namespace DissertWindowsFormApplication
{
    public partial class FrmChat : Form
    {
        private static FrmChat FrmChatInstance;
        private int txtMessageLength;
        private FrmChat()
        {
            InitializeComponent();
            CreateHandle();
            Text = "Chat";
            listViewChats.MultiSelect = false;
            listViewChats.View = View.Details;
            listViewChats.FullRowSelect = true;

            listViewChats.ForeColor = Color.White;

            listViewChatMessages.MultiSelect = false;
            listViewChatMessages.View = View.Details;
            listViewChatMessages.FullRowSelect = true;

            listViewChatParticipants.MultiSelect = false;
            listViewChatParticipants.View = View.Details;
            listViewChatParticipants.FullRowSelect = true;

            lblMessageLength.Invoke(new Action(() =>
            {
                txtMessageLength = RetrieveFieldMaxCharacterLength(Db_Field_Message, Db_Table_Message);
                richTxtMessage.MaxLength = txtMessageLength;

                lblMessageLength.Text = $"0/{txtMessageLength}";
                lblMessageMaxLength.Text = $"Max Length: {txtMessageLength} chars";
            }));
        }

        private int RetrieveFieldMaxCharacterLength(string fieldName, string tableName)
        {
            using (HttpWebResponse httpWebResponse =
                WebRequest.Create($"{Constants.Server_String}/Item/RetrieveFieldMaxCharacterLength?fieldName={fieldName}&tableName={tableName}").GetResponse() as HttpWebResponse)
            {
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    int characterMaxLength = 0;
                    while (!streamReader.EndOfStream)
                    {
                        characterMaxLength = int.Parse(streamReader.ReadLine());
                    }
                    return characterMaxLength;
                }
            }
        }


        public void RefreshGui()
        {
            InitListViewChats();
            InitListViewMessages();
            InitListViewParticipants();

            List<Chat> chatList = RetrieveCompanyChats();
            List<ChatParticipant> chatParticipantList = new List<ChatParticipant>();
            chatList.ForEach(chat => Console.WriteLine($"Chat:{chat.ToString()}"));
            chatList.Sort(new Chat());
            int index = 0;
            Console.WriteLine($"ChatList count: {chatList.Count}");
            chatList.ForEach(chat =>
            {
                StringBuilder chatAdminStringBuilder = new StringBuilder();
                chat.ChatUserList.ForEach(chatUser => 
                {
                    if (chatParticipantList.Exists(chatParticipant => chatParticipant.Type.Equals(Db_Entry_Admin)
                    && chatParticipant.ChatParticipantUser.Id.Equals(chatUser.Id)))
                    {
                        chatAdminStringBuilder.Append(chatUser.Username);
                        chatAdminStringBuilder.Append("\n");
                    }
                });
                FrmChatInstance.listViewChats.Items.Add(
                        new ListViewItem(
                            new[] {
                                chat.ChatName,
                                chat.ChatUserList.Count.ToString(),
                                chat.ChatCreationDate.ToString()
                            }
                        )
                    );
                FrmChatInstance.listViewChats.Items[index].BackColor = index % 2 == 0 ? Color.Blue : Color.Red;
                index++;
            });
            FrmChatInstance.listViewChats.Refresh();
            SetLabelSelectedChatText();
            SetLabelSelectedParticipantText();
        }
        public static FrmChat GetInstance()
        {
            if (FrmChatInstance == null)
            {
                FrmChatInstance = new FrmChat();
                FrmChatInstance.FormClosed += new FormClosedEventHandler(delegate (object sender, FormClosedEventArgs e) {
                    Application.Exit();
                });
            }
            User loggedInUser = Utility.LoggedInUser;
            FrmChatInstance.menuItemUsername.Text = loggedInUser != null ? $"Logged in as: {loggedInUser.Username}" : "Not Logged In";
            FrmChatInstance.richTxtMessage.Text = string.Empty;
            return FrmChatInstance;
        }

        private List<ChatParticipant> RetrieveChatParticipants(Guid chatId)
        {
            try
            {
                using(HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/ChatParticipant/RetrieveChatParticipantsByChatId?chatId={chatId.ToString()}")
                    .GetResponse() as HttpWebResponse)
                {
                    List<ChatParticipant> chatParticipantList = new List<ChatParticipant>();
                    using(StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        while (!streamReader.EndOfStream)
                        {
                            chatParticipantList.Add(javaScriptSerializer.Deserialize<ChatParticipant>(streamReader.ReadLine()));
                        }
                        chatParticipantList.Sort(new ChatParticipant());
                        return chatParticipantList;
                    }
                }
            }
            catch(WebException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        private void InitListViewMessages()
        {
            listViewChatMessages.Clear();
            listViewChatMessages.Columns.Add("Chat Name");
            listViewChatMessages.Columns.Add("Message");
            listViewChatMessages.Columns.Add("Sender");
            listViewChatMessages.Columns.Add("Date Sent");
        }

        private void InitListViewParticipants()
        {
            listViewChatParticipants.Clear();
            listViewChatParticipants.Columns.Add("Chat Name");
            listViewChatParticipants.Columns.Add("Username");
            listViewChatParticipants.Columns.Add("Chat Role");
            listViewChatParticipants.Columns.Add("Company Role");
        }

        public void InitListViewChats()
        {
            listViewChats.Clear();
            listViewChats.Columns.Add("Chat Name");
            listViewChats.Columns.Add("Participant Count");
            listViewChats.Columns.Add("Creation Date");
        }
        public List<Chat> RetrieveCompanyChats()
        {
            try
            {
                List<Chat> chatList = new List<Chat>();
                using (HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/Chat/RetrieveChatsByCompanyId?companyId={Utility.LoggedInUser.CompanyId.ToString()}")
                    .GetResponse() as HttpWebResponse)
                {
                    using(StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        while (!streamReader.EndOfStream)
                        {
                            Chat retrievedChat = javaScriptSerializer.Deserialize<Chat>(streamReader.ReadLine());
                            retrievedChat.ChatUserList.Sort(new User());
                            chatList.Add(retrievedChat);
                        }
                        chatList.Sort(new Chat());
                        return chatList;
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"An exception occurred:{ex.Message}");
                return null;
            }
        }

        private void btnCreateChat_Click(object sender, EventArgs e)
        {
            Hide();
            FrmChatSettings frmChatSettings = FrmChatSettings.GetInstance();
            frmChatSettings.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)//listView1 is listViewChats
        {
             
        }

        private void FrmChat_Load(object sender, EventArgs e)
        {
            
        }

        private void btnChangeChat_Click(object sender, EventArgs e)
        {
            btnChangeChatName.Invoke(new Action(() =>
            {
                if (Utility.SelectedChat != null)
                {
                    Chat selectedChat = Utility.SelectedChat;
                    User loggedInUser = Utility.LoggedInUser;
                    List<ChatParticipant> chatParticipantList = RetrieveChatParticipants(selectedChat.ChatId);
                    try
                    {
                        if (chatParticipantList.Exists(chatParticipant =>
                         chatParticipant.ChatParticipantUser.Id.Equals(Utility.LoggedInUser.Id) &&
                         chatParticipant.Type.Equals(Db_Entry_Admin)))
                        {
                            string newChatName = txtNewChatName.Text.Trim();
                            if (Regex.IsMatch(newChatName, @"^[a-zA-Z]+$"))
                            {
                                using (HttpWebResponse httpWebMessage = WebRequest.Create(
                                    $"{Constants.Server_String}/Chat/UpdateChat?chatId={selectedChat.ChatId.ToString()}" +
                                $"&chatAdminId={loggedInUser.Id.ToString()}&companyId={loggedInUser.CompanyId.ToString()}" +
                                $"&chatName={newChatName}" +
                                $"&chatCreationDate={selectedChat.ChatCreationDate.ToString("yyyy-MM-dd hh:mm:ss")}").GetResponse()
                                as HttpWebResponse)
                                {
                                    using (Stream webStream = httpWebMessage.GetResponseStream())
                                    {
                                        webStream.Flush();
                                        RefreshGui();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("The name must be non-blank and only contain letters");
                            }
                        }
                        else
                        {
                             MessageBox.Show("You cannot change this chat's name as you are not its admin");
                        }
                    }
                    catch (WebException ex)
                    {
                        Debug.WriteLine($"An exception occurred: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a chat and ensure you are its admin");
                }
            }));
        }


        private void JoinChat(bool isAdmin)
        {
            try
            {
                List<Chat> chatList = RetrieveCompanyChats();
                if (Utility.SelectedChat != null)
                {
                    List<ChatParticipant> retrievedChatParticipantList = RetrieveChatParticipants(Utility.SelectedChat.ChatId);
                    if (!retrievedChatParticipantList.Exists(chatParticipant => chatParticipant.ChatParticipantUser.Id.Equals(Utility.LoggedInUser.Id)))
                    {
                        try
                        {
                            string chatUserType = isAdmin ? Constants.Db_Entry_Regular_User : Constants.Db_Entry_Admin;
                            using (HttpWebResponse httpWebResponse = WebRequest.Create(
                                $"{Constants.Server_String}/ChatParticipant/CreateChatParticipant?" +
                                $"chatId={Utility.SelectedChat.ChatId.ToString()}" +
                                $"&userId={Utility.LoggedInUser.Id.ToString()}&type=" +
                                $"{chatUserType}").GetResponse() as HttpWebResponse)
                            {
                                using (Stream webStream = httpWebResponse.GetResponseStream())
                                {
                                    webStream.Flush();
                                    RefreshGui();
                                    MessageBox.Show("You have joined the chat");
                                }
                            }
                        }
                        catch (WebException ex)
                        {
                            Console.WriteLine($"An exception occurred:{ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("You are already part of this chat");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a chat to join");
                }
            }
            catch(WebException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
            }
        }
        private void btnJoinChat_Click(object sender, EventArgs e)
        {
            btnJoinChat.Invoke(new Action(() =>
            {
                Chat selectedChat = Utility.SelectedChat;
                if (selectedChat != null)
                {
                    if (!selectedChat.ChatUserList.Exists(chatUser => chatUser.Id.Equals(Utility.LoggedInUser.Id)))
                    {
                        List<ChatParticipant> chatParticipantList = RetrieveChatParticipants(selectedChat.ChatId);
                        JoinChat(chatParticipantList.Any(chatParticipant => chatParticipant.Type.Equals(Constants.Db_Entry_Admin)));
                    }
                    else
                    {
                        MessageBox.Show("You are already a part of this chat");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a chat first");
                }
            }));
        }
        private void SetLabelSelectedChatText()//Sets the text of the widget named 'lblSelectedChat'
        {
            Chat selectedChat = Utility.SelectedChat;
            lblSelectedChat.Text = selectedChat != null ? $"Selected Chat: {selectedChat.ChatName.ToString()}" :
                "No chat selected";
        }
        private void SetLabelSelectedParticipantText()
        {
            ChatParticipant chatParticipant = Utility.SelectedChatParticipant;
            lblSelectedParticipant.Text = chatParticipant != null ? $"Selected Participant: {chatParticipant.ChatParticipantUser.Username.ToString()}":
                "No participant selected";
        }
        private void listViewChatContent_SelectedIndexChanged(object sender, EventArgs e)
        {
             
        }

        private void dataGridViewChatParticipants_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            btnSendMessage.Invoke(new Action(() =>
            {
                if (Utility.SelectedChat != null)
                {
                    Chat selectedChat = Utility.SelectedChat;

                    if (selectedChat.ChatUserList.Exists(chatUser => chatUser.Id.Equals(Utility.LoggedInUser.Id)))
                    {
                        try
                        {
                            using (HttpWebResponse httpWebResponse = WebRequest.Create(
                                $"{Constants.Server_String}/ChatMessage/CreateChatMessage?chatId={selectedChat.ChatId.ToString()}" +
                                $"&messageId={Guid.NewGuid().ToString()}&messageSenderId={Utility.LoggedInUser.Id.ToString()}" +
                                $"&message={richTxtMessage.Text.Trim()}&messageCreationDate={DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}")
                                .GetResponse() as HttpWebResponse)
                            {
                                using (Stream stream = httpWebResponse.GetResponseStream())
                                {
                                    stream.Flush();
                                    RefreshGui();
                                }
                            }
                        }
                        catch (WebException ex)
                        {
                            Console.WriteLine($"An exception occurred: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("In order to send a message, you must be a part of the chat ");
                    }
                }
                else
                {
                    MessageBox.Show("Before sending a message, please select a chat, and then click \"View chat\"");
                }
            }));
        }

        private void btnLeaveChat_Click(object sender, EventArgs e)
        {
            btnLeaveChat.Invoke(new Action(() =>
            {
                if (Utility.SelectedChat != null)
                {
                    Chat selectedChat = Utility.SelectedChat;
                    List<ChatParticipant> retrievedChatParticipantsList = RetrieveChatParticipants(selectedChat.ChatId);
                    if (!retrievedChatParticipantsList.Exists(
                        retrievedChatPartipant => retrievedChatPartipant.ChatParticipantChat.ChatId.Equals(Utility.LoggedInUser.Id)))
                    {
                        try
                        {
                            using (HttpWebResponse httpWebResponse = WebRequest.Create(
                                $"{Constants.Server_String}/ChatParticipant/DeleteChatParticipant?" +
                                $"chatId={selectedChat.ChatId.ToString()}" +
                                $"&userId={Utility.LoggedInUser.Id.ToString()}").GetResponse() as HttpWebResponse)
                            {
                                using (Stream webStream = httpWebResponse.GetResponseStream())
                                {
                                    webStream.Flush();
                                    RefreshGui();
                                    MessageBox.Show($"You have left the chat {selectedChat.ChatName}");
                                }
                            }
                        }
                        catch (WebException ex)
                        {
                            Debug.WriteLine($"An exception occurred: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("You were not part of this chat initially");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a chat to leave");
                }
            }));
        }

        private void menuItemActivityMenu_Click(object sender, EventArgs e)//Now named menuItemMainMenu
        {
            Hide();
            if (Utility.LoggedInUser.Type.Equals(Constants.User_Type_Regular_User))
            {
                FrmMainMenu frmMainMenu = FrmMainMenu.GetInstance();
                frmMainMenu.Show();
            }
            else
            {
                FrmMainMenuAdmin frmMainMenuAdmin = FrmMainMenuAdmin.GetInstance();
                frmMainMenuAdmin.Show();
            }
        }

        private void menuItemLogOut_Click(object sender, EventArgs e)
        {
            if (Utility.LoggedInUser != null)
            {
                Utility.LoggedInUser = null;
                Utility.SelectedChat = null;
                Utility.SelectedChatParticipant = null;
                MessageBox.Show("Successfully logged out");
                Hide();
                FrmEntry frmEntry = FrmEntry.GetInstance();
                frmEntry.Show();
            }
            else
                MessageBox.Show("Already logged out");
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private string DisplayType(string type)
        {
            return type.Equals(Db_Entry_Regular_User) ? "Regular User" : "Admin";
        }

        private void btnViewMessage_Click(object sender, EventArgs e)//Now called btnSelectChat
        {
            if (listViewChats.SelectedItems.Count > 0)
            {
                btnSelectChat.Invoke(new Action(() =>
                {
                    Debug.WriteLine($"Item Index: {listViewChats.SelectedIndices[0]} ");
                     
                    InitListViewMessages();
                    InitListViewParticipants();
                    List<Chat> chatList = RetrieveCompanyChats();
                    
                    Chat selectedChat = chatList[listViewChats.SelectedItems[0].Index];
                    Utility.SelectedChat = selectedChat;
                    List<ChatParticipant> chatParticipantList = RetrieveChatParticipants(selectedChat.ChatId);
                    InitListViewMessages();

                    if (selectedChat.ChatUserList.Exists(chatUser => chatUser.Id.Equals(Utility.LoggedInUser.Id)))
                    {
                        selectedChat.ChatMessageList.Sort(new ChatMessage());

                        selectedChat.ChatMessageList.ForEach(chatMessage =>
                        {
                            Debug.WriteLine(chatMessage.ToString());
                            listViewChatMessages.Items.Add(
                                new ListViewItem(new[]
                                {
                                selectedChat.ChatName,
                                chatMessage.Message,
                                chatMessage.MessageSender.Username,
                                chatMessage.MessageCreationDate.ToString()
                                })
                            );
                        });
                    }
                    else
                    {
                        listViewChatMessages.Items.Add(new ListViewItem(
                            new[] {
                                "Join the chat to view its messages",
                                "Join the chat to view its messages",
                                "Join the chat to view its messages",
                                "Join the chat to view its messages"
                            })
                            );
                    }
                    listViewChatMessages.Refresh();
                    selectedChat.ChatUserList.ForEach(chatUser =>
                    {
                        ChatParticipant chatParticipant = chatParticipantList.Find(
                            _chatParticipant => _chatParticipant.ChatParticipantUser.Id.Equals(chatUser.Id));
                        Debug.WriteLine($"Participant:{chatParticipant.ToString()}");
                        listViewChatParticipants.Items.Add(new ListViewItem(new[]
                        {
                            selectedChat.ChatName,
                            chatUser.Username,
                            DisplayType(chatParticipant.Type),
                            DisplayType(chatUser.Type)
                        }));
                    });
                    listViewChatParticipants.Refresh();
                    SetLabelSelectedChatText();
                    SetLabelSelectedParticipantText();
                }));
            }
            else
            {
                MessageBox.Show("Please select a chat from the list");
            }
        }

        
        private void btnRemoveParticipant_Click(object sender, EventArgs e)
        {
            if (Utility.SelectedChat != null)
            {
                Chat selectedChat = Utility.SelectedChat;
                List<ChatParticipant> chatParticipantList = RetrieveChatParticipants(selectedChat.ChatId);
                Debug.WriteLine($"Participant: {Utility.SelectedChatParticipant.ToString()}");
                btnRemoveParticipant.Invoke(new Action(() =>
                {
                    if (chatParticipantList.Exists(
                        chatParticipant => chatParticipant.ChatParticipantUser.Id.Equals(Utility.LoggedInUser.Id)
                        && chatParticipant.Type.Equals(Db_Entry_Admin)))
                    {
                        try
                        {
                            ChatParticipant chatParticipantForDeletion = Utility.SelectedChatParticipant;
                            using (HttpWebResponse httpWebResponse = WebRequest.Create(
                                $"{Constants.Server_String}/ChatParticipant/DeleteChatParticipant?" +
                                $"chatId={chatParticipantForDeletion.ChatParticipantChat.ChatId.ToString()}" +
                                $"&userId={chatParticipantForDeletion.ChatParticipantUser.Id.ToString()}")
                                .GetResponse() as HttpWebResponse)
                            {
                                Debug.WriteLine("Initiated response");
                                using (Stream webStream = httpWebResponse.GetResponseStream())
                                {
                                    webStream.Flush();
                                    RefreshGui();
                                    MessageBox.Show("Successfully removed chat's participant");
                                }
                            }
                        }
                        catch(WebException ex)
                        {
                            Debug.WriteLine($"An exception occurred: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a participant belonging to a group for which you are an admin");
                    }
                }));
            }
            else
            {
                MessageBox.Show("Please select a participant to delete");
            }
        }

        private void btnDeleteChat_Click(object sender, EventArgs e)
        {
            if (Utility.SelectedChat != null)
            {
                btnDeleteChat.Invoke(new Action(() =>
                {
                    Chat selectedChat = Utility.SelectedChat;
                    List<ChatParticipant> chatParticipantList = RetrieveChatParticipants(selectedChat.ChatId);
                    
                    if (chatParticipantList.Exists(
                        chatParticipant => chatParticipant.ChatParticipantUser.Id.Equals(Utility.LoggedInUser.Id)
                    && chatParticipant.Type.Equals(Db_Entry_Admin)))
                    {
                        try
                        {
                            using (HttpWebResponse httpWebResponse = WebRequest.Create(
                                $"{Constants.Server_String}/Chat/DeleteChatByChatId?chatId={selectedChat.ChatId.ToString()}").GetResponse() as HttpWebResponse)
                            {
                                using (Stream webStream = httpWebResponse.GetResponseStream())
                                {
                                    webStream.Flush();
                                    RefreshGui();
                                    MessageBox.Show("Successfully deleted chat");
                                }
                            }
                        }
                        catch(WebException ex)
                        {
                            Debug.WriteLine($"An exception occurred: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot delete this chat as you are not its admin");
                    }
                }));
            }
            else
            {
                MessageBox.Show("Please select a chat");
            }
        }
        
        private List<Role> RetrieveRolesByCompanyId(Guid companyId)
        {
            try
            {
                using(HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/Role/RetrieveRolesByCompanyId?companyId={companyId.ToString()}").GetResponse() as HttpWebResponse)
                {
                    List<Role> roleList = new List<Role>();
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        while (!streamReader.EndOfStream)
                        {
                            roleList.Add(javaScriptSerializer.Deserialize<Role>(streamReader.ReadLine()));
                        }
                        return roleList;
                    }
                }
            }
            catch(WebException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }
        private void CreateRole(string roleAction, string roleType)
        {
            try
            {
                using (HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/Role/CreateRole?roleId={Guid.NewGuid().ToString()}" +
                    $"&userId={Utility.SelectedChatParticipant.ChatParticipantUser.Id.ToString()}" +
                    $"&companyId={Utility.LoggedInUser.CompanyId.ToString()}"+
                    $"&roleAction={roleAction}&roleType={roleType}" +
                    $"&chatId={Utility.SelectedChat.ChatId.ToString()}" +
                    $"&endorsingChatAdminId={Utility.LoggedInUser.Id.ToString()}").GetResponse() as HttpWebResponse)
                {
                    using(Stream webStream = httpWebResponse.GetResponseStream())
                    {
                        webStream.Flush();
                    }
                }
            }
            catch(WebException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
            }
        }

        private void UpdateRole(string roleAction, string roleType, int endorsementCount)
        {
            try
            {
                using (HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/Role/UpdateRole?roleId={Guid.NewGuid().ToString()}" +
                    $"&userId={Utility.SelectedChatParticipant.ChatParticipantUser.Id.ToString()}" +
                    $"&companyId={Utility.LoggedInUser.CompanyId.ToString()}&roleAction={roleAction}&roleType={roleType}" +
                    $"&chatId={Utility.SelectedChat.ChatId.ToString()}&endorsingChatAdminId={Utility.LoggedInUser.Id}")
                    .GetResponse() as HttpWebResponse)
                {
                    using (Stream webStream = httpWebResponse.GetResponseStream())
                    {
                        webStream.Flush();
                    }
                }
            }
            catch (WebException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
            }
        }

        private void UpdateUserRole(Role roleForDeletion)
        {
            ChatParticipant selectedChatParticipant = Utility.SelectedChatParticipant;
            if(roleForDeletion == null)//A promotion occurs
                selectedChatParticipant.Type = Constants.User_Type_Admin;
            else
            {
                //A demotion occurs
                if(roleForDeletion.EndorsingChatAdminList.Count >= 2)
                    selectedChatParticipant.Type = Constants.User_Type_Regular_User;
            }
                
            using (HttpWebResponse httpWebResponse = WebRequest.Create(
            $"{Constants.Server_String}/ChatParticipant/UpdateChatParticipant?" +
            $"chatId={selectedChatParticipant.ChatParticipantChat.ChatId.ToString()}" +
            $"&userId={selectedChatParticipant.ChatParticipantUser.Id.ToString()}" +
            $"&type={selectedChatParticipant.Type}")
            .GetResponse() as HttpWebResponse)
            {
                using (Stream webStream = httpWebResponse.GetResponseStream())
                {
                    webStream.Flush();
                }
            }
            if (roleForDeletion != null)
            {
                Debug.WriteLine($"Company Id:{Utility.LoggedInUser.CompanyId.ToString()}");
                using (HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/Role/DeleteRole?roleId={roleForDeletion.RoleId.ToString()}" +
                    $"&userId={Utility.LoggedInUser.Id.ToString()}&companyId={Utility.LoggedInUser.CompanyId.ToString()}" +
                    $"&roleAction={roleForDeletion.RoleAction}&roleType={roleForDeletion.RoleType}" +
                    $"&chatId={roleForDeletion.RoleChat.ChatId.ToString()}").GetResponse() as HttpWebResponse)
                {
                    using (Stream webStream = httpWebResponse.GetResponseStream())
                    {
                        webStream.Flush();
                    }
                }
            }
        }
        private void ExecuteRoleChange(string roleAction)
        {
            Button clickedButton = roleAction.CompareTo(Db_Const_Promotion) == 0 ? btnPromoteToAdmin : btnDemoteFromAdmin;

            clickedButton.Invoke(new Action(() => {

            List<Role> roleList = RetrieveRolesByCompanyId(Utility.LoggedInUser.CompanyId);
            ChatParticipant selectedChatParticipant = Utility.SelectedChatParticipant;
            // A check for an identical 'ChatId' is no longer necessary as we are retrieving by 'ChatId', as indicated by the preceding method's name 
            //Any role for this user
            
            if (roleAction.Equals(Db_Const_Promotion))//Roles for users getting promoted are not created
            {
                Debug.WriteLine("\n\nDoes not Exist\n\n");
                ////In other words, checks if there exists any role for this user
                UpdateUserRole(null);//We have no interest in a role for this particular invocation
                    MessageBox.Show(
                        $"{Utility.SelectedChatParticipant.ChatParticipantUser.Username} has been promoted to a chat admin");

            }
            else //Roles for users getting demoted are created
            {
                Role retrievedRole = roleList.Find(_role =>
                _role.RoleUser.Id.Equals(Utility.SelectedChatParticipant.ChatParticipantUser.Id)
                 && _role.RoleChat.ChatId.Equals(Utility.SelectedChat.ChatId));
                 
                 if(retrievedRole!=null)
                 {
                     retrievedRole.RoleType = Db_Const_Chat;
                     if (!retrievedRole.EndorsingChatAdminList.Exists(chatAdmin => chatAdmin.Id.Equals(Utility.LoggedInUser.Id)))
                     {
                         retrievedRole.EndorsingChatAdminList.Add(Utility.LoggedInUser);
                         UpdateUserRole(retrievedRole);

                     }
                     else
                     {
                         MessageBox.Show("You have already endorsed this user");
                     }
                 }
                 else
                 {
                     MessageBox.Show("1 more endorsement is required for the demotion of this user");
                     CreateRole(Db_Const_Demotion, Db_Const_Chat);
                 }
            }
         }));
           
            
        }

        private void btnPromoteToAdmin_Click(object sender, EventArgs e)
        {
            if (Utility.SelectedChatParticipant != null)
            {
                List<ChatParticipant> chatParticipantList = RetrieveChatParticipants(Utility.SelectedChat.ChatId);

                if (Utility.LoggedInUser.Equals(Db_Entry_Admin) || (chatParticipantList.Exists(chatParticipant =>
                chatParticipant.ChatParticipantUser.Id.Equals(Utility.LoggedInUser.Id) && chatParticipant.Type.Equals(Db_Entry_Admin))))
                    ExecuteRoleChange(Db_Const_Promotion);
                else
                    MessageBox.Show("You must be a system or chat admin to use this functionality");
            }
            else
            {
                MessageBox.Show("Please select a chat participant");
            }
        }

        private void btnDemoteFromAdmin_Click(object sender, EventArgs e)
        {
            if (Utility.SelectedChatParticipant != null)
            {
                List<ChatParticipant> chatParticipantList = RetrieveChatParticipants(Utility.SelectedChat.ChatId);

                if (Utility.LoggedInUser.Equals(Db_Entry_Admin) || (chatParticipantList.Exists(chatParticipant =>
                chatParticipant.ChatParticipantUser.Id.Equals(Utility.LoggedInUser.Id) && chatParticipant.Type.Equals(Db_Entry_Admin))))
                {
                    
                    ExecuteRoleChange(Db_Const_Demotion);
                }
                else
                    MessageBox.Show("You must be a system or chat admin to use this functionality");
            }
            else
            {
                MessageBox.Show("Please select a chat participant");
            }
        }

        private void btnSelectParticipant_Click(object sender, EventArgs e)
        {
            if (Utility.SelectedChat != null)
            {
                if (listViewChatParticipants.SelectedItems.Count > 0)
                {
                    List<ChatParticipant> chatParticipantList = RetrieveChatParticipants(Utility.SelectedChat.ChatId);
                    Utility.SelectedChatParticipant = chatParticipantList[listViewChatParticipants.SelectedItems[0].Index];
                    SetLabelSelectedParticipantText();
                }
                else
                {
                    MessageBox.Show("Please select a chat participant");
                }
            }
            else
            {
                MessageBox.Show("Please select a chat first");
            }
        }

        private void menuItemRefresh_Click(object sender, EventArgs e)
        {
            RefreshGui();
        }

        private void richTxtMessage_TextChanged(object sender, EventArgs e)
        {
            lblMessageLength.Text = $"{richTxtMessage.Text.Length}/{txtMessageLength}";
        }
    }
}
