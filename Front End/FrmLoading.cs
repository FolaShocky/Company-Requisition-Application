using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DissertWindowsFormApplication
{
    public partial class FrmLoading : Form
    {
        private Timer labelTextTransitionTimer;
        private Timer labelColourTransitionTimer;
        private static FrmLoading FrmLoadingInstance;
        private StringBuilder loadingStringBuilder;
        public static FrmLoading GetInstance()
        {
            if (FrmLoadingInstance == null)
            {
                FrmLoadingInstance = new FrmLoading();
            }
            return FrmLoadingInstance;
        }
        private FrmLoading()
        {
            InitializeComponent();
            ControlBox = false;
            Text = string.Empty;
            lblLoading.Text = "Loading";
            loadingStringBuilder = new StringBuilder(lblLoading.Text);
            labelTextTransitionTimer = new Timer();
            labelColourTransitionTimer = new Timer();
            labelColourTransitionTimer.Interval = 3000;
            labelTextTransitionTimer.Interval = 500;
            labelTextTransitionTimer.Tick += new EventHandler(delegate (object sender, EventArgs eventArgs)
            {
                switch (loadingStringBuilder.ToString()) {
                    case "Loading...":
                    {
                        loadingStringBuilder.Clear();
                        loadingStringBuilder.Append("Loading");
                        lblLoading.Text = loadingStringBuilder.ToString();
                        break;
                    }
                    default:
                    {
                        loadingStringBuilder.Append(".");
                        lblLoading.Text = loadingStringBuilder.ToString();
                        break;
                    }
                }
            });
            labelColourTransitionTimer.Tick += new EventHandler(delegate (object sender, EventArgs eventArgs)
            {
                
                Color labelForeColour = lblLoading.ForeColor;
                switch (labelForeColour.ToKnownColor())
                {
                    case KnownColor.Blue:
                    {
                        lblLoading.ForeColor = Color.Green;
                        break;
                    }
                    case KnownColor.Green:
                    {
                        lblLoading.ForeColor = Color.Red;
                        break;
                    }
                    default:
                    {
                        lblLoading.ForeColor = Color.Blue;
                        break;
                    }
                }
            });
            labelColourTransitionTimer.Start();
            labelTextTransitionTimer.Start();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FrmLoading_Load(object sender, EventArgs e)
        {

        }

        private void lblLoading_Click(object sender, EventArgs e)
        {

        }
    }
}
