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
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using static WebApplication1.Models.Constants;
namespace DissertWindowsFormApplication
{
    public partial class FrmChatSettings : Form
    {
        private List<string> retrievedChatNameList;
        private static FrmChatSettings FrmChatSettingsInstance;
        private FrmChatSettings()
        {
            InitializeComponent();
            CreateHandle();
            Text = "Chat Settings";
            
            lblChatNameLength.Invoke(new Action(() => {
                int chatNameLength = RetrieveMaxCharacterLength(Db_Field_Chat_Name, Db_Table_Chat);
                lblChatNameLength.Text = $"(Max Length: {chatNameLength} chars)";
                txtChatName.MaxLength = chatNameLength;
            }));
        } 
       
        public static FrmChatSettings GetInstance()
        {
            if (FrmChatSettingsInstance == null)
                FrmChatSettingsInstance = new FrmChatSettings();
            FrmChatSettingsInstance.FormClosed += new FormClosedEventHandler(delegate (object sender, FormClosedEventArgs e)
            {
                Application.Exit();
            });
            FrmChatSettingsInstance.Invoke(new Action(() => 
                FrmChatSettingsInstance.retrievedChatNameList = FrmChatSettingsInstance.RetrieveChatNames()
            ));
            return FrmChatSettingsInstance;
        }

        private int RetrieveMaxCharacterLength(string fieldName, string tableName)
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

        private List<string> RetrieveChatNames()
        {
            try
            {
                List<string> chatNameList = new List<string>();
                using(HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/Chat/RetrieveChatsByCompanyId?companyId={Utility.LoggedInUser.CompanyId}")
                    .GetResponse() as HttpWebResponse)
                {
                    using(StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        while (!streamReader.EndOfStream)
                        {
                            chatNameList.Add(javaScriptSerializer.Deserialize<Chat>(streamReader.ReadLine()).ChatName);
                        }
                        return chatNameList;
                    }
                }
            }
            catch(WebException ex)
            {
                Console.WriteLine($"An exception occurred:{ex.Message}");
                return null;
            }
        }
        private void btnCreateChat_Click(object sender, EventArgs e)
        {
            string chatName = txtChatName.Text.Trim();
            Regex chatNameRegex = new Regex(@"^[a-zA-Z]+$");
            if (chatNameRegex.IsMatch(chatName) && !string.IsNullOrEmpty(chatName))
            {
                if (!retrievedChatNameList.Exists(retrievedChatName => retrievedChatName.ToLower().Equals(chatName.ToLower())))
                {
                    btnCreateChat.Invoke(new Action(() =>
                    {
                        try
                        {
                            User user = Utility.LoggedInUser;
                            using (HttpWebResponse httpWebResponse = WebRequest.Create(
                                $"{Constants.Server_String}/Chat/CreateChat?chatId={Guid.NewGuid().ToString()}" +
                                $"&userId={user.Id.ToString()}&companyId={user.CompanyId.ToString()}" +
                                $"&chatName={chatName}&chatCreationDate={DateTime.Now.ToString()}&type={Db_Entry_Admin}").GetResponse() as HttpWebResponse)
                            {
                                using (Stream stream = httpWebResponse.GetResponseStream())
                                {
                                    stream.Flush();
                                    Hide();
                                    FrmChat frmChat = FrmChat.GetInstance();
                                    frmChat.RetrieveCompanyChats();
                                    frmChat.Show();
                                }
                            }
                        }
                        catch (WebException ex)
                        {
                            Console.WriteLine($"An exception occurred:{ex.Message}");
                        }
                    }));
                }
                else
                {
                    MessageBox.Show("A chat with this name already exists.\nPlease choose a different one");
                }
            }
            else
            {
                MessageBox.Show("The name must be non-blank and only contain letters");
            }
        }

        private void menuItemLogOut_Click(object sender, EventArgs e)
        {
            FrmEntry frmEntry = FrmEntry.GetInstance();
            if (Utility.LoggedInUser != null)
            {
                Utility.LoggedInUser = null;
                Utility.SelectedChat = null;
                Utility.SelectedChatParticipant = null;
                MessageBox.Show("Successfully logged out");
                Hide();
                frmEntry.Show();
            }
            else
                MessageBox.Show("Already logged out");
        }

        private void menuItemActivityMeny_Click(object sender, EventArgs e)//menuItemActivityMenu
        {
            Hide();
            if(Utility.LoggedInUser.Type.Equals(Constants.User_Type_Regular_User))
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

        private void menuItemChat_Click(object sender, EventArgs e)
        {
            FrmChat frmChat = FrmChat.GetInstance();
            Hide();
            frmChat.Show();
        }
    }
}
