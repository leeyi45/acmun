using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace leeyi45.acmun.Main_Screen
{
    partial class homescreen
    {
        private void InitializeDebate()
        {
            DebateSpeakCount = Council.DebateSpeakCount;
            DebateSpeakTime = Council.DebateSpeakTime;

            DebateTimeBar.RunningChanged += DebateTimerRunning_Changed;

            debateTimeSelector.ValueChanged += debateTimeSelector_ValueChanged;
            debateCountSelector.ValueChanged += debateCountSelector_ValueChanged;

            debateARemoveButton.Click += debateRemoveACountry_Click;
            debateFRemoveButton.Click += debateRemoveFCountry_Click;

            debateAClearButton.Click += debateClearACountry_Click;
            debateFClearButton.Click += debateClearFCountry_Click;

            debateNextButton.Click += debateNextButton_Click;
            debateStartButton.Click += debateStartButton_Click;
            debateStopButton.Click += debatePauseButton_Click;

            debatePictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

            LoadDebate();
        }

        private void DebateTimerRunning_Changed(object sender, EventArgs e)
        {
            debateTimeSelector.Enabled = !DebateTimeBar.Running;
            debateStopButton.Enabled = DebateTimeBar.Running;
            debateStartButton.Enabled = !DebateTimeBar.Running;
        }

        private void LoadDebate()
        {
            debateCountSelector.Value = DebateSpeakCount;
            debateTimeSelector.Value = DebateSpeakTime;

            DebateTimeBar.Duration = DebateSpeakTime;
            DebateTimeBar.Reset();

            debateFSelector.ClearSpeakers();
            debateASelector.ClearSpeakers();

            debateForTextBox.ForeColor = System.Drawing.Color.Green;
            debateForTextBox.Text = "For";

            debateCountryLabel.Text = "";

            IsFor = true;
        }

        private int DebateSpeakCount;

        private TimeSpan DebateSpeakTime;

        private Delegation debateSpeaker;
        private Delegation DebateSpeaker
        {
            get => debateSpeaker;
            set
            {
                debateSpeaker = value;
                debateCountryLabel.Text = value.Name;
                debatePictureBox.ImageLocation = $@"flags\{value.AltName}.png";
            }
        }

        private bool IsFor;

        private void debateTimeSelector_ValueChanged(object sender, EventArgs e)
        {
            DebateTimeBar.Duration = debateTimeSelector.Value;
            DebateSpeakTime = debateTimeSelector.Value;
        }

        private void debateCountSelector_ValueChanged(object sender, EventArgs e)
        {
            var newval = (int)debateCountSelector.Value;

            if (newval < DebateSpeakCount)
            {
                var diff = DebateSpeakCount - newval;
                debateASelector.Speakers.RemoveRange(diff - 1, diff);
                debateASelector.Speakers.RemoveRange(diff - 1, diff);
            }

            DebateSpeakCount = newval;
        }

        #region Buttons
        private void debateRemoveACountry_Click(object sender, EventArgs e)
        {
            if (debateASelector.ListBoxSelectedItem == null) return;

            debateASelector.RemoveSpeaker(debateASelector.ListBoxSelectedIndex);
        }

        private void debateRemoveFCountry_Click(object sender, EventArgs e)
        {
            if (debateFSelector.ListBoxSelectedItem == null) return;

            debateFSelector.RemoveSpeaker(debateFSelector.ListBoxSelectedIndex);
        }

        private void debateClearACountry_Click(object sender, EventArgs e) => debateASelector.ClearSpeakers();

        private void debateClearFCountry_Click(object sender, EventArgs e) => debateFSelector.ClearSpeakers();

        private void debateNextButton_Click(object sender, EventArgs e)
            => DebateNextSpeaker();

        private void debatePauseButton_Click(object sender, EventArgs e)
            => DebateTimeBar.Stop();

        private void debateStartButton_Click(object sender, EventArgs e)
        {
            if (DebateSpeaker != null) DebateTimeBar.Start();
            else DebateNextSpeaker();
        }

        private void debateResetButton_Click(object sender, EventArgs e)
            => LoadDebate();
        #endregion

        private void DebateNextSpeaker()
        {
            DebateTimeBar.Reset();

            Delegation nextSpeaker;

            if (IsFor)
            {
                if (debateFSelector.Speakers.Count == 0)
                {
                    IsFor = false;
                    return;
                }

                nextSpeaker = Council.DelsByShortf[debateFSelector.Speakers[0]];
                debateFSelector.RemoveSpeaker(0);
                debateForTextBox.ForeColor = System.Drawing.Color.Green;
                debateForTextBox.Text = "For";
                IsFor = false;
            }
            else
            {
                if (debateASelector.Speakers.Count == 0)
                {
                    IsFor = true;
                    return;
                }

                nextSpeaker = Council.DelsByShortf[debateASelector.Speakers[0]];
                debateASelector.RemoveSpeaker(0);
                debateForTextBox.ForeColor = System.Drawing.Color.Red;
                debateForTextBox.Text = "Against";
                IsFor = true;
            }

            if (DebateSpeaker != null) DebateSpeaker.SpeakingTime.Add(DebateTimeBar.ElapsedTime);

            DebateSpeaker = nextSpeaker;
            DebateTimeBar.Start();

        }
    }
}
