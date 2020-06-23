using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebApplication1.Models;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Diagnostics;
using System.Web.Script.Serialization;
namespace DissertWindowsFormApplication
{
    public partial class FrmMainMenuAdmin : Form
    {
        private static FrmMainMenuAdmin FrmMainMenuAdminInstance;
        private List<Item> itemList;
        private FrmLoading FrmLoadingInstance;
        private FrmMainMenuAdmin()
        {
            InitializeComponent();
            CreateHandle();
            InitListViewItemRequests();
            InitListViewRoleChanges();
        }
       
        private void InitForm()
        {
            if (Utility.LoggedInUser != null)
            {
                FrmMainMenuAdminInstance.CheckForItemRequests();
                FrmMainMenuAdminInstance.CheckForCompanyRoles();
                FrmMainMenuAdminInstance.itemList =  FrmMainMenuAdminInstance.RetrieveAdminItems(Utility.LoggedInUser.Id);
            }
            User loggedInUser = Utility.LoggedInUser;
            FrmMainMenuAdminInstance.menuItemUsername.Text = loggedInUser != null ? $"Logged in as: {loggedInUser.Username}" : "Not Logged in";
        }
        public static FrmMainMenuAdmin GetInstance()
        {
            if (FrmMainMenuAdminInstance == null)
            {
                FrmMainMenuAdminInstance = new FrmMainMenuAdmin();
                FrmMainMenuAdminInstance.FormClosed += new FormClosedEventHandler(delegate (object sender, FormClosedEventArgs e) {
                    Application.Exit();
                });
                FrmMainMenuAdminInstance.FrmLoadingInstance = FrmLoading.GetInstance();
                FrmMainMenuAdminInstance.itemList = FrmMainMenuAdminInstance.RetrieveAdminItems(Utility.LoggedInUser.Id);
            }
            FrmMainMenuAdminInstance.InitForm();
            return FrmMainMenuAdminInstance;
        }

        private void InitListViewItemRequests()
        {
            listViewItemRequests.Clear();
            listViewItemRequests.FullRowSelect = true;
            listViewItemRequests.View = View.Details;
            listViewItemRequests.MultiSelect = false;
            listViewRoleChanges.GridLines = true;
            listViewItemRequests.Columns.Add("Item Id");
            listViewItemRequests.Columns.Add("Name");
            listViewItemRequests.Columns.Add("Quantity");
            listViewItemRequests.Columns.Add("Reason");
            listViewItemRequests.Columns.Add("Username");
            listViewItemRequests.Columns.Add("Request Date");
        }

        private void InitListViewRoleChanges()
        {
            listViewRoleChanges.Clear();
            listViewRoleChanges.FullRowSelect = true;
            listViewRoleChanges.View = View.Details;
            listViewRoleChanges.MultiSelect = false;
            listViewRoleChanges.GridLines = true;
            listViewRoleChanges.Columns.Add("Role Id");
            listViewRoleChanges.Columns.Add("Subject Username");
            listViewRoleChanges.Columns.Add("Chat Name");//Not guaranteed to have a non-null value
            listViewRoleChanges.Columns.Add("Role Action");
            listViewRoleChanges.Columns.Add("Role Type");
            listViewRoleChanges.Columns.Add("Endorsement Count");
            listViewRoleChanges.Columns.Add("Endorsing Admins");//These can be chat or general admins
        }

        private void CheckForCompanyRoles()
        {
            InitListViewRoleChanges();
            listViewRoleChanges.Invoke(new Action(() =>
            {
                List<Role> roleList = RetrieveCompanyRoles();
                roleList.ForEach(role =>
                {
                    StringBuilder endorsingAdminsStringBuilder = new StringBuilder();
                    role.EndorsingChatAdminList.ForEach(endorsingChatAdmin =>
                    {
                        endorsingAdminsStringBuilder.Append(endorsingChatAdmin.Username);
                        endorsingAdminsStringBuilder.Append("\n");
                    });
                    listViewRoleChanges.Items.Add(new ListViewItem(new string[]
                    {
                        role.RoleId.ToString(),
                        role.RoleUser.Username,
                        role.RoleChat.ChatName,
                        role.RoleAction,
                        role.RoleType,
                        role.EndorsementCount.ToString(),
                        endorsingAdminsStringBuilder.ToString()
                    }));
                });
            }));
            listViewRoleChanges.Refresh();
        }
        private void CheckForItemRequests()
        {
            InitListViewItemRequests();

            listViewItemRequests.Invoke(new Action(() =>
            {
                List<Item> itemList = RetrieveAdminItems(Utility.LoggedInUser.Id);
                Console.WriteLine($"Size: {itemList.Count}");
                itemList.Sort(new Item());
                itemList.ForEach(item =>
                {
                    listViewItemRequests.Items.Add(
                        new ListViewItem(
                            new string[] {
                                item.ItemId.ToString(),
                                item.Name,
                                item.Quantity.ToString(),
                                item.Reason,
                                RetrieveRegularUser(item.UserId).Username,
                                item.RequestDate.ToString("yyyy-MM-dd hh:mm:ss")
                            }
                        )
                    );
                });
                listViewItemRequests.Refresh();
            }));
            

        }

