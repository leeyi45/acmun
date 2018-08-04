using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;

namespace leeyi45.acmun.AboutBox
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();

            versionLabel.Text = Application.ProductVersion;
            websiteLinkLabel.LinkClicked += WebsiteLinkLabel_LinkClicked;
        }

        private void WebsiteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) 
            => System.Diagnostics.Process.Start("http://github.com/leeyi45/acmun");
    }
}
