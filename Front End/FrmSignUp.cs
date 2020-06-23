using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Web.Script.Serialization;
using static DissertWindowsFormApplication.Constants;
using System.Text.RegularExpressions;
using WebApplication1.Models;
using static WebApplication1.Models.Company;

namespace DissertWindowsFormApplication
{


    /*In WebApplication1, add an overload for Admin and RegularUser that, in addition to those already present, takes another parameter called companyName.
    In each of the respective controllers add logic to complete the change.
         */

    public partial class FrmSignUp : Form
    {
        private static FrmSignUp FrmSignUpInstance;
        private static FrmLoading FrmLoadingInstance;
        private FrmSignUp()
        {
            InitializeComponent();
            CreateHandle();
            Text = "Sign Up";
            txtPassword.PasswordChar = '\u2022';
            txtConfirmPassword.PasswordChar = '\u2022';
            cBoxCompanyName.TextChanged += new EventHandler(cBoxCompanyName_TextChanged);
            FrmLoadingInstance = FrmLoading.GetInstance();
            FrmLoadingInstance.Hide();
        }

        public static FrmSignUp GetInstance()
        {
            if (FrmSignUpInstance == null)
                FrmSignUpInstance = new FrmSignUp();
            FrmSignUpInstance.FormClosed += new FormClosedEventHandler(delegate (object sender, FormClosedEventArgs e) {
                Application.Exit();
            });
            FrmSignUpInstance.InitForm();
            return FrmSignUpInstance;
        }

        private void InitForm()
        {
            FrmSignUpInstance.Invoke(new Action(() => {
                FrmSignUpInstance.RetrieveCompaniesAndPopulateCBox();
                FrmSignUpInstance.InitLengthLabels();
            }));
        }
        
        private void InitLengthLabels()
        {
            int firstNameLength = RetrieveFieldMaxCharacterLength(Db_Field_FirstName, Db_Table_User_Details);
            int surnameLength = RetrieveFieldMaxCharacterLength(Db_Field_Surname, Db_Table_User_Details);
            int usernameLength = RetrieveFieldMaxCharacterLength(Db_Field_Username, Db_Table_User_Details);
            int passwordLength = RetrieveFieldMaxCharacterLength(Db_Field_Password, Db_Table_User_Details);
            int companyNameLength = RetrieveFieldMaxCharacterLength(Db_Field_Company_Name, Db_Table_User_Details); 
            lblFirstNameLength.Text = $"Max Length:({firstNameLength} chars)";
            lblSurnameLength.Text = $"Max Length:({surnameLength} chars)";
            lblUsernameLength.Text = $"Max Length:({usernameLength} chars)";
            lblPasswordLength.Text = $"Range:(7-{passwordLength} chars)";
            lblCompanyNameLength.Text = $"Max Length:({companyNameLength} chars)";
            txtFirstName.MaxLength = firstNameLength;
            txtSurname.MaxLength = surnameLength;
            txtUsername.MaxLength = usernameLength;
            txtPassword.MaxLength = passwordLength;
            txtNewCompany.MaxLength = companyNameLength;
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

        private List<Company> RetrieveAllCompanies()
        {
            try
            {
                List<Company> companyList = new List<Company>();
                using (HttpWebResponse httpWebResponse = WebRequest.Create($"{Constants.Server_String}/Company/RetrieveCompanies").GetResponse() as HttpWebResponse)
                {
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            string line = streamReader.ReadLine();
                            Company retrievedCompany = javaScriptSerializer.Deserialize<Company>(line);
                            companyList.Add(retrievedCompany);
                        }
                    }
                    return companyList;
                }
            }
            catch(WebException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return new List<Company>();
            }
        }

