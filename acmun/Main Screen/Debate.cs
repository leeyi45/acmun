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

            debateAddACountry.Click += debateAddACountry_Click;
            debateAddFCountry.Click += debateAddFCountry_Click;

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

            debateACountryListBox.Items.Clear();
            debateFCountryListBox.Items.Clear();

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
                debateACountryListBox.Speakers.RemoveRange(diff - 1, diff);
                debateFCountryListBox.Speakers.RemoveRange(diff - 1, diff);
            }

            DebateSpeakCount = newval;
        }

        #region Buttons
        private void debateAddACountry_Click(object sender, EventArgs e)
        {
            if (debateASelector.SelectedItem == null) return;
            if (debateACountryListBox.Speakers.Count >= DebateSpeakCount) return;

            debateACountryListBox.AddSpeaker(Council.Present[debateASelector.SelectedIndex]);
            debateASelector.Text = "";
        }

        private void debateAddFCountry_Click(object sender, EventArgs e)
        {
            if (debateFSelector.SelectedItem == null) return;
            if (debateFCountryListBox.Speakers.Count >= DebateSpeakCount) return;

            debateFCountryListBox.AddSpeaker(Council.Present[debateFSelector.SelectedIndex]);
            debateFSelector.Text = "";
        }

        private void debateRemoveACountry_Click(object sender, EventArgs e)
        {
            if (debateACountryListBox.SelectedItem == null) return;

            var index = debateACountryListBox.SelectedIndex;

            debateACountryListBox.Items.RemoveAt(index);
            debateACountryListBox.Speakers.RemoveAt(index);
        }

        private void debateRemoveFCountry_Click(object sender, EventArgs e)
        {
            if (debateFCountryListBox.SelectedItem == null) return;

            var index = debateFCountryListBox.SelectedIndex;

            debateFCountryListBox.Items.RemoveAt(index);
            debateFCountryListBox.Speakers.RemoveAt(index);
        }

        private void debateClearACountry_Click(object sender, EventArgs e)
        {
            debateACountryListBox.Items.Clear();
            debateACountryListBox.Speakers.Clear();
        }

        private void debateClearFCountry_Click(object sender, EventArgs e)
        {
            debateFCountryListBox.Items.Clear();
            debateFCountryListBox.Speakers.Clear();
        }

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
                if (debateFCountryListBox.Speakers.Count == 0)
                {
                    IsFor = false;
                    return;
                }

                nextSpeaker = Council.CountriesByShortf[debateFCountryListBox.Speakers[0]];
                debateFCountryListBox.RemoveSpeaker(0);
                debateForTextBox.ForeColor = System.Drawing.Color.Green;
                debateForTextBox.Text = "For";
                IsFor = false;
            }
            else
            {
                if (debateACountryListBox.Speakers.Count == 0)
                {
                    IsFor = true;
                    return;
                }

                nextSpeaker = Council.CountriesByShortf[debateACountryListBox.Speakers[0]];
                debateACountryListBox.RemoveSpeaker(0);
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
