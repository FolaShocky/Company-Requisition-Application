using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Net;
using System.Net.Http;
using WebApplication1.Models;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using static WebApplication1.Models.Constants;
using System.Diagnostics;
namespace DissertWindowsFormApplication
{
    public partial class FrmRequestItem : Form
    {
        private List<Admin> AdminList { get; set; }
        private static FrmRequestItem FrmRequestItemInstance;
        private int ReasonMaxCharacterLength;
        public FrmRequestItem()
        {
            InitializeComponent();
            CreateHandle();
            lblReasonCharacterCount.Text = $"0/200";
            txtQuantity.MaxLength = 3;
        }
        
        private int RetrieveFieldMaxCharacterLength(string fieldName,string tableName)
        {
            using(HttpWebResponse httpWebResponse = 
                WebRequest.Create($"{Constants.Server_String}/Item/RetrieveFieldMaxCharacterLength?fieldName={fieldName}&tableName={tableName}").GetResponse() as HttpWebResponse)
            {
                using(StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
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
        public static FrmRequestItem GetInstance()
        {
            if (FrmRequestItemInstance == null)
                FrmRequestItemInstance = new FrmRequestItem();
            FrmRequestItemInstance.FormClosed += new FormClosedEventHandler(delegate (object sender, FormClosedEventArgs e) {
                Application.Exit();
            });
            FrmRequestItemInstance.Invoke(new Action(() =>
            {
                FrmRequestItemInstance.ReasonMaxCharacterLength = FrmRequestItemInstance.RetrieveFieldMaxCharacterLength(Db_Field_Reason, Db_Table_Item);
                FrmRequestItemInstance.InitLengthLabels();
            }));
            FrmRequestItemInstance.menuItemUsername.Text = $"Logged in as: {Utility.LoggedInUser.Username}";
            FrmRequestItemInstance.PopulateCBoxAdmins();
            return FrmRequestItemInstance;
        }

        private void PopulateCBoxAdmins()
        {
            FrmRequestItemInstance.cBoxAdmin.Invoke(new Action(() =>
            {
                FrmRequestItemInstance.AdminList = FrmRequestItemInstance.RetrieveAdmins(Utility.LoggedInUser.CompanyId);
                FrmRequestItemInstance.AdminList.Sort(new Admin());
                FrmRequestItemInstance.AdminList.ForEach(admin =>
                {
                    if (!cBoxAdmin.Items.Contains(admin.Username))
                    {
                        FrmRequestItemInstance.cBoxAdmin.Items.Add(admin.Username);
                    }
                });
            }));
        }
        private List<Admin> RetrieveAdmins(Guid companyId)
        {
            using (HttpWebResponse httpWebResponse = WebRequest.Create(
                $"{Constants.Server_String}/Admin/RetrieveAdmins?companyId={companyId.ToString()}").GetResponse() as HttpWebResponse)
            {
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    List<Admin> adminList = new List<Admin>();
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    while (!streamReader.EndOfStream)
                    {
                        adminList.Add(javaScriptSerializer.Deserialize<Admin>(streamReader.ReadLine()));
                    }
                    return adminList;
                }
            }
        }

        private void CreateItem(string name, int quantity, string reason)
        {
            try
            {
                int selectedIndex = cBoxAdmin.SelectedIndex;
                Item item = new Item(name, quantity, reason);
                item.CompanyId = Utility.LoggedInUser.CompanyId;
                item.UserId = Utility.LoggedInUser.Id;
                item.AdminId = AdminList[selectedIndex].Id;
                item.ItemId = Guid.NewGuid();
                item.IsActive = true;
                item.Response = Db_Const_Response_Undecided;
                item.RequestDate = DateTime.Now;
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                using(Stream webStream = WebRequest.CreateHttp(
                    $"{Constants.Server_String}/Item/CreateItem?itemId={item.ItemId.ToString()}" +
                    $"&companyId={item.CompanyId.ToString()}" +
                    $"&userId={item.UserId.ToString()}&adminId={item.AdminId.ToString()}" +
                    $"&isActive={item.IsActive}&name={item.Name}" +
                    $"&quantity={item.Quantity}&reason={item.Reason}&response={item.Response}" +
                    $"&requestDate={item.RequestDate.ToString("yyyy-MM-dd hh:mm:ss")}")
                    .GetResponse().GetResponseStream())
                {
                    Debug.WriteLine($"Date:{item.RequestDate.ToString("yyyy-MM-dd hh:mm:ss")}");
                    webStream.Flush();
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
            }
        }
 

        private void InitLengthLabels()
        {
            int nameLength = RetrieveFieldMaxCharacterLength(Db_Field_Name, Db_Table_Item);
            int reasonLength = RetrieveFieldMaxCharacterLength(Db_Field_Reason, Db_Table_Item);
            lblItemNameLength.Text = $"Max Length:({nameLength} chars)";
            lblReasonLength.Text = $"Max Length:({ReasonMaxCharacterLength} chars)";
            txtName.MaxLength = nameLength;
            richTxtReason.MaxLength = reasonLength; 
        }

        private void ResetWidgets()
        {
            cBoxAdmin.Text = string.Empty;
            txtName.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            richTxtReason.Text = string.Empty;
        }

        private bool FieldsAreValid()
        {
            bool adminIsValid = cBoxAdmin.Items.Contains(cBoxAdmin.Text.Trim());
            bool nameIsValid = Regex.IsMatch(txtName.Text.Trim(), @"^[a-zA-Z ]+$");
            bool quantityIsValid = Regex.IsMatch(txtQuantity.Text.Trim(), @"^[0-9]+$");
            bool reasonIsValid = !string.IsNullOrEmpty(richTxtReason.Text.Trim());

            if (!adminIsValid)
            {
                MessageBox.Show("Please select an admin from the dropdown");
            }
            if (!nameIsValid)
            {
                MessageBox.Show("Please ensure the name is solely composed of letters and/or spaces");
            }
            if (!quantityIsValid)
            {
                MessageBox.Show("Please ensure the quantity is a valid positive whole number");
            }
            if (!reasonIsValid)
            {
                MessageBox.Show("Please ensure the reason field is not empty");
            }
            return adminIsValid && nameIsValid && quantityIsValid && reasonIsValid;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (FieldsAreValid())
            {
                btnSubmit.Invoke(new Action(() =>
                {
                    FrmLoading frmLoading = FrmLoading.GetInstance();
                    frmLoading.Show();
                    Enabled = false;
                    int quantity = int.Parse(txtQuantity.Text.Trim());
                    if (quantity < 1)
                        quantity = 1;
                    CreateItem(txtName.Text.Trim(),quantity, richTxtReason.Text.Trim());
                    frmLoading.Hide();
                    Enabled = true;
                    ResetWidgets();
                }));
            }
        }

		private void richTxtReason_TextChanged(object sender, EventArgs eventArgs)
        {
            lblReasonCharacterCount.Text = $"{richTxtReason.Text.Length}/{ReasonMaxCharacterLength.ToString()}";
        }

        private void FrmRequestItem_Load(object sender, EventArgs e)
        {
            
            
        }

        private void menuItemMainMenu_Click(object sender, EventArgs e)
        {
            FrmMainMenu frmMainMenu = FrmMainMenu.GetInstance();
            Hide();
            frmMainMenu.Show();
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void lblReasonCharacterCount_Click(object sender, EventArgs e)
        {

        }

        private void menuItemRefresh_Click(object sender, EventArgs e)
        {
            ResetWidgets();
            cBoxAdmin.Invoke(new Action(() => PopulateCBoxAdmins()));
        }

        private void lblReasonCharacterCount_Click_1(object sender, EventArgs e)
        {
            
        }

        private void menuItemUsername_Click(object sender, EventArgs e)
        {

        }
    }
}
