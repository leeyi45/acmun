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
            gslListBox.DragDone += gslListBox_DragDone;
            gslListBox.ClickSelect += gslListBox_ClickSelect;

            gslComboBox.ItemSelected += gslComboBox_ItemSelected;

            GSLSpeakingTime = new TimeSpan(0, 1, 30);

            GSLTimer = new Clock(GSLSpeakingTime);
            GSLTimer.Tick += TimerTick;
            GSLTimer.TimeUp += TimeUp;
            GSLTimer.Started += TimerStarted;
            GSLTimer.Stopped += TimerStopped;
            GSLTimer.ResetTriggered += TimerReset;

            gslTimeSelector.ValueChanged += gslTimeSelector_ValueChanged;
            gslTimeSelector.Value = GSLSpeakingTime;

            gslPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        List<Country> GSLList = new List<Country>();

        TimeSpan GSLSpeakingTime;

        Country gslSpeaker;
        Country GSLCurrentSpeaker
        {
            get => gslSpeaker;
            set
            {
                gslSpeaker = value;
                gslCountryLabel.Text = value.Name;
                gslPictureBox.ImageLocation = $@"flags\{value.Shortf}.png";
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
        private void TimerTick(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                var thing = new Action(() => TimerTick(null, EventArgs.Empty));
                Invoke(thing);
            }
            else
            {
                gslTimeLabel.Text = $"{GSLTimer.CurrentTime.ToValString()}/{GSLSpeakingTime.ToValString()}";
                gslProgressBar.Increment(1);
            }

        }

        private void TimeUp(object sender, EventArgs e) 
            => Invoke(new Action(() => 
            {
                gslTimeLabel.ForeColor = Color.Red;
                yielded = true;
            }));

        private void TimerStopped(object sender, EventArgs e)
        {
            gslTimeSelector.Enabled = true;
            gslProgressBar.ForeColor = Color.Red;

            gslStartButton.Enabled = true;
            gslStartButton.Text = "Start";

            gslPauseButton.Enabled = false;
            gslPauseButton.Text = "Paused";
        }

        private void TimerStarted(object sender, EventArgs e)
        {
            gslTimeSelector.Enabled = false;
            gslProgressBar.ForeColor = Color.Green;
            gslTimeLabel.ForeColor = Color.Black;

            gslStartButton.Enabled = false;
            gslStartButton.Text = "Started";

            gslPauseButton.Enabled = true;
            gslPauseButton.Text = "Pause";
        }
            
        private void TimerReset(object sender, EventArgs e)
        {
            gslProgressBar.Value = 0;
            gslTimeLabel.Text = $"00:00/{GSLSpeakingTime.ToValString()}";
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
            if (gslListBox.Items.Count == 0) return;

            if (gslListBox.SelectedItem == null) return;

            var selectedIndex = gslListBox.SelectedIndex;
            gslListBox.Items.RemoveAt(gslListBox.SelectedIndex);

            if (gslListBox.Items.Count == 0) return;

            gslListBox.SelectedIndex = Math.Max(0, selectedIndex - 1);
        }

        private void gslClearButton_Click(object sender, EventArgs e)
        {
            gslListBox.Items.Clear();
            gslComboBox.Text = "";
        }

        private void gslAddButton_Click(object sender, EventArgs e)
        {
            if (gslComboBox?.SelectedItem == null) return;

            var country = Council.Present[gslComboBox.SelectedIndex];
            GSLList.Insert(GSLList.Count, country);
            gslListBox.Items.Add(country.Name);

            gslComboBox.SelectedIndex = -1;
        }

        private void gslNextButton_Click(object sender, EventArgs e)
        {
            if (gslListBox.Items.Count == 0) return;
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

        private void gslListBox_DragDone(object sender, int oldIndex, int newIndex)
        {
            var data = GSLList[oldIndex];
            GSLList.RemoveAt(oldIndex);
            GSLList.Insert(newIndex, data);
        }
        #endregion

        private void gslTimeSelector_ValueChanged(object sender, EventArgs e)
        {
            GSLSpeakingTime = gslTimeSelector.Value;
            gslProgressBar.Maximum = (int)GSLSpeakingTime.TotalSeconds;

            gslTimeLabel.Text = GSLTimer.CurrentTime.ToString(@"mm\:ss") + "/" + GSLSpeakingTime.ToString(@"mm\:ss");
            GSLTimer.EditDuration(GSLSpeakingTime);
        }

        private void gslComboBox_ItemSelected(object sender, int index) => GSLAddSpeaker(index);

        private void GSLAddSpeaker(int index)
        {
            var country = Council.Present[index];
            GSLList.Insert(GSLList.Count, country);
            gslListBox.Items.Add(country.Name);
        }

        private void GSLNextSpeaker(int index = 0)
        {
            if(GSLCurrentSpeaker != null) GSLCurrentSpeaker.SpeakingTime += GSLTimer.CurrentTime;
            GSLTimer.Restart();

            GSLCurrentSpeaker = GSLList[index];
            GSLList.RemoveAt(index);

            gslListBox.Items.RemoveAt(index);
            yielded = false;
        }

    }
}
