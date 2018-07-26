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
            DebateTimer.Started += DebateTimerStarted;
            DebateTimer.Stopped += DebateTimerStopped;
            DebateTimer.Tick += DebateTimerTick;

            debateTimeSelector.ValueChanged += debateTimeSelector_ValueChanged;
            debateCountSelector.ValueChanged += debateCountSelector_ValueChanged;

            DebateASpeakers = new List<Country>(DebateSpeakCount);
            DebateFSpeakers = new List<Country>(DebateSpeakCount);

            debateAddACountry.Click += debateAddACountry_Click;
            debateAddFCountry.Click += debateAddFCountry_Click;

            debateARemoveButton.Click += debateRemoveACountry_Click;
            debateFRemoveButton.Click += debateRemoveFCountry_Click;

            debateAClearButton.Click += debateClearACountry_Click;
            debateFClearButton.Click += debateClearFCountry_Click;

            debateNextButton.Click += debateNextButton_Click;
            debateStartButton.Click += debateStartButton_Click;
            debateStopButton.Click += debatePauseButton_Click;

            debateACountryListBox.DragDone += debateAListBox_DragDrop;
            debateFCountryListBox.DragDone += debateFListBox_DragDrop;

            debatePictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

            LoadDebate();
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

        private void DebateTimerStarted(object sender, EventArgs e)
        {
            debateTimeSelector.Enabled = false;

            debateStopButton.Enabled = true;
            debateStopButton.Text = "Pause";

            debateStartButton.Enabled = false;
            debateStartButton.Text = "Started";
        }

        private void DebateTimerStopped(object sender, EventArgs e)
        {
            debateTimeSelector.Enabled = true;

            debateStopButton.Enabled = false;
            debateStopButton.Text = "Paused";

            debateStartButton.Enabled = true;
            debateStartButton.Text = "Start";
        }

        private void DebateTimerTick(object sender, EventArgs e)
        {
            debateProgressBar.Increment(1);
            debateTimeLabel.Text = $"{DebateTimer.CurrentTime.ToValString()}/{DebateSpeakTime.ToValString()}";
        }

        private int DebateSpeakCount;

        private List<Country> DebateASpeakers;
        private List<Country> DebateFSpeakers;

        private Clock DebateTimer;
        private TimeSpan DebateSpeakTime;

        private Country debateSpeaker;
        private Country DebateSpeaker
        {
            get => debateSpeaker;
            set
            {
                debateSpeaker = value;
                debateCountryLabel.Text = value.Name;
                debatePictureBox.ImageLocation = $@"flags\{value.Shortf}.png";
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
                DebateASpeakers.RemoveRange(diff - 1, diff);
                DebateFSpeakers.RemoveRange(diff - 1, diff);

                debateACountryListBox.Items.Clear();
                debateACountryListBox.Items.AddRange(DebateASpeakers.Select(x => x.Name).ToArray());

                debateFCountryListBox.Items.Clear();
                debateFCountryListBox.Items.AddRange(DebateFSpeakers.Select(x => x.Name).ToArray());
            }

            DebateSpeakCount = newval;
        }

        #region Buttons
        private void debateAddACountry_Click(object sender, EventArgs e)
        {
            if (debateASelector.SelectedItem == null) return;
            if (DebateASpeakers.Count >= DebateSpeakCount) return;

            var country = Council.Present[debateASelector.SelectedIndex];

            debateACountryListBox.Items.Add(country.Name);
            DebateASpeakers.Add(country);
            debateASelector.Text = "";
        }

        private void debateAddFCountry_Click(object sender, EventArgs e)
        {
            if (debateFSelector.SelectedItem == null) return;
            if (DebateFSpeakers.Count >= DebateSpeakCount) return;

            var country = Council.Present[debateFSelector.SelectedIndex];

            debateFCountryListBox.Items.Add(country.Name);
            DebateFSpeakers.Add(country);
            debateFSelector.Text = "";
        }

        private void debateRemoveACountry_Click(object sender, EventArgs e)
        {
            if (debateACountryListBox.SelectedItem == null) return;

            var index = debateACountryListBox.SelectedIndex;

            debateACountryListBox.Items.RemoveAt(index);
            DebateASpeakers.RemoveAt(index);
        }

        private void debateRemoveFCountry_Click(object sender, EventArgs e)
        {
            if (debateFCountryListBox.SelectedItem == null) return;

            var index = debateFCountryListBox.SelectedIndex;

            debateFCountryListBox.Items.RemoveAt(index);
            DebateFSpeakers.RemoveAt(index);
        }

        private void debateClearACountry_Click(object sender, EventArgs e)
        {
            debateACountryListBox.Items.Clear();
            DebateASpeakers.Clear();
        }

        private void debateClearFCountry_Click(object sender, EventArgs e)
        {
            debateFCountryListBox.Items.Clear();
            DebateFSpeakers.Clear();
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

        private void debateAListBox_DragDrop(object sender, int oldIndex, int newIndex)
        {
            var data = DebateASpeakers[oldIndex];
            DebateASpeakers.RemoveAt(oldIndex);
            DebateASpeakers.Insert(newIndex, data);
        }

        private void debateFListBox_DragDrop(object sender, int oldIndex, int newIndex)
        {
            var data = DebateFSpeakers[oldIndex];
            DebateFSpeakers.RemoveAt(oldIndex);
            DebateFSpeakers.Insert(newIndex, data);
        }

        private void DebateNextSpeaker()
        {
            DebateTimer.Reset();
            debateTimeLabel.Text = $"00:00/{DebateSpeakTime.ToValString()}";
            debateProgressBar.Value = 0;

            Country nextSpeaker;

            if (IsFor)
            {
                if (DebateFSpeakers.Count == 0)
                {
                    IsFor = false;
                    return;
                }

                nextSpeaker = DebateFSpeakers[0];
                DebateFSpeakers.RemoveAt(0);
                debateFCountryListBox.Items.RemoveAt(0);
                debateForTextBox.ForeColor = System.Drawing.Color.Green;
                debateForTextBox.Text = "For";
                IsFor = false;
            }
            else
            {
                if (DebateASpeakers.Count == 0)
                {
                    IsFor = true;
                    return;
                }

                nextSpeaker = DebateASpeakers[0];
                DebateASpeakers.RemoveAt(0);
                debateACountryListBox.Items.RemoveAt(0);
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