        private List<Role> RetrieveCompanyRoles()
        {
            try
            {
                using (HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/Role/RetrieveRolesByCompanyId?" +
                    $"companyId={Utility.LoggedInUser.CompanyId.ToString()}").GetResponse() as HttpWebResponse)
                {
                    List<Role> roleList = new List<Role>();
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        while (!streamReader.EndOfStream)
                            roleList.Add(javaScriptSerializer.Deserialize<Role>(streamReader.ReadLine()));
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
        private RegularUser RetrieveRegularUser(Guid userId)
        {
            using(HttpWebResponse httpWebResponse = WebRequest.Create(
                $"{Constants.Server_String}/RegularUser/IdRetrieval?id={userId.ToString()}").GetResponse() as HttpWebResponse)
            {
                using(StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    RegularUser regularUser = new RegularUser();
                    while(!streamReader.EndOfStream)
                        regularUser = javaScriptSerializer.Deserialize<RegularUser>(streamReader.ReadLine());
                    return regularUser;
                }
            }
        }
        private List<Item> RetrieveAdminItems(Guid adminId)
        {
            using(HttpWebResponse httpWebResponse = WebRequest.Create(
                $"{Constants.Server_String}/Item/RetrieveAdminItems?adminId={adminId.ToString()}").GetResponse() as HttpWebResponse)
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                List<Item> itemList = new List<Item>();
                using(StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    Console.WriteLine("Added item_");
                    while (!streamReader.EndOfStream)
                    {
                        Console.WriteLine("Retrieved an item");
                        string line = streamReader.ReadLine();
                        itemList.Add(javaScriptSerializer.Deserialize<Item>(line));
                    }
                    itemList.Sort(new Item());
                    return itemList;
                }
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void FrmMainMenuAdmin_Load(object sender, EventArgs e)
        {
            
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

        private void btnAccept_Click(object sender, EventArgs e)
        {
            FrmLoadingInstance.Show();
            btnAccept.Invoke(new Action(() =>
            {
                if (listViewItemRequests.SelectedItems.Count > 0)
                {
                    Enabled = false;
                    Item item = FrmMainMenuAdminInstance.itemList[listViewItemRequests.SelectedItems[0].Index];
                    item.Response = WebApplication1.Models.Constants.Db_Const_Response_Yes;
                    using (HttpWebResponse httpWebResponse = WebRequest.Create($"{Constants.Server_String}/Item/UpdateItem?" +
                        $"itemId={item.ItemId.ToString()}&companyId={item.CompanyId.ToString()}" +
                        $"&userId={item.UserId.ToString()}&adminId={item.AdminId.ToString()}" +
                        $"&isActive={item.IsActive}&name={item.Name}" +
                        $"&quantity={item.Quantity}&reason={item.Reason}&response={item.Response}" +
                        $"&requestDate={item.RequestDate.ToString("yyyy-MM-dd hh:mm:ss")}").GetResponse() as HttpWebResponse)
                    {
                        using (Stream stream = httpWebResponse.GetResponseStream())
                        {
                            stream.Flush();
                            MessageBox.Show("Notification sent to user");
                            CheckForItemRequests();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select an item");
                }
                FrmLoadingInstance.Hide();
                Enabled = true;
            }));
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            FrmLoadingInstance.Show();
            btnReject.Invoke(new Action(() =>
            {
                if (listViewItemRequests.SelectedItems.Count > 0)
                {
                    Item item = FrmMainMenuAdminInstance.itemList[listViewItemRequests.SelectedItems[0].Index];
                    Enabled = false;
                    item.Response = WebApplication1.Models.Constants.Db_Const_Response_No;
                    using (HttpWebResponse httpWebResponse = WebRequest.Create($"{Constants.Server_String}/Item/UpdateItem?" +
                        $"itemId={item.ItemId.ToString()}&companyId={item.CompanyId.ToString()}" +
                        $"&userId={item.UserId.ToString()}&adminId={item.AdminId.ToString()}" +
                        $"&isActive={item.IsActive}&name={item.Name}" +
                        $"&quantity={item.Quantity}&reason={item.Reason}&response={item.Response}" +
                        $"&requestDate={item.RequestDate.ToString("yyyy-MM-dd hh:mm:ss")}").GetResponse() as HttpWebResponse)
                    {
                        using (Stream stream = httpWebResponse.GetResponseStream())
                        {
                            stream.Flush();
                            MessageBox.Show("Notification sent to user");
                            CheckForItemRequests();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select an item");
                }
                Enabled = true;
                FrmLoadingInstance.Hide();
            }));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void listBoxRoleChange_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listViewRoleChanges_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            Hide();
            FrmChat frmChat = FrmChat.GetInstance();
            frmChat.Invoke(new Action(()=>
            {
                Enabled = false;
                FrmLoadingInstance.Show();
                frmChat.InitListViewChats();
                frmChat.RefreshGui();
                FrmLoadingInstance.Hide();
                Enabled = true;
            }));
            frmChat.Show();
        }

        private void menuItemUsername_Click(object sender, EventArgs e)
        {

        }

        private void menuItemUpdateDetails_Click(object sender, EventArgs e)
        {
            Hide();
            FrmUpdateDetails frmUpdateDetails = FrmUpdateDetails.GetInstance();
            frmUpdateDetails.Show();
        }

        private void mainMenuChat_Click(object sender, EventArgs e)
        {
            Hide();
            FrmChat frmChat = FrmChat.GetInstance();
            frmChat.Show();
        }

        private void mainMenuRefresh_Click(object sender, EventArgs e)
        {
            Enabled = false;
            FrmLoadingInstance.Show();
            InitForm();
            FrmLoadingInstance.Hide();
            Enabled = true;
        }
    }
}
