using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DissertWindowsFormApplication
{
    public partial class FrmActivity : Form
    {
        private static FrmActivity frmActivityInstance;
        private FrmActivity()
        {
            InitializeComponent();
            
        }
        public static FrmActivity GetInstance()
        {
            if (frmActivityInstance == null)
                frmActivityInstance = new FrmActivity();
            return frmActivityInstance;
        }
        private void btnChat_Click(object sender, EventArgs e)
        {

        }
    }
}
