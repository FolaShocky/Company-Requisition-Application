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
using System.Net.Http;
using System.Net;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using System.Diagnostics;
namespace DissertWindowsFormApplication
{
    public partial class FrmMainMenu : Form
    {
        private static FrmMainMenu FrmMainMenuInstance;
        private List<Item> itemList;
        private FrmMainMenu()
        {
            InitializeComponent();
            CreateHandle();
            itemList = new List<Item>();
            
        }
        private void CheckForResponses()
        {
            FrmMainMenuInstance.Invoke(new Action(() =>
            {
                listViewItemRequestResponses.Clear();
                itemList.Clear();
                listViewItemRequestResponses.Columns.Add("Item Id");
                listViewItemRequestResponses.Columns.Add("Name");
                listViewItemRequestResponses.Columns.Add("Quantity");
                listViewItemRequestResponses.Columns.Add("Your Reason");
                listViewItemRequestResponses.Columns.Add("Request Date");
                listViewItemRequestResponses.Columns.Add("Response");
                listViewItemRequestResponses.MultiSelect = false;
                listViewItemRequestResponses.GridLines = true;
                listViewItemRequestResponses.FullRowSelect = true;
                listViewItemRequestResponses.View = View.Details;
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                using (HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/Item/RetrieveItems?" +
                    $"userId={Utility.LoggedInUser.Id.ToString()}").GetResponse() as HttpWebResponse)
                {
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            Console.WriteLine("Is here");
                            Item item = javaScriptSerializer.Deserialize<Item>(streamReader.ReadLine());
                            FrmMainMenuInstance.itemList.Add(item);
                        }
                        FrmMainMenuInstance.itemList.Sort(new Item());
                        FrmMainMenuInstance.itemList.ForEach(item =>
                        {
                            FrmMainMenuInstance.listViewItemRequestResponses.Items.Add(new ListViewItem(new string[] {
                                    item.ItemId.ToString(),
                                    item.Name,
                                    item.Quantity.ToString(),
                                    item.Reason,
                                    item.RequestDate.ToString("yyyy-MM-dd hh:mm:ss"),
                                    item.Response
                                }));
                            Debug.WriteLine($"Item:{item.ToString()}");
                        });
                    }
                    listViewItemRequestResponses.Refresh();
                }
            }));
        }

        private void InitForm()
        {
            User loggedInUser = Utility.LoggedInUser;
            if (loggedInUser != null)
            {
                FrmMainMenuInstance.CheckForResponses();
                FrmMainMenuInstance.Invoke(new Action(() => FrmMainMenuInstance.RetrieveAdmins(loggedInUser.CompanyId)));
            }
            FrmMainMenuInstance.menuItemUsername.Text = loggedInUser != null ? $"Logged in as: {loggedInUser.Username}" : "Not Logged in";
        }

        public static FrmMainMenu GetInstance()
        {
            if (FrmMainMenuInstance == null)
            {
                FrmMainMenuInstance = new FrmMainMenu();
                FrmMainMenuInstance.FormClosed += new FormClosedEventHandler(delegate (object sender, FormClosedEventArgs e) {
                    Application.Exit();
                });
            }
            FrmMainMenuInstance.InitForm();
            return FrmMainMenuInstance;
        }

        private void FrmMainMenu_Load(object sender, EventArgs e)
        {
            
        }

        private void FrmMainMenu_Closed(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblOptions_Click(object sender, EventArgs e)
        {

        }
        //companyId: The companyId of the logged in employee
        private List<Admin> RetrieveAdmins(Guid companyId)
        {
            using(HttpWebResponse httpWebResponse = WebRequest.Create(
                $"{Constants.Server_String}/Admin/RetrieveAdmins?companyId={companyId.ToString()}").GetResponse() as HttpWebResponse)
            {
                using(StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    List<Admin> adminList = new List<Admin>();
                    while (!streamReader.EndOfStream)
                    {
                        Admin admin = javaScriptSerializer.Deserialize<Admin>(streamReader.ReadLine());
                        adminList.Add(admin);
                    }
                    return adminList;
                }
            }

        }
        private void btnRequestItem_Click(object sender, EventArgs e)
        {
            
            
        }

        private void menuItemSignUp_Click(object sender, EventArgs e)
        {
           
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void menuItemMainMenu_Click(object sender, EventArgs e)
        {
            FrmMainMenu frmMainMenu = FrmMainMenu.GetInstance();

            if (Utility.LoggedInUser != null)
            {
                Utility.LoggedInUser = null;
                MessageBox.Show("Successfully logged out");
                Hide();
                frmMainMenu.Show();
            }
            else
                MessageBox.Show("Already logged out");
        }

        private void menuItemLogin_Click(object sender, EventArgs e)
        {

        }

        private void listViewItemRequestResponses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnDelete.Invoke(new Action(() =>
            {
                
                if (listViewItemRequestResponses.SelectedItems != null)
                {
                    FrmLoading frmLoading = FrmLoading.GetInstance();
                    Enabled = false;
                    frmLoading.Show();
                    Item item = itemList[listViewItemRequestResponses.SelectedItems[0].Index];
                    using (HttpWebResponse httpWebResponse = WebRequest.Create(
                        $"{Constants.Server_String}/Item/DeleteItem?itemId={item.ItemId.ToString()}").GetResponse() as HttpWebResponse)
                    {
                        using (Stream stream = httpWebResponse.GetResponseStream())
                        {
                            stream.Flush();
                        }
                    }
                    CheckForResponses();
                    frmLoading.Hide();
                    Enabled = true;
                }
                else
                {
                    MessageBox.Show("Please select an item");
                }
            }));
            
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            Hide();
            FrmChat frmChat = FrmChat.GetInstance();
            frmChat.Invoke(new Action(() =>
            {
                FrmLoading frmLoading = FrmLoading.GetInstance();
                Enabled = false;
                frmLoading.Show();
                frmChat.InitListViewChats();
                frmChat.RefreshGui();
                frmLoading.Hide();
                Enabled = true;
            }));
            frmChat.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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

        private void menuItemRefresh_Click(object sender, EventArgs e)
        {
            FrmLoading frmLoading = FrmLoading.GetInstance();
            Enabled = false;
            frmLoading.Show();
            InitForm();
            frmLoading.Hide();
            Enabled = true;
        }

        private void menuItemRequestItem_Click(object sender, EventArgs e)
        {
            FrmLoading frmLoading = FrmLoading.GetInstance();
            Enabled = false;
            frmLoading.Show();
            Hide();
            FrmRequestItem frmRequestItem = FrmRequestItem.GetInstance();
            frmRequestItem.Show();
            frmLoading.Hide();
            Enabled = true;
        }
    }
}
