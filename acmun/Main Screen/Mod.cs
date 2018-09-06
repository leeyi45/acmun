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
            ModSpeakTimeBar.RunningChanged += ModSpeakTimerRunning_Changed;
            ModTotalTimeBar.RunningChanged += ModTotalTimerRunning_Changed;

            modSpeakTimeSelector.ValueChanged += modSpeakTimeSelector_ValueChanged;
            modTotalTimeSelector.ValueChanged += modTotalTimeSelector_ValueChanged;

            modSelector.ClickSelect += modSelector_ClickSelect;

            modTotalStartButton.Click += modTotalStartButton_Click;
            modTotalPauseButton.Click += modTotalPauseButton_Click;

            modStartButton.Click += modStartButton_Click;
            modPauseButton.Click += modPauseButton_Click;

            modResetButton.Click += modResetButton_Click;
            modExtendButton.Click += modExtendButton_Click;

            modPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

            LoadMod(ModCaucus.DefaultMod);
        }

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
            ModTotalTimeBar.Reset();
            ModSpeakTimeBar.Reset();

            ModTotalTimeBar.Duration = caucus.Duration;
            ModSpeakTimeBar.Duration = caucus.SpeakTime;

            modTopicTextBox.Text = caucus.Topic;
            modCountryTextBox.Text = "";

            ModSpeakTime = caucus.SpeakTime;
            ModTotalTime = caucus.Duration;

            ModSpeakerCount = caucus.SpeakerCount;
            ModSpeakerIndex = 0;

            modSpeakTimeSelector.Value = ModSpeakTime;
            modTotalTimeSelector.Value = ModTotalTime;

            modCountryCountTextBox.Text = $"Speaker 0 out of {ModSpeakerCount}";
            modCountryTextBox.Clear();
            modSelector.ClearSpeakers();

            modTopicTextBox.TopicChanged += ModTopicTextBox_TopicChanged;

            CurrentMod = caucus;
        }

        #region Timer Stuff
        private void ModTotalTimerRunning_Changed(object sender, EventArgs e)
        {
            modTotalStartButton.Enabled = !ModTotalTimeBar.Running;
            modTotalPauseButton.Enabled = ModTotalTimeBar.Running;
            modTotalTimeSelector.Enabled = !ModTotalTimeBar.Running;
            modExtendButton.Enabled = !ModTotalTimeBar.Running;
        }

        private void ModSpeakTimerRunning_Changed(object sender, EventArgs e)
        {
            modStartButton.Enabled = !ModSpeakTimeBar.Running;
            modPauseButton.Enabled = ModSpeakTimeBar.Running;
            modExtendButton.Enabled = !ModSpeakTimeBar.Running;
            modSpeakTimeSelector.Enabled = !ModSpeakTimeBar.Running;
        }
        #endregion

        #region Buttons
        private void modStartButton_Click(object sender, EventArgs e)
        {
            if (ModCurrentSpeaker != null) ModSpeakTimeBar.Start();
        }

        private void modPauseButton_Click(object sender, EventArgs e)
            => ModSpeakTimeBar.Stop();

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
            => ModTotalTimeBar.Start();

        private void modTotalPauseButton_Click(object sender, EventArgs e)
            => ModTotalTimeBar.Stop();

        private void modNextButton_Click(object sender, EventArgs e)
        {
            if (modSelector.Speakers.Count == 0) return;
            ModNextSpeaker(0);
        }

        private void modResetButton_Click(object sender, EventArgs e) => LoadMod(ModCaucus.DefaultMod);

        private void modExtendButton_Click(object sender, EventArgs e)
        {
            ModTotalTimeBar.Stop();

            var ext = new TimeExt.timeExt(ModTotalTimeBar.ElapsedTime, ModTotalTime, false);
            ext.ShowDialog();

            if (ext.DialogResult == DialogResult.OK)
            {
                ModTotalTime += ext.Result;
                ModTotalTimeBar.Duration = ModTotalTime;
            }
        }
        #endregion

        private void ModNextSpeaker(int index)
        {
            if(ModCurrentSpeaker != null) ModCurrentSpeaker.SpeakingTime.Add(ModSpeakTimeBar.ElapsedTime);
            ModSpeakTimeBar.Restart();
            ModTotalTimeBar.Start();

            ModCurrentSpeaker = Council.DelsByShortf[modSelector.Speakers[index]];
            modSelector.RemoveSpeaker(index);

            ModSpeakerIndex++;
            modCountryCountTextBox.Text = $"Speaker { ModSpeakerIndex } out of { ModSpeakerCount }";
        }

        #region Time ValueUpDowns
        private void modSpeakTimeSelector_ValueChanged(object sender, EventArgs e)
        {
            ModSpeakTime = modSpeakTimeSelector.Value;
            ModSpeakTimeBar.Duration = ModSpeakTime;
        }

        private void modTotalTimeSelector_ValueChanged(object sender, EventArgs e)
        {
            ModTotalTime = modTotalTimeSelector.Value;
            ModTotalTimeBar.Duration = ModTotalTime;
        }
        #endregion

        private void modSelector_ClickSelect(object sender, int index)
        {
            ModNextSpeaker(index);
        }

        private void modComboBox_ItemSelected(object sender, int index)
            => ModListAdd(index);

        private void ModTopicTextBox_TopicChanged(object sender, EventArgs e)
            => CurrentMod.Topic = modTopicTextBox.Text;

        private void ModListAdd(int index)
        {
            if (modSelector.Speakers.Count >= ModSpeakerCount) MessageBox.Show("Exceeded Caucus Speaker Count!", "Exceeded!",
                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            modSelector.AddSpeaker(Council.Present[index]);
        }

    }
}
