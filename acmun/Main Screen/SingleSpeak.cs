using System;
using System.Windows.Forms;

namespace leeyi45.acmun.Main_Screen
{
    partial class homescreen
    {
        private void InitializeSingle()
        {
            singleTimeSelector.ValueChanged += singleSpeak_ValueChanged;

            singleListBox.SelectedIndexChanged += singleListBox_SelectionChanged;

            SingleTimer = new Clock();

            SingleTimer.Started += SingleTimerStarted;
            SingleTimer.Stopped += SingleTimerStopped;
            SingleTimer.Tick += SingleTimerTick;

            singlePictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        Clock SingleTimer;

        TimeSpan SingleSpeakTotal;

        Country singleSpeak;
        Country SingleSpeaker
        {
            get => singleSpeak;
            set
            {
                singleSpeak = value;
                singleCountryTextBox.Text = value.Name;
                singlePictureBox.ImageLocation = $@"flags\{value.Shortf}.png";
            }
        }

        private void LoadSingleSpeak(Country speaker, TimeSpan duration)
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

        private void SingleTimerStarted(object sender, EventArgs e)
        {
            singleStartButton.Enabled = false;
            singleStartButton.Text = "Started";

            singlePauseButton.Enabled = true;
            singlePauseButton.Text = "Pause";

            singleTimeSelector.Enabled = false;
        }

        private void SingleTimerStopped(object sender, EventArgs e)
        {
            singleStartButton.Enabled = true;
            singleStartButton.Text = "Start";

            singlePauseButton.Enabled = false;
            singlePauseButton.Text = "Paused";

            singleTimeSelector.Enabled = true;
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
        }
    }
}