        private List<User> RetrieveAllUsers()
        {
            try
            {
                List<User> userList = new List<User>();
                using (HttpWebResponse httpWebResponse = WebRequest.Create($"{Constants.Server_String}/RegularUser/RetrieveAllUsers").GetResponse() as HttpWebResponse)
                {
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            string line = streamReader.ReadLine();
                            User retrievedUser = javaScriptSerializer.Deserialize<User>(line);
                            userList.Add(retrievedUser);
                        }
                    }
                    return userList;
                }
            }
            catch (WebException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }
        private void RetrieveCompaniesLogic()
        {
            try
            {
                List<Company> retrievedCompaniesList = new List<Company>();
                cBoxCompanyName.Items.Clear();
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                using (HttpWebResponse httpWebResponse = WebRequest.Create($"{Server_String}/Company/RetrieveCompanies").GetResponse() as HttpWebResponse)
                {
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            string line = streamReader.ReadLine();
                            Company retrievedCompany = javaScriptSerializer.Deserialize<Company>(line);
                            retrievedCompaniesList.Add(retrievedCompany);
                        }
                    }
                    retrievedCompaniesList.ForEach(company => Debug.WriteLine($"Company:{company}"));
                  
                    List<string> companyNameList = new List<string>();
                    retrievedCompaniesList.ForEach(retrievedCompany => companyNameList.Add(retrievedCompany.CompanyName));

                    companyNameList.Sort();
                    companyNameList.ForEach(companyName => {
                        if (!cBoxCompanyName.Items.Contains(companyName))
                            cBoxCompanyName.Items.Add(companyName);
                    });
                    cBoxCompanyName.Refresh();
                    if (cBoxCompanyName.Items.Count == 0)
                        cBoxCompanyName.Items.Add(No_Company_Exists);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
            }

        }

        private void RetrieveCompaniesAndPopulateCBox()
        {
            RetrieveCompaniesLogic();
        }
        
        private void EnableDisableSignUpFields(bool shouldEnable)
        {
            txtFirstName.Enabled = shouldEnable;
            txtSurname.Enabled = shouldEnable;
            txtUsername.Enabled = shouldEnable;
            txtPassword.Enabled = shouldEnable;
            txtConfirmPassword.Enabled = shouldEnable;
            btnSignUp.Enabled = shouldEnable;
            txtNewCompany.Enabled = shouldEnable;
            cBoxCompanyName.Enabled = shouldEnable;

            if(shouldEnable)
            {
                txtFirstName.Text = string.Empty;
                txtSurname.Text = string.Empty;
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtConfirmPassword.Text = string.Empty;
                cBoxCompanyName.Text = string.Empty;
                txtNewCompany.Text = string.Empty;
            }
        }

        private void ExecuteSignUpLogic(bool isNewCompany)//Parameter - isNewCompany: Has the user opted to create a new company
        {
            try
            {
                string userType = isNewCompany ? Db_Entry_Admin : Db_Entry_Regular_User;
                Guid newCompanyGuid = Guid.NewGuid();
                string newCompanyName = txtNewCompany.Text.Trim();
                Company selectedCompany = cBoxCompanyName.Text.Trim().Equals(string.Empty) ? 
                    new Company(newCompanyGuid,newCompanyName) :
                    RetrieveAllCompanies().First(company => company.CompanyName.Contains(cBoxCompanyName.Text));
                if (isNewCompany)
                {
                    using (HttpWebResponse httpWebResponse = WebRequest.Create($"{Server_String}/Company/CreateCompany?" +
                           $"companyId={selectedCompany.CompanyId.ToString()}&companyName={selectedCompany.CompanyName}").GetResponse() as HttpWebResponse)
                    {
                        using (Stream signUpWebStream = httpWebResponse.GetResponseStream())
                        {
                            signUpWebStream.Flush();
                        }
                    }
                }
                
                using (HttpWebResponse httpWebResponse = WebRequest.Create($"{Constants.Server_String}/{userType}/" +
                $"CreateUser?firstName={txtFirstName.Text.Trim()}&surname={txtSurname.Text.Trim()}&username={txtUsername.Text.Trim()}" +
                $"&password={txtPassword.Text.Trim()}&type={userType}&companyId={selectedCompany.CompanyId.ToString()}").GetResponse() as HttpWebResponse)
                {
                    using (Stream webStream = httpWebResponse.GetResponseStream())
                    {
                        webStream.Flush();
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"A Web Exception occurred");
                using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    Console.WriteLine($"{streamReader.ReadToEnd()}");
                }
            }
        }
        
        private bool AllFieldsAreValid()
        {
            List<User> retrievedUserList = RetrieveAllUsers();//Guaranteed to be a nonexistent company

            bool firstNameIsOk = Regex.IsMatch(txtFirstName.Text,@"^[a-zA-Z]+$");
           
            bool surnameIsOk = Regex.IsMatch(txtSurname.Text, @"^[a-zA-Z]+$");
            bool usernameIsOk = Regex.IsMatch(txtUsername.Text, @"^[a-zA-Z0-9]+$") && !retrievedUserList.Exists(user => user.Username.Equals(txtUsername.Text.Trim()));
            bool passwordIsOk = txtPassword.Text.Trim().Length >= 7 && txtPassword.Text.Equals(txtConfirmPassword.Text);
            bool companyNameIsOk = (Regex.IsMatch(txtNewCompany.Text.Trim(), @"^[a-zA-Z]+$") 
                && !cBoxCompanyName.Items.Contains(txtNewCompany.Text))
                || (cBoxCompanyName.Items.Contains(cBoxCompanyName.Text));
            if (!firstNameIsOk)
            {
                MessageBox.Show("Please ensure the First Name field is only composed of letters");
            }
            if (!surnameIsOk)
            {
                MessageBox.Show("Please ensure the surname field is only composed of letters");
            }
            if (!usernameIsOk)
            {
                MessageBox.Show("Please ensure the username field is only composed of letters and numbers\n" +
                    "Otherwise please choose a different username as it is already taken");
            }
            if (!passwordIsOk)
            {
                MessageBox.Show("Please ensure the password field is at least 7 characters long \n and that the passwords match");
            }
            if (!companyNameIsOk)
            {
                MessageBox.Show($"Please enter a *new* company composed solely of letters or\nselect an existing company from the drowpdown");
            }
            
            return firstNameIsOk && surnameIsOk && usernameIsOk && passwordIsOk && companyNameIsOk;
        }

        private List<Admin> RetrieveAllAdmins()
        {
            List<Admin> retrievedAdminsList = new List<Admin>();
            try
            {
                using(HttpWebResponse httpWebResponse = WebRequest.Create($"{Server_String}/Admin/RetrieveAll")
                    .GetResponse() as HttpWebResponse)
                {
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        while (!streamReader.EndOfStream)
                        {
                            Admin admin = javaScriptSerializer.Deserialize<Admin>(streamReader.ReadLine());
                            retrievedAdminsList.Add(admin);
                        }
                    }
                    return retrievedAdminsList;
                }
            }

            catch (WebException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        private List<User> RetrieveCompanyUsers(Guid companyId)
        {
            List<User> retrievedUserList = new List<User>();
            try
            {
                using(HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Server_String}/RegularUser/RetrieveBaseUsersByCompanyId?companyId={companyId.ToString()}")
                    .GetResponse() as HttpWebResponse)
                {
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        while (!streamReader.EndOfStream)
                        {
                            User retrievedUser = javaScriptSerializer.Deserialize<User>(streamReader.ReadLine());
                            retrievedUserList.Add(retrievedUser);
                        }
                    }
                    return retrievedUserList;
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        private void btnSignUp_Click_1(object sender, EventArgs e)
        {
            btnSignUp.Invoke(
                new Action(() => {
                    
                    EnableDisableSignUpFields(false);
                    if (AllFieldsAreValid())
                    {
                        FrmLoadingInstance.Show();
                        Enabled = false;
                        ExecuteSignUpLogic(!txtNewCompany.Text.Trim().Equals(string.Empty));
                        FrmLoadingInstance.Hide();
                        Enabled = true;
                    }
                    EnableDisableSignUpFields(true);
                })
             );
            
        }

        private void FrmSignUp_Load(object sender, EventArgs e)
        {

        }

        private void FrmSignUp_Closed(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void cBoxCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menuItemLogin_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = FrmLogin.GetInstance();
            Hide();
            frmLogin.Show();
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtNewCompany_TextChanged(object sender, EventArgs e)
        {
            cBoxCompanyName.Text = string.Empty;
            cBoxCompanyName.Enabled = txtNewCompany.Text.Trim().Length  == 0;
        }

        private void cBoxCompanyName_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cBoxCompanyName_TextChanged(object sender ,EventArgs e)
        {
            txtNewCompany.Text = string.Empty;
            txtNewCompany.Enabled = cBoxCompanyName.Text.Trim().Length == 0;
        }

        private void menuItemRefresh_Click(object sender, EventArgs e)
        {
            InitForm();
        }
    }
}
