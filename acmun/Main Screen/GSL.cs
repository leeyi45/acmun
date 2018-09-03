using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace leeyi45.acmun.Main_Screen
{
    partial class homescreen
    {
        private void InitializeGSL()
        {
            gslSelector.ClickSelect += gslListBox_ClickSelect;

            GSLSpeakingTime = new TimeSpan(0, 1, 30);
            GSLTimeBar.Duration = GSLSpeakingTime;
            GSLTimeBar.RunningChanged += GSLTimerRunningChanged;

            gslTimeSelector.ValueChanged += gslTimeSelector_ValueChanged;
            gslTimeSelector.Value = GSLSpeakingTime;

            gslStartButton.Click += gslStartButton_Click;
            gslPauseButton.Click += gslPauseButton_Click;

            gslPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        TimeSpan GSLSpeakingTime;

        Delegation gslSpeaker;
        Delegation GSLCurrentSpeaker
        {
            get => gslSpeaker;
            set
            {
                gslSpeaker = value;
                gslCountryLabel.Text = value.Name;
                gslPictureBox.ImageLocation = $@"flags\{value.AltName}.png";
            }
        }

        bool yielded_;
        bool yielded
        {
            get => yielded_;
            set
            {
                yielded_ = value;
                gslYieldButton.Enabled = !value;
            }
        }

        private void GSLTimerRunningChanged(object sender, EventArgs e)
        {
            gslStartButton.Enabled = !GSLTimeBar.Running;
            gslPauseButton.Enabled = GSLTimeBar.Running;
            gslTimeSelector.Enabled = !GSLTimeBar.Running;
        }

        #region Buttons
        private void gslStartButton_Click(object sender, EventArgs e)
        {
            if (GSLCurrentSpeaker != null) GSLTimeBar.Start();
        }

        private void gslPauseButton_Click(object sender, EventArgs e) => GSLTimeBar.Stop();

        private void gslRemoveButton_Click(object sender, EventArgs e)
        {
            if (gslSelector.Speakers.Count == 0) return;

            if (gslSelector.ListBoxSelectedItem == null) return;

            var selectedIndex = gslSelector.ListBoxSelectedIndex;
            gslSelector.RemoveSpeaker(gslSelector.ListBoxSelectedIndex);

            if (gslSelector.Speakers.Count == 0) return;

            gslSelector.ListBoxSelectedIndex = Math.Max(0, selectedIndex - 1);
        }

        private void gslClearButton_Click(object sender, EventArgs e) => gslSelector.ClearSpeakers();

        private void gslNextButton_Click(object sender, EventArgs e)
        {
            if (gslSelector.Speakers.Count == 0) return;
            else GSLNextSpeaker();
        }

        private void gslYieldButton_Click(object sender, EventArgs e)
        {
            GSLTimeBar.Stop();
            var yield = new YieldPicker.YieldPicker();

            if (yield.ShowDialog() == DialogResult.OK)
            {
                GSLCurrentSpeaker = Council.Present[yield.YieldIndex];
                yielded = true;
            }

        }
        #endregion

        private void gslListBox_ClickSelect(object sender, int index) => GSLNextSpeaker(index);

        private void gslTimeSelector_ValueChanged(object sender, EventArgs e)
        {
            GSLSpeakingTime = gslTimeSelector.Value;
            GSLTimeBar.Duration = GSLSpeakingTime;
        }

        private void gslComboBox_ItemSelected(object sender, int index) => GSLAddSpeaker(index);

        private void GSLAddSpeaker(int index) 
            => gslSelector.AddSpeaker(Council.Present[index]);

        private void GSLNextSpeaker(int index = 0)
        {
            if (GSLCurrentSpeaker != null)
            {
                GSLCurrentSpeaker.SpeakingTime += GSLTimeBar.ElapsedTime;
                GSLCurrentSpeaker.SpeechCount++;
            }
            GSLTimeBar.Restart();

            GSLCurrentSpeaker = Council.DelsByShortf[gslSelector.Speakers[index]];
            gslSelector.RemoveSpeaker(index);
            yielded = false;
        }

    }
}
