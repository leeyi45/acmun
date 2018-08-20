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

            DebateTimer = new Clock();
            DebateTimer.Tick += DebateTimerTick;
            DebateTimer.RunningChanged += DebateTimerRunning_Changed;

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
            debateTimeSelector.Enabled = !DebateTimer.Running;
            debateStopButton.Enabled = DebateTimer.Running;
            debateStartButton.Enabled = !DebateTimer.Running;
        }

        private void LoadDebate()
        {
            debateCountSelector.Value = DebateSpeakCount;
            debateTimeSelector.Value = DebateSpeakTime;

            DebateTimer.EditDuration(DebateSpeakTime);

            debateTimeLabel.Text = $"00:00/{DebateSpeakTime.ToValString()}";
            debateProgressBar.Value = 0;
            debateProgressBar.Maximum = (int)DebateSpeakTime.TotalSeconds;

            DebateTimer.Reset();

            debateFSelector.ClearSpeakers();
            debateASelector.ClearSpeakers();

            debateForTextBox.ForeColor = System.Drawing.Color.Green;
            debateForTextBox.Text = "For";

            debateCountryLabel.Text = "";

            IsFor = true;
        }

        private void DebateTimerTick(object sender, EventArgs e)
        {
            debateProgressBar.Increment(1);
            debateTimeLabel.Text = $"{DebateTimer.CurrentTime.ToValString()}/{DebateSpeakTime.ToValString()}";
        }

        private int DebateSpeakCount;

        //private List<Delegation> debateACountryListBox.Speakers;
        //private List<Delegation> debateFCountryListBox.Speakers;

        private Clock DebateTimer;
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
            DebateTimer.EditDuration(debateTimeSelector.Value);
            DebateSpeakTime = debateTimeSelector.Value;
            debateProgressBar.Maximum = (int)DebateSpeakTime.TotalSeconds;
            debateTimeLabel.Text = $"{DebateTimer.CurrentTime.ToValString()}/{DebateSpeakTime.ToValString()}";
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
            => DebateTimer.Stop();

        private void debateStartButton_Click(object sender, EventArgs e)
        {
            if (DebateSpeaker != null) DebateTimer.Start();
            else DebateNextSpeaker();
        }

        private void debateResetButton_Click(object sender, EventArgs e)
            => LoadDebate();
        #endregion

        private void DebateNextSpeaker()
        {
            DebateTimer.Reset();
            debateTimeLabel.Text = $"00:00/{DebateSpeakTime.ToValString()}";
            debateProgressBar.Value = 0;

            Delegation nextSpeaker;

            if (IsFor)
            {
                if (debateFSelector.Speakers.Count == 0)
                {
                    IsFor = false;
                    return;
                }

                nextSpeaker = Council.CountriesByShortf[debateFSelector.Speakers[0]];
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

                nextSpeaker = Council.CountriesByShortf[debateASelector.Speakers[0]];
                debateASelector.RemoveSpeaker(0);
                debateForTextBox.ForeColor = System.Drawing.Color.Red;
                debateForTextBox.Text = "Against";
                IsFor = true;
            }

            if (DebateSpeaker != null) DebateSpeaker.SpeakingTime.Add(DebateTimer.CurrentTime);

            DebateSpeaker = nextSpeaker;
            DebateTimer.Start();

        }
    }
}
