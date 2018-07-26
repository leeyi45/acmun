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

namespace leeyi45.acmun.Roll_Call
{
    public partial class rollCallScreen : Form
    {
        public rollCallScreen(string councilName)
        {
            InitializeComponent();

            rollCallBox.ItemCheck += rollCallBox_Item_Checked;
            rollCallBox.SelectedIndexChanged += rollCallBox_SelectedChanged;
            observerCheckBox.CheckedChanged += observerCheckBox_CheckedChanged;
            allPresentCheckBox.CheckedChanged += allPresentCheckBox_CheckedChanged;
            quorumLabel.Text = $"{rollCallBox.CheckedItems.Count}/{CountryCount}";
            councilNameLabelBox.Text = councilName;
            StartPosition = FormStartPosition.CenterParent;
        }

        private int rollCallCount;

        private void rollCallScreen_Load(object sender, EventArgs e)
        {
            rollCallBox.Items.AddRange(CountryList.Select(x => x.Name).ToArray());

            var arr = CountryList;

            for(int i = 0; i < arr.Length; i++)
            {
                rollCallBox.SetItemChecked(i, arr[i].Present);
            }

            rollCallCount = PresentCount;
        } 

        private void rollCallBox_Item_Checked(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked) rollCallCount++;
            else rollCallCount--;

            CountryList[e.Index].Present = e.NewValue == CheckState.Checked;

            quorumLabel.Text = $"{rollCallCount}/{CountryCount}";

            //allPresentCheckBox.CheckedChanged -= allPresentCheckBox_CheckedChanged;
            //if (rollCallCount == rollCallBox.Items.Count) allPresentCheckBox.Checked = true;
            //else allPresentCheckBox.Checked = false;
            //allPresentCheckBox.CheckedChanged += allPresentCheckBox_CheckedChanged;
            
        }

        private void rollCallBox_SelectedChanged(object sender, EventArgs e)
        {
            if (rollCallBox.SelectedIndex == -1) observerCheckBox.Enabled = false;
            else
            {
                observerCheckBox.Enabled = true;
                observerCheckBox.CheckedChanged -= observerCheckBox_CheckedChanged;
                observerCheckBox.Checked = CountryList[rollCallBox.SelectedIndex].Observer;
                observerCheckBox.CheckedChanged += observerCheckBox_CheckedChanged;
            }
        }

        private void observerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rollCallBox.SelectedIndex == -1) return;

            CountryList[rollCallBox.SelectedIndex].Observer = observerCheckBox.Checked;
            if (CountryList[rollCallBox.SelectedIndex].Observer) rollCallBox.Items[rollCallBox.SelectedIndex] += " (Observer)";
            else rollCallBox.Items[rollCallBox.SelectedIndex] = CountryList[rollCallBox.SelectedIndex].Shortf;
        }

        private void allPresentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < rollCallBox.Items.Count; i++)
            {
                rollCallBox.SetItemChecked(i, allPresentCheckBox.Checked);
            }

            rollCallCount = (allPresentCheckBox.Checked ? rollCallBox.Items.Count : 0);
        }

        private void doneButton_Click(object sender, EventArgs e)
            => Close();

        private new void Close()
        {
            UpdatePresent();

           if(PresentCount == 0)
            {
                switch (MessageBox.Show("There are no countries selected!\n Proceed?", "Warning", 
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                {
                    case DialogResult.Cancel: return;
                }
            }

            Program.Instance.UpdateQuorum();
            base.Close();
        }
    }
}
