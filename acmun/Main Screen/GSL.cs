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

            //gslComboBox.ItemSelected += gslComboBox_ItemSelected;

            GSLSpeakingTime = new TimeSpan(0, 1, 30);

            GSLTimer = new Clock(GSLSpeakingTime);
            GSLTimer.Tick += GSLTimerTick;
            GSLTimer.TimeUp += GSLTimeUp;
            GSLTimer.RunningChanged += GSLTimerRunningChanged;
            GSLTimer.ResetTriggered += GSLTimerReset;

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

        Clock GSLTimer;

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

        #region Timer
        private void GSLTimerTick(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                var thing = new Action(() => GSLTimerTick(null, EventArgs.Empty));
                Invoke(thing);
            }
            else
            {
                gslTimeLabel.Text = $"{GSLTimer.CurrentTime.ToValString()}/{GSLSpeakingTime.ToValString()}";
                gslProgressBar.Increment(1);
            }

        }

        private void GSLTimeUp(object sender, EventArgs e) 
            => Invoke(new Action(() => 
            {
                gslTimeLabel.ForeColor = Color.Red;
                yielded = true;
            }));
            
        private void GSLTimerReset(object sender, EventArgs e)
        {
            gslProgressBar.Value = 0;
            gslTimeLabel.Text = $"00:00/{GSLSpeakingTime.ToValString()}";
        }

        private void GSLTimerRunningChanged(object sender, EventArgs e)
        {
            gslStartButton.Enabled = !GSLTimer.Running;
            gslPauseButton.Enabled = GSLTimer.Running;
            gslTimeSelector.Enabled = !GSLTimer.Running;
        }
        #endregion

        #region Buttons
        private void gslStartButton_Click(object sender, EventArgs e)
        {
            if (GSLCurrentSpeaker != null) GSLTimer.Start();
        }

        private void gslPauseButton_Click(object sender, EventArgs e) => GSLTimer.Stop();

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
            GSLTimer.Stop();
            var yield = new YieldPicker.YieldPicker();

            if (yield.ShowDialog() == DialogResult.OK)
            {
                GSLCurrentSpeaker = Council.Present[yield.YieldIndex];
                yielded = true;
            }

        }
        #endregion

        #region ListBox
        private void gslListBox_ClickSelect(object sender, int index) => GSLNextSpeaker(index);
        #endregion

        private void gslTimeSelector_ValueChanged(object sender, EventArgs e)
        {
            GSLSpeakingTime = gslTimeSelector.Value;
            gslProgressBar.Maximum = (int)GSLSpeakingTime.TotalSeconds;

            gslTimeLabel.Text = $"{GSLTimer.CurrentTime.ToValString()}/{GSLSpeakingTime.ToValString()}";
            GSLTimer.EditDuration(GSLSpeakingTime);
        }

        private void gslComboBox_ItemSelected(object sender, int index) => GSLAddSpeaker(index);

        private void GSLAddSpeaker(int index) 
            => gslSelector.AddSpeaker(Council.Present[index]);

        private void GSLNextSpeaker(int index = 0)
        {
            if (GSLCurrentSpeaker != null)
            {
                GSLCurrentSpeaker.SpeakingTime += GSLTimer.CurrentTime;
                GSLCurrentSpeaker.SpeechCount++;
            }
            GSLTimer.Restart();

            GSLCurrentSpeaker = Council.CountriesByShortf[gslSelector.Speakers[index]];
            gslSelector.RemoveSpeaker(index);
            yielded = false;
        }

    }
}
