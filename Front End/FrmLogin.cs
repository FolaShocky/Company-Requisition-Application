using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Http;
using WebApplication1.Models;
using static WebApplication1.Models.Constants;
using System.Web.Script.Serialization;
namespace DissertWindowsFormApplication
{
    public partial class FrmLogin : Form
    {
        private static FrmLogin FrmLoginInstance;
        //Was initally public - could cause problems if changed
        private FrmLogin()
        {
            InitializeComponent();
            CreateHandle();
            txtPassword.PasswordChar = '\u2022';
        }

        public static FrmLogin GetInstance()
        {
            if (FrmLoginInstance == null)
                FrmLoginInstance = new FrmLogin();
            FrmLoginInstance.FormClosed += new FormClosedEventHandler(delegate(object sender, FormClosedEventArgs e){
                Application.Exit();
            });
               
            return FrmLoginInstance;
        }
    
        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Invoke(new Action(()=> {
                User abstractUser = LoggedInUser();
                Utility.LoggedInUser = abstractUser;
                
                if (Utility.LoggedInUser != null)
                {
                    Hide();
                    FrmMainMenu frmMainMenu = FrmMainMenu.GetInstance();
                    FrmMainMenuAdmin frmMainMenuAdmin = FrmMainMenuAdmin.GetInstance();
                    txtUsername.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    if (Utility.LoggedInUser is RegularUser)
                        frmMainMenu.Show();
                    else
                        frmMainMenuAdmin.Show();
                }
                else
                {
                    MessageBox.Show("Your login details are incorrect.\n" +
                        "Please try again.");
                }
            }));
        }

        private void RetrieveAllCompanies()
        {
            List<Company> companyList = new List<Company>();
            try
            {
                using (HttpWebResponse httpWebResponse = WebRequest.Create($"{Constants.Server_String}/Company/RetrieveCompanies").GetResponse() as HttpWebResponse)
                {
                    using(StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                            Company retrievedCompany = javaScriptSerializer.Deserialize<Company>(streamReader.ReadLine());
                            companyList.Add(retrievedCompany);
                            companyList.ForEach(company => {
                                
                            });
                        }
                    }
                }
            }
            catch(IOException ex)
            {
                Console.WriteLine($"An exception occurred:{ex.Message}");
            }
        }
        private User LoggedInUser()
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            StringBuilder userTypeStringBuilder = new StringBuilder(cBoxAdmin.Checked ? Constants.User_Type_Admin : Constants.User_Type_Regular_User); 
            User abstractUser = null;
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                using(HttpWebResponse httpWebResponse = WebRequest.Create(
                    $"{Constants.Server_String}/{userTypeStringBuilder.ToString()}/" +
                    $"UsernamePasswordRetrieval?username={username}&password={password}").GetResponse() as HttpWebResponse) {
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            string line = streamReader.ReadLine();
                            if (!string.IsNullOrEmpty(line))
                            {
                                if (userTypeStringBuilder.ToString().Contains(Constants.User_Type_Admin))
                                    abstractUser = javaScriptSerializer.Deserialize<Admin>(line);
                                else
                                    abstractUser = javaScriptSerializer.Deserialize<RegularUser>(line);
                                Utility.LoggedInUser = abstractUser;
                                break;
                            }
                        }
                        if (abstractUser.Id.Equals(Guid.Empty))
                            return null;
                        return abstractUser;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An exception occurred:{ex.Message}");
                return null;
            }
        }
        private void FrmLogin_Closed(object sender, EventArgs e)
        {
            Application.Exit();
        }
     
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //Invoke(new Action(RetrieveAllCompanies));
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)//Sign Up MenuItem
        {
            FrmSignUp frmSignUp = FrmSignUp.GetInstance();
            Hide();
            frmSignUp.Show();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//cBoxShowPassword
        {
            txtPassword.PasswordChar = cBoxShowPassword.Checked ? '\0' : '\u2022';
        }

        private void cBoxAdmin_CheckedChanged(object sender, EventArgs e)
        {
            
                
        }
    }

}
