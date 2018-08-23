using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace leeyi45.acmun.Main_Screen
{
    partial class homescreen
    {
        private void InitializeMod()
        {
            ModTotalTimer = new Clock();
            ModTotalTimer.Tick += ModTotalTick;

            ModSpeakTimer = new Clock();
            ModSpeakTimer.Tick += ModSpeakTick;

            ModSpeakTimer.RunningChanged += ModSpeakTimerRunning_Changed;
            ModTotalTimer.RunningChanged += ModTotalTimerRunning_Changed;

            ModSpeakTimer.TimeUp += ModSpeakTimer_TimeUp;
            ModTotalTimer.TimeUp += ModTotalTimer_TimeUp;

            modSpeakTimeSelector.ValueChanged += modSpeakTimeSelector_ValueChanged;
            modTotalTimeSelector.ValueChanged += modTotalTimeSelector_ValueChanged;

            modSelector.ClickSelect += modSelector_ClickSelect;

            modTotalStartButton.Click += modTotalStartButton_Click;
            modTotalPauseButton.Click += modTotalPauseButton_Click;

            modResetButton.Click += modResetButton_Click;
            modExtendButton.Click += modExtendButton_Click;

            modPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

            LoadMod(ModCaucus.DefaultMod);
        }

        Clock ModSpeakTimer;
        Clock ModTotalTimer;

        TimeSpan ModSpeakTime;
        TimeSpan ModTotalTime;

        Delegation modSpeaker;
        Delegation ModCurrentSpeaker
        {
            get => modSpeaker;
            set
            {
                modSpeaker = value;
                modCountryTextBox.Text = value.Name;
                modPictureBox.ImageLocation = $@"flags\{value.AltName}.png";
            }
        }

        int ModSpeakerCount;
        int ModSpeakerIndex;

        private ModCaucus CurrentMod;

        private void LoadMod(ModCaucus caucus)
        {
            ModTotalTimer.Reset();
            ModSpeakTimer.Reset();

            ModTotalTimer.EditDuration(caucus.Duration);
            ModSpeakTimer.EditDuration(caucus.SpeakTime);

            modTopicTextBox.Text = caucus.Topic;
            modCountryTextBox.Text = "";

            modTotalTimeLabel.Text = $"00:00/{caucus.Duration.ToValString()}";
            modSpeakTimeLabel.Text = $"00:00/{caucus.SpeakTime.ToValString()}";

            modSpeakProgressBar.Value = 0;
            modTotalProgressBar.Value = 0;

            modSpeakProgressBar.Maximum = (int)caucus.SpeakTime.TotalSeconds;
            modTotalProgressBar.Maximum = (int)caucus.Duration.TotalSeconds;

            ModSpeakTime = caucus.SpeakTime;
            ModTotalTime = caucus.Duration;

            ModSpeakerCount = caucus.SpeakerCount;
            ModSpeakerIndex = 0;

            modSpeakTimeSelector.Value = ModSpeakTime;
            modTotalTimeSelector.Value = ModTotalTime;

            modCountryCountTextBox.Text = $"Speaker 0 out of {ModSpeakerCount}";
            modCountryTextBox.Clear();
            modTotalTimeLabel.ForeColor = Color.Black;
            modSelector.ClearSpeakers();

            modTopicTextBox.Click += DisableTextBox;
            modTopicTextBox.TopicChanged += ModTopicTextBox_TopicChanged;

            CurrentMod = caucus;
        }

        private void ModTopicTextBox_TopicChanged(object sender, EventArgs e) 
            => CurrentMod.Topic = modTopicTextBox.Text;

        #region Timer Stuff
        private void ModTotalTick(object sender, EventArgs e)
        {
            modTotalProgressBar.Increment(1);
            modTotalTimeLabel.Text = $"{ModTotalTimer.CurrentTime.ToValString()}/{ModTotalTimer.Duration.ToValString()}";
        }

        private void ModSpeakTick(object sender, EventArgs e)
        {
            modSpeakProgressBar.Increment(1);
            modSpeakTimeLabel.Text = $"{ModSpeakTimer.CurrentTime.ToValString()}/{ModSpeakTimer.Duration.ToValString()}";
        }

        private void ModTotalTimerRunning_Changed(object sender, EventArgs e)
        {
            modTotalStartButton.Enabled = !ModTotalTimer.Running;
            modTotalPauseButton.Enabled = ModTotalTimer.Running;
            modTotalTimeSelector.Enabled = !ModTotalTimer.Running;
            modExtendButton.Enabled = !ModTotalTimer.Running;
        }

        private void ModSpeakTimerRunning_Changed(object sender, EventArgs e)
        {
            modStartButton.Enabled = !ModSpeakTimer.Running;
            modPauseButton.Enabled = ModSpeakTimer.Running;
            modExtendButton.Enabled = !ModSpeakTimer.Running;
            modSpeakTimeSelector.Enabled = !ModSpeakTimer.Running;
        }

        private void ModSpeakTimer_TimeUp(object sender, EventArgs e) 
            => modSpeakTimeLabel.ForeColor = Color.Red;

        private void ModTotalTimer_TimeUp(object sender, EventArgs e)
            => modTotalTimeLabel.ForeColor = Color.Red;

        #endregion

        #region Buttons
        private void modStartButton_Click(object sender, EventArgs e)
        {
            if (ModCurrentSpeaker != null) ModSpeakTimer.Start();
        }

        private void modPauseButton_Click(object sender, EventArgs e)
            => ModSpeakTimer.Stop();

        private void modClearButton_Click(object sender, EventArgs e)
            => modSelector.ClearSpeakers();

        private void modRemoveButton_Click(object sender, EventArgs e)
        {
            if (modSelector.ListBoxSelectedItem != null)
            {
                modSelector.RemoveSpeaker(modSelector.ListBoxSelectedIndex);
                modSelector.ListBoxSelectedIndex = -1;
            }
        }

        private void modTotalStartButton_Click(object sender, EventArgs e)
            => ModTotalTimer.Start();

        private void modTotalPauseButton_Click(object sender, EventArgs e)
            => ModTotalTimer.Stop();

        private void modNextButton_Click(object sender, EventArgs e)
        {
            if (modSelector.Speakers.Count == 0) return;
            ModNextSpeaker(0);
        }

        private void modResetButton_Click(object sender, EventArgs e) => LoadMod(ModCaucus.DefaultMod);

        private void modExtendButton_Click(object sender, EventArgs e)
        {
            ModTotalTimer.Stop();

            var ext = new TimeExt.timeExt(ModTotalTimer.CurrentTime, ModTotalTime, false);
            ext.ShowDialog();

            if (ext.DialogResult == DialogResult.OK)
            {
                ModTotalTime += ext.Result;
                ModTotalTimer.EditDuration(UnmodDuration);
                modTotalTimeLabel.Text = $"{UnmodTimer.CurrentTime.ToValString()}/{UnmodDuration.ToValString()}";
            }
        }
        #endregion

        private void ModNextSpeaker(int index)
        {
            if(ModCurrentSpeaker != null) ModCurrentSpeaker.SpeakingTime.Add(ModSpeakTimer.CurrentTime);
            ModSpeakTimer.Restart();
            ModTotalTimer.Start();

            ModCurrentSpeaker = Council.CountriesByShortf[modSelector.Speakers[index]];
            modSelector.RemoveSpeaker(index);

            ModSpeakerIndex++;
            modSpeakProgressBar.Value = 0;
            modCountryCountTextBox.Text = $"Speaker { ModSpeakerIndex } out of {ModSpeakerCount}";
            modSpeakTimeLabel.ForeColor = Color.Black;
        }

        #region Time ValueUpDowns
        private void modSpeakTimeSelector_ValueChanged(object sender, EventArgs e)
        {
            ModSpeakTime = modSpeakTimeSelector.Value;
            modSpeakProgressBar.Maximum = (int)ModSpeakTime.TotalSeconds;

            modSpeakTimeLabel.Text = $"{ModSpeakTimer.CurrentTime.ToValString()}/{ModSpeakTime.ToValString()}";
            ModSpeakTimer.EditDuration(ModSpeakTime);
        }

        private void modTotalTimeSelector_ValueChanged(object sender, EventArgs e)
        {
            ModTotalTime = modTotalTimeSelector.Value;
            modTotalProgressBar.Maximum = (int)ModTotalTime.TotalSeconds;

            modTotalTimeLabel.Text = $"{ModTotalTimer.CurrentTime.ToValString()}/{ModTotalTime.ToValString()}";
            ModTotalTimer.EditDuration(ModTotalTime);
        }
        #endregion

        private void modSelector_ClickSelect(object sender, int index)
        {
            ModNextSpeaker(index);
        }

        private void modComboBox_ItemSelected(object sender, int index)
            => ModListAdd(index);

        private void ModListAdd(int index)
        {
            if (modSelector.Speakers.Count >= ModSpeakerCount) MessageBox.Show("Exceeded Caucus Speaker Count!", "Exceeded!",
                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            modSelector.AddSpeaker(Council.Present[index]);
        }

    }
}
