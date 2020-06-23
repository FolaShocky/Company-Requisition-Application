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
namespace DissertWindowsFormApplication
{
    public partial class FrmEntry : Form
    {
        //Was initally public - could cause problems if changed
        private static FrmEntry FrmEntryInstance;
        private FrmEntry()
        {
            InitializeComponent();
            Show();
        }
        

        public static FrmEntry GetInstance()
        {
            if (FrmEntryInstance == null)
                FrmEntryInstance = new FrmEntry();
            User loggedInUser = Utility.LoggedInUser;
            FrmEntryInstance.menuItemUsername.Text = loggedInUser != null ? $"Logged in as: {loggedInUser.Username}" : "Not Logged In"; 
            return FrmEntryInstance;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = FrmLogin.GetInstance();
            Hide();
            frmLogin.Show();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            FrmSignUp frmSignUp = FrmSignUp.GetInstance();
            Hide();
            frmSignUp.Show();
        }

        private void FrmEntry_Closed(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmEntry_Load(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FrmSignUp frmSignUp = FrmSignUp.GetInstance();
            Hide();
            frmSignUp.Show();
        }

        private void menuItemLogin_Click(object sender, EventArgs e)//SignUp MenuItem
        {
            FrmLogin frmLogin = FrmLogin.GetInstance();
            Hide();
            frmLogin.Show();
        }

        private void menuItemLogOut_Click(object sender, EventArgs e)
        {
            if (Utility.LoggedInUser != null)
            {
                Utility.LoggedInUser = null;
                Utility.SelectedChat = null;
                Utility.SelectedChatParticipant = null;
                MessageBox.Show("Successfully logged out");
            }
            else
                MessageBox.Show("Already logged out");
        }
    }
}
