using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace leeyi45.acmun.MotionCreator
{
    public partial class MotionCreator : Form
    {
        public MotionCreator()
        {
            InitializeComponent();
            UpdateMotions();

            typeComboBox.SelectedIndexChanged += typeComboBox_SelectChanged;
            StartPosition = FormStartPosition.CenterParent;
        }

        public new void ShowDialog()
        {
            countryComboBox.SelectedIndex = -1;
            typeComboBox.SelectedIndex = -1;
            topicTextBox.Clear();
            countryComboBox.Items.AddRange(Council.Present.Select(x => x.Shortf).ToArray());
            base.ShowDialog();
        }

        public void UpdateMotions()
        {
            typeComboBox.Items.AddRange(Council.Motions.Values.Select(x => x.Name).ToArray());
        }

        private void LoadMotion(MotionData motion)
        {
            totalTimeSelector.Enabled = motion.Duration;
            speakTimeSelector.Enabled = motion.SpeakTime;
            topicTextBox.Enabled = motion.Topic;
        }

        private void typeComboBox_SelectChanged(object sender, EventArgs e)
        {
            if (typeComboBox.SelectedIndex < 0) return;
            LoadMotion(Council.MotionsAsList[typeComboBox.SelectedIndex]);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if(typeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a motion type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(countryComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select the delegation that proposed the motion", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (totalTimeSelector.Enabled && totalTimeSelector.Value == TimeSpan.Zero)
            {
                MessageBox.Show("Total time cannot be zero", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (speakTimeSelector.Enabled && speakTimeSelector.Value == TimeSpan.Zero)
            {
                MessageBox.Show("Speaking time cannot be zero", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Result = new Motion(Council.MotionsAsList[typeComboBox.SelectedIndex],
               topicTextBox.Text, Council.Present[countryComboBox.SelectedIndex], speakTimeSelector.Value,
               totalTimeSelector.Value);
            }
            catch(Motion.MissingDataException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult = DialogResult.Yes;

        }

        public Motion Result { get; private set; }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void topicTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape && !string.IsNullOrWhiteSpace(topicTextBox.Text))
            {
                topicTextBox.Clear();
            } 
        }
    }
}
