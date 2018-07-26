using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace leeyi45.acmun.Vote_Settings
{
    public partial class VoteSet : Form
    {
        public VoteSet()
        {
            InitializeComponent();

            checkBox1.Checked = Council.FiftyPlus1;
            checkBox2.Checked = Council.TwoThirdPlus1;

            unmodTimeSelector.ValueChanged += unmodTimeSelector_ValueChanged;
            unmodTimeSelector.Value = Council.UnmodSummaryTime;

            debateTimeSelector.ValueChanged += debateTimeSelector_ValueChanged;
            debateTimeSelector.Value = Council.DebateSpeakTime;
            numericUpDown1.Value = Council.DebateSpeakCount;

            textBox1.Text = Council.Name;

            StartPosition = FormStartPosition.CenterParent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Council.FiftyPlus1 = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Council.TwoThirdPlus1 = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Council.SaveSpeakTime = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Council.SaveMotions = checkBox4.Checked;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Council.TrackDebate = checkBox5.Checked;
        }

        private void unmodTimeSelector_ValueChanged(object sender, EventArgs e)
        {
            Council.UnmodSummaryTime = unmodTimeSelector.Value;
        }

        private new void Close()
        {
            Council.Name = textBox1.Text;
            Program.Instance.UpdateQuorum();
            base.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Council.DebateSpeakCount = (int)numericUpDown1.Value;
        }
        
        private void debateTimeSelector_ValueChanged(object sender, EventArgs e)
        {
            Council.DebateSpeakTime = debateTimeSelector.Value;
        }
    }
}
