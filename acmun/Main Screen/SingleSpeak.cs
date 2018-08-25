﻿using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace leeyi45.acmun.Main_Screen
{
    partial class homescreen
    {
        private void InitializeSingle()
        {
            singleTimeSelector.ValueChanged += singleSpeak_ValueChanged;

            singleListBox.SelectedIndexChanged += singleListBox_SelectionChanged;

            SingleTimer = new Clock();
            SingleTimer.EditDuration(new TimeSpan(0, 1, 30));

            SingleTimer.RunningChanged += SingleTimerRunningChanged;
            SingleTimer.Tick += SingleTimerTick;
            SingleTimer.TimeUp += SingleTimer_TimeUp;
            SingleTimer.ResetTriggered += SingleTimer_ResetTriggered;

            singlePictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

            singleStartButton.Click += singleStartButton_Click;
            singlePauseButton.Click += singlePauseButton_Click;
            singleResetButton.Click += singleResetButton_Click;
        }

        private void SingleTimer_ResetTriggered(object sender, EventArgs e) => singleTimeLabel.ForeColor = Color.Black;

        private void SingleTimer_TimeUp(object sender, EventArgs e) => singleTimeLabel.ForeColor = Color.Red;

        Clock SingleTimer;

        TimeSpan SingleSpeakTotal;

        Delegation singleSpeak;
        Delegation SingleSpeaker
        {
            get => singleSpeak;
            set
            {
                if (singleSpeak != null) singleSpeak.SpeakingTime += SingleTimer.CurrentTime;

                singleSpeak = value;
                singleCountryTextBox.Text = value?.Name ?? String.Empty;
                singlePictureBox.ImageLocation = $@"flags\{value?.AltName ?? String.Empty}.png";
            }
        }

        private void LoadSingleSpeak(Delegation speaker, TimeSpan duration)
        {
            singleProgressBar.Value = 0;
            SingleSpeakTotal = duration;
            SingleTimer.EditDuration(duration);
            SingleSpeaker = speaker;

            singleProgressBar.Maximum = (int)duration.TotalSeconds;

            singleTimeSelector.Value = duration;

            var index = 0;
            for(int i = 0; i < Council.Present.Length; i++)
            {
                if(speaker == Council.Present[i])
                {
                    index = i;
                    break;
                }
            }

            singleListBox.SelectedIndex = index;
        }

        private void SingleTimerRunningChanged(object sender, EventArgs e)
        {
            singleStartButton.Enabled = !SingleTimer.Running;
            singlePauseButton.Enabled = SingleTimer.Running;
            singleTimeSelector.Enabled = !SingleTimer.Running;
            singleListBox.Enabled = !SingleTimer.Running;
        }

        private void SingleTimerTick(object sender, EventArgs e)
        {
            singleProgressBar.Increment(1);
            singleTimeLabel.Text = $"{SingleTimer.CurrentTime.ToValString()}/{SingleSpeakTotal.ToValString()}";
        }

        private void singleSpeak_ValueChanged(object sender, EventArgs e)
        {
            var time = singleTimeSelector.Value;
            SingleSpeakTotal = time;
            SingleTimer.EditDuration(time);
            singleTimeLabel.Text = $"{SingleTimer.CurrentTime.ToValString()}/{SingleSpeakTotal.ToValString()}";
        }

        private void singleListBox_SelectionChanged(object sender, EventArgs e)
        {
            if(!SingleTimer.Running)
            {
                LoadSingleSpeak(Council.Present[singleListBox.SelectedIndex], SingleTimer.Duration);
            }
        }

        private void singleStartButton_Click(object sender, EventArgs e)
            => SingleTimer.Start();

        private void singlePauseButton_Click(object sender, EventArgs e)
            => SingleTimer.Stop();

        private void singleResetButton_Click(object sender, EventArgs e)
        {
            SingleTimer.Reset();
            SingleSpeaker = null;
        }
    }
}
