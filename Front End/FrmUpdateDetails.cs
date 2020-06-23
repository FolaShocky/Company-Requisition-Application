using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using WebApplication1.Models;
using System.Web.Script.Serialization;
using System.Collections.Concurrent;
using static WebApplication1.Models.Constants;
using System.Threading;
namespace DissertWindowsFormApplication
{
    public partial class FrmUpdateDetails : Form
    {
        private static FrmUpdateDetails FrmUpdateDetailsInstance;

        private FrmLoading FrmLoadingInstance;
        public static FrmUpdateDetails GetInstance()
        {
            if (FrmUpdateDetailsInstance == null)
            {
                FrmUpdateDetailsInstance = new FrmUpdateDetails();
                FrmUpdateDetailsInstance.FormClosed += new FormClosedEventHandler(delegate (object sender, FormClosedEventArgs e) {
                    Application.Exit();
                });
                FrmUpdateDetailsInstance.FrmLoadingInstance = FrmLoading.GetInstance();
            }
            FrmUpdateDetailsInstance.InitForm();
            return FrmUpdateDetailsInstance;
        }

        private void InitForm()
        {
            User loggedInUser = Utility.LoggedInUser;
            FrmUpdateDetailsInstance.Invoke(new Action(() =>
            {
                FrmUpdateDetailsInstance.menuItemUsername.Text = loggedInUser != null ? $"Logged in as: {loggedInUser.Username}" : "Not Logged in";
                FrmUpdateDetailsInstance.PopulateCBoxRegularUsers();
                FrmUpdateDetailsInstance.InitLengthLabels();
            }));
        }

