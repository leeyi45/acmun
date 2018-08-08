using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

            modSpeakTimeSelector.ValueChanged += modSpeakTimeSelector_ValueChanged;
            modTotalTimeSelector.ValueChanged += modTotalTimeSelector_ValueChanged;

            modListBox.DragDone += modListBox_DragDone;
            modListBox.ClickSelect += modListBox_ClickSelect;

            modComboBox.ItemSelected += modComboBox_ItemSelected;

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

        Country modSpeaker;
        Country ModCurrentSpeaker
        {
            get => modSpeaker;
            set
            {
                modSpeaker = value;
                modCountryTextBox.Text = value.Name;
                modPictureBox.ImageLocation = $@"flags\{value.AltName}.png";
            }
        }

        List<Country> ModList;

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
            modListBox.Items.Clear();

            ModList = new List<Country>(caucus.SpeakerCount);

            CurrentMod = caucus;
        }

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

        #endregion

        #region Buttons
        private void modStartButton_Click(object sender, EventArgs e)
        {
            if (ModCurrentSpeaker != null) ModSpeakTimer.Start();
        }

        private void modPauseButton_Click(object sender, EventArgs e)
            => ModSpeakTimer.Stop();

        private void modClearButton_Click(object sender, EventArgs e)
            => modListBox.Items.Clear();

        private void modRemoveButton_Click(object sender, EventArgs e)
        {
            if (modListBox.SelectedItem != null)
            {
                ModList.RemoveAt(modListBox.SelectedIndex);
                modListBox.Items.RemoveAt(modListBox.SelectedIndex);
                modListBox.Deselect();
            }
        }

        private void modTotalStartButton_Click(object sender, EventArgs e)
            => ModTotalTimer.Start();

        private void modTotalPauseButton_Click(object sender, EventArgs e)
            => ModTotalTimer.Stop();

        private void modNextButton_Click(object sender, EventArgs e)
        {
            if (modListBox.Items.Count == 0) return;
            ModNextSpeaker(0);
        }

        private void modAddButton_Click(object sender, EventArgs e)
        {
            if (modComboBox.SelectedItem == null) return;
            ModListAdd(modComboBox.SelectedIndex);
            modComboBox.Text = "";
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

            ModCurrentSpeaker = ModList[index];
            ModList.RemoveAt(index);

            modListBox.Items.RemoveAt(index);

            ModSpeakerIndex++;
            modSpeakProgressBar.Value = 0;
            modCountryCountTextBox.Text = $"Speaker { ModSpeakerIndex } out of {ModSpeakerCount}";
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

        private void modListBox_DragDone(object sender, int oldIndex, int newIndex)
        {
            var data = ModList[oldIndex];
            ModList.RemoveAt(oldIndex);
            ModList.Insert(newIndex, data);
        }

        private void modListBox_ClickSelect(object sender, int index)
        {
            ModNextSpeaker(index);
        }

        private void modComboBox_ItemSelected(object sender, int index)
            => ModListAdd(index);

        private void ModListAdd(int index)
        {
            if (ModList.Count >= ModSpeakerCount) MessageBox.Show("Exceeded Caucus Speaker Count!", "Exceeded!",
                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            var country = Council.Present[index];
            ModList.Insert(ModList.Count, country);
            modListBox.Items.Add(country.Name);
        }

    }
}
