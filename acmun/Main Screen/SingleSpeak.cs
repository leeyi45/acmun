using System;
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

            SingleTimeBar.Duration = new TimeSpan(0, 1, 30);

            SingleTimeBar.RunningChanged += SingleTimerRunningChanged;

            singlePictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

            singleStartButton.Click += singleStartButton_Click;
            singlePauseButton.Click += singlePauseButton_Click;
            singleResetButton.Click += singleResetButton_Click;
        }

        TimeSpan SingleSpeakTotal;

        Delegation singleSpeak;
        Delegation SingleSpeaker
        {
            get => singleSpeak;
            set
            {
                if (singleSpeak != null) singleSpeak.SpeakingTime += SingleTimeBar.ElapsedTime;

                singleSpeak = value;
                singleCountryTextBox.Text = value?.Name ?? String.Empty;
                singlePictureBox.ImageLocation = $@"flags\{value?.AltName ?? String.Empty}.png";
            }
        }

        private void LoadSingleSpeak(Delegation speaker, TimeSpan duration)
        {
            SingleSpeakTotal = duration;
            SingleTimeBar.Duration = duration;
            SingleSpeaker = speaker;

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
            singleStartButton.Enabled = !SingleTimeBar.Running;
            singlePauseButton.Enabled = SingleTimeBar.Running;
            singleTimeSelector.Enabled = !SingleTimeBar.Running;
            singleListBox.Enabled = !SingleTimeBar.Running;
        }

        private void singleSpeak_ValueChanged(object sender, EventArgs e)
        {
            var time = singleTimeSelector.Value;
            SingleSpeakTotal = time;
            SingleTimeBar.Duration = time;
        }

        private void singleListBox_SelectionChanged(object sender, EventArgs e) 
            => LoadSingleSpeak(Council.Present[singleListBox.SelectedIndex], SingleTimeBar.Duration);

        private void singleStartButton_Click(object sender, EventArgs e)
            => SingleTimeBar.Start();

        private void singlePauseButton_Click(object sender, EventArgs e)
            => SingleTimeBar.Stop();

        private void singleResetButton_Click(object sender, EventArgs e)
        {
            SingleTimeBar.Reset();
            SingleSpeaker = null;
        }
    }
}
