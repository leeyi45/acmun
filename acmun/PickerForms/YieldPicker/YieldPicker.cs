using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static leeyi45.acmun.Council;

namespace leeyi45.acmun.YieldPicker
{
    public partial class YieldPicker : Form
    {
        public YieldPicker()
        {
            InitializeComponent();

            countryBox.Items.AddRange(PresentShortf);
            countryBox.SelectedIndexChanged += CountryBox_SelectedIndexChanged;
        }

        private void CountryBox_SelectedIndexChanged(object sender, EventArgs e)
            => yieldButton.Enabled = (countryBox.SelectedIndex != -1);

        private void yieldButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public int YieldIndex => countryBox.SelectedIndex;
    }
}
