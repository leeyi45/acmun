using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace leeyi45.acmun.TimeExt
{
    public partial class timeExt : Form
    {
        public timeExt(TimeSpan Used, TimeSpan Original, bool isMod)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            timeLeftLabel.Text = $"{Used.ToValString()}/{Original.ToValString()}";
            purposeLabel.Text = $"Extend time for {(isMod ? "moderated" : "unmoderated" + " caucus")}";
            StartPosition = FormStartPosition.CenterParent;
        }

        public TimeSpan Result => timeSelector1.Value;

        private void extendButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