        private FrmUpdateDetails()
        {
            InitializeComponent();
            CreateHandle();
            txtPassword.PasswordChar = '\u2022';
            txtConfirmPassword.PasswordChar = '\u2022'; 
            ResetGUIControls();
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

        private bool DeletePersonalAccount()
        {
            try
            {
                User loggedInUser = Utility.LoggedInUser;
                string userType = Utility.LoggedInUser.Type.Equals(Constants.Db_Entry_Admin) ? Constants.Db_Entry_Admin : Constants.Db_Entry_Regular_User;
                using (HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/{userType}/DeleteUser?id={loggedInUser.Id.ToString()}").GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = httpWebResponse.GetResponseStream())
                    {
                        stream.Flush();
                    }
                }
                return true;
            }
            catch (WebException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }
        private bool DeleteUserAccount(Guid userId)
        {
            try
            {
                User loggedInUser = Utility.LoggedInUser;
                string userType = Utility.LoggedInUser.Type.Equals(Constants.Db_Entry_Admin) ? Constants.Db_Entry_Admin : Constants.Db_Entry_Regular_User;
                using (HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/{userType}/DeleteUser?id={userId.ToString()}").GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = httpWebResponse.GetResponseStream())
                    {
                        stream.Flush();
                    }
                }
                return true;
            }
            catch (WebException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        private List<RegularUser> RetrieveAllCompanyRegularUsers()
        {
            try
            {
                User loggedInUser = Utility.LoggedInUser;
                List<RegularUser> regularUserList = new List<RegularUser>();
                string userType = Utility.LoggedInUser.Type.Equals(Constants.Db_Entry_Admin) ? Constants.Db_Entry_Admin : Constants.Db_Entry_Regular_User;
                using (HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/{userType}/RetrieveUsersByCompanyId?companyId={loggedInUser.CompanyId}")
                    .GetResponse() as HttpWebResponse)
                {
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        while (!streamReader.EndOfStream)
                        {
                            regularUserList.Add(javaScriptSerializer.Deserialize<RegularUser>(streamReader.ReadLine()));
                        }
                        regularUserList.Sort(new RegularUser());
                    }
                }
                return regularUserList;
                
            }
            catch (WebException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        private bool UpdateUserDetails(string firstName, string surname, string username, string password)
        {
            try
            {
                User loggedInUser = Utility.LoggedInUser;
                string userType = Utility.LoggedInUser.Type.Equals(Constants.Db_Entry_Admin) ? Constants.Db_Entry_Admin : Constants.Db_Entry_Regular_User;
                using (HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/{userType}/UpdateUser?id={loggedInUser.Id.ToString()}&firstName={firstName}&surname={surname}" +
                    $"&username={username}&password={password}&type={loggedInUser.Type}&companyId={loggedInUser.CompanyId.ToString()}").GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = httpWebResponse.GetResponseStream())
                    {
                        stream.Flush();
                    }
                }
                return true;
            }
            catch (WebException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        private void FrmUpdateDetails_Load(object sender, EventArgs e)
        {

        }

        private void ResetTextBox(ref TextBox textBox)
        {
            textBox.Enabled = true;
            textBox.Text = string.Empty;
        }

        private void ResetCheckBox(ref CheckBox checkBox)
        {
            checkBox.Checked = false;
        }

        private void ResetGUIControls()
        {
            ResetTextBox(ref txtFirstName);
            ResetTextBox(ref txtSurname);
            ResetTextBox(ref txtUsername);
            ResetTextBox(ref txtPassword);
            ResetTextBox(ref txtConfirmPassword);
        }

        private void InitLengthLabels()
        {
            int firstNameLength = RetrieveFieldMaxCharacterLength(Db_Field_FirstName, Db_Table_User_Details);
            int surnameLength = RetrieveFieldMaxCharacterLength(Db_Field_Surname, Db_Table_User_Details);
            int usernameLength = RetrieveFieldMaxCharacterLength(Db_Field_Username, Db_Table_User_Details);
            int passwordLength = RetrieveFieldMaxCharacterLength(Db_Field_Password, Db_Table_User_Details);
            lblFirstNameLength.Text = $"Max Length:({firstNameLength} chars)";
            lblSurnameLength.Text = $"Max Length:({surnameLength} chars)";
            lblUsernameLength.Text = $"Max Length:({usernameLength} chars)";
            lblPasswordLength.Text = $"Max Length:({passwordLength} chars)";
            txtFirstName.MaxLength = firstNameLength;
            txtSurname.MaxLength = surnameLength;
            txtUsername.MaxLength = usernameLength;
            txtPassword.MaxLength = passwordLength;
        }

        private void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            User loggedInUser = Utility.LoggedInUser;
            if(txtFirstName.Text.Equals(string.Empty))
                txtFirstName.Text = loggedInUser.FirstName;
            if(txtSurname.Text.Equals(string.Empty))
                txtSurname.Text = loggedInUser.Surname;
            if(txtUsername.Equals(string.Empty))
                txtUsername.Text = loggedInUser.Username;
            if(txtPassword.Text.Equals(string.Empty))
                txtPassword.Text = loggedInUser.Password;
            txtFirstName.Enabled = false;
            txtSurname.Enabled = false;
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            btnUpdateInfo.Invoke(new Action(() =>
            {
                string newFirstName = txtFirstName.Text;
                string newSurname = txtSurname.Text;
                string newUsername = txtUsername.Text;
                string newPassword = txtPassword.Text;
                Regex firstNameRegex = new Regex(@"^[a-zA-Z]+$");
                Regex surnameRegex = new Regex(@"^[a-zA-Z]+$");
                Regex usernameRegex = new Regex(@"^[a-zA-Z0-9]+$");
                bool firstNameIsOk = firstNameRegex.IsMatch(newFirstName);
                bool surnameIsOk = surnameRegex.IsMatch(newSurname);
                bool usernameIsOk = usernameRegex.IsMatch(newUsername);
                bool passwordIsOk = txtConfirmPassword.Text.Equals(newPassword) && newPassword.Length >= 7;
                Debug.WriteLine($"Password is ok:{passwordIsOk}");
                if (firstNameIsOk && surnameIsOk && usernameIsOk && passwordIsOk)
                {

                    FrmLoadingInstance.Show();
                    Enabled = false;
                    UpdateUserDetails(newFirstName, newSurname, newUsername, newPassword);
                    StringBuilder updatedPropertiesStringBuilder = new StringBuilder();
                    if (!loggedInUser.FirstName.Equals(newFirstName))
                    {
                        updatedPropertiesStringBuilder.Append($"Updated First Name to {newFirstName}.\n");
                    }
                    if (!loggedInUser.Surname.Equals(newSurname))
                    {
                        updatedPropertiesStringBuilder.Append($"Updated Surname to {newSurname}.\n");
                    }
                    if (!loggedInUser.Username.Equals(newUsername))
                    {
                        updatedPropertiesStringBuilder.Append($"Updated Username to {newUsername}.\n");
                    }
                    if (!updatedPropertiesStringBuilder.ToString().Equals(string.Empty))
                        MessageBox.Show(updatedPropertiesStringBuilder.ToString());
                }
                else
                {
                    if (!firstNameIsOk && !loggedInUser.FirstName.Equals(newFirstName))
                        MessageBox.Show("Please ensure the First Name field is only composed of letters");
                    if (!surnameIsOk && !loggedInUser.Surname.Equals(newSurname))
                        MessageBox.Show("Please ensure the Surname field is only composed of letters");
                    if (!usernameIsOk && !loggedInUser.Username.Equals(newUsername))
                        MessageBox.Show("Please ensure the Username field is only composed of letters and numbers");
                    if (!passwordIsOk && !loggedInUser.Password.Equals(newPassword))
                        MessageBox.Show("Please ensure the passwords match\n and that your password is at least 7 characters long");
                }
                FrmLoadingInstance.Hide();
                Enabled = true;
                ResetGUIControls();
                
            }));
        }

        private bool DeleteRegularUser(Guid userId)
        {
            try
            {
                User loggedInUser = Utility.LoggedInUser;
                string userType = Utility.LoggedInUser.Type.Equals(Constants.Db_Entry_Admin) ? Constants.Db_Entry_Admin : Constants.Db_Entry_Regular_User;
                using (HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/{userType}/DeleteUser?id={userId.ToString()}").GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = httpWebResponse.GetResponseStream())
                    {
                        stream.Flush();
                    }
                }
                return true;
            }
            catch (WebException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        private void menuItemUsername_Click(object sender, EventArgs e)
        {

        }


        private User RetrieveLatestAccountDetails()
        {
            User loggedInUser = Utility.LoggedInUser;
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                string userType = loggedInUser.Type;
                using (HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/{userType}/" +
                    $"IdRetrieval?id={loggedInUser.Id.ToString()}").GetResponse() as HttpWebResponse)
                {
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            string line = streamReader.ReadLine();
                            Utility.LoggedInUser = javaScriptSerializer.Deserialize<User>(line);
                        }
                        return Utility.LoggedInUser;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred:{ex.Message}");
                return null;
            }
        }
        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void menuItemChat_Click(object sender, EventArgs e)
        {
            Hide();
            FrmChat frmChat = FrmChat.GetInstance();
            frmChat.Show();
        }

        private void cBoxFirstName_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void cBoxSurname_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void cBoxUsername_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void cBoxPassword_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void btnDeletePersonalAccount_Click(object sender, EventArgs e)
        {
            btnDeletePersonalAccount.Invoke(new Action(() =>
            {
                FrmLoading frmLoading = FrmLoading.GetInstance();
                Enabled = false;
                frmLoading.Show();
                DeletePersonalAccount();
                Utility.LoggedInUser = null;
                frmLoading.Hide();
                Enabled = true;
                Hide();
                FrmEntry frmEntry = FrmEntry.GetInstance();
                frmEntry.Show();
            }));
        }

        private void PopulateCBoxRegularUsers()
        {
            cBoxRegularUsers.Items.Clear();
            List<RegularUser> regularUserList = RetrieveAllCompanyRegularUsers();
            regularUserList.ForEach(regularUser => cBoxRegularUsers.Items.Add(regularUser.Username));
            cBoxRegularUsers.Refresh();
        }

        private void btnDeleteRegularUser_Click(object sender, EventArgs e)
        {
            btnDeleteRegularUser.Invoke(new Action(() => {
                cBoxRegularUsers.Text = cBoxRegularUsers.Text.Trim();
                if (Utility.LoggedInUser.Type.Equals(Constants.Db_Entry_Admin))
                {
                    if (!cBoxRegularUsers.Text.Trim().Equals(string.Empty) && cBoxRegularUsers.Items.Contains(cBoxRegularUsers.Text))
                    {
                        FrmLoading frmLoading = FrmLoading.GetInstance();
                        frmLoading.Show();
                        Enabled = false;
                        List<RegularUser> regularUserList = RetrieveAllCompanyRegularUsers();
                        RegularUser regularUser = regularUserList.Find(_regularUser => _regularUser.Username.Equals(cBoxRegularUsers.Text));
                        DeleteRegularUser(regularUser.Id);
                        PopulateCBoxRegularUsers();
                        Enabled = true;
                        frmLoading.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Please choose a user from the dropdown");
                    }

                }
                else
                {
                    MessageBox.Show("Only admins can use this functionality");
                }
            }));
        }

        private bool PromoteToAdmin(User user)
        {
            try
            {
                using (HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/{Db_Entry_Regular_User}/UpdateUser?id={user.Id.ToString()}&firstName={user.FirstName}&surname={user.Surname}" +
                    $"&username={user.Username}&password={user.Password}&type={user.Type}&companyId={user.CompanyId.ToString()}").GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = httpWebResponse.GetResponseStream())
                    {
                        stream.Flush();
                    }

                }
                return true;
            }
            catch (WebException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }
        private void btnPromoteToAdmin_Click(object sender, EventArgs e)
        {

            btnPromoteToAdmin.Invoke(new Action(() =>
            {
                cBoxRegularUsers.Text = cBoxRegularUsers.Text.Trim();
                if (Utility.LoggedInUser.Type.Equals(Constants.Db_Entry_Admin))
                {
                    if (!cBoxRegularUsers.Text.Trim().Equals(string.Empty) && cBoxRegularUsers.Items.Contains(cBoxRegularUsers.Text))
                    {
                        List<RegularUser> regularUserList = RetrieveAllCompanyRegularUsers();
                        RegularUser regularUser = regularUserList.Find(_regularUser => _regularUser.Username.Equals(cBoxRegularUsers.Text));
                        regularUser.Type = Db_Entry_Admin;
                        PromoteToAdmin(regularUser);
                        PopulateCBoxRegularUsers();
                    }
                    else
                    {
                        MessageBox.Show("Please choose a user from the dropdown");
                    }
                }
                else
                {
                    MessageBox.Show("Only admins can use this functionality");
                }
            }));
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

        private void menuItemRefresh_Click(object sender, EventArgs e)
        {
            ResetGUIControls();
            InitForm();
        }
    }
}
