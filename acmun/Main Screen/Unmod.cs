using System;
using System.Windows.Forms;

namespace leeyi45.acmun.Main_Screen
{
    partial class homescreen
    {
        private void InitializeUnmod()
        {
            unmodTab.Click += unmodTab_Click;

            unmodTopicTextBox.KeyDown += unmodTextBox_KeyDown;
            unmodCountryTextBox.KeyDown += unmodTextBox_KeyDown;

            UnmodTimer = new Clock();
            UnmodTimer.Tick += UnmodTimerTick;
            UnmodTimer.RunningChanged += UnmodTimerRunningChanged;

            unmodStartButton.Click += unmodStartButton_Click;
            unmodPauseButton.Click += unmodPauseButton_Click;
            unmodFinishButton.Click += unmodFinishButton_Click;

            unmodTopicTextBox.TopicChanged += UnmodTopicTextBox_TopicChanged;
            unmodTopicTextBox.Enter += DisableTextBox;

            LoadUnmod(UnmodCaucus.DefaultUnmod);
        }

        private void UnmodTopicTextBox_TopicChanged(object sender, EventArgs e)
        {
            CurrentUnmod.Topic = unmodTopicTextBox.Text;
        }

        private Clock UnmodTimer;

        private TimeSpan UnmodDuration;

        private Delegation UnmodProposer;

        private UnmodCaucus CurrentUnmod;

        private void LoadUnmod(UnmodCaucus caucus)
        {
            UnmodProposer = caucus.Proposer;

            unmodTopicTextBox.Text = caucus.Topic;
            unmodCountryTextBox.Text = caucus.Proposer?.Shortf ?? "";

            UnmodTimer.EditDuration(caucus.Duration);
            UnmodDuration = caucus.Duration;

            unmodProgressBar.Maximum = (int)UnmodDuration.TotalSeconds;
            unmodProgressBar.Value = 0;

            unmodTimeLabel.Text = $"00:00/{UnmodDuration.ToValString()}";

            CurrentUnmod = caucus;
        }

        private void UnmodTimerTick(object sender, EventArgs e)
        {
            unmodProgressBar.Increment(1);
            unmodTimeLabel.Text = $"{UnmodTimer.CurrentTime.ToValString()}/{UnmodDuration.ToValString()}";
        }

        private void UnmodTimerRunningChanged(object sender, EventArgs e)
        {
            unmodStartButton.Enabled = !UnmodTimer.Running;
            unmodPauseButton.Enabled = UnmodTimer.Running;
        }

        private void unmodTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var box = sender as TextBox;
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Escape)
            {
                ActiveControl = null;
                e.Handled = true;
            }
        }    

        private void unmodStartButton_Click(object sender, EventArgs e)
            => UnmodTimer.Start();

        private void unmodExtendButton_Click(object sender, EventArgs e)
        {
            UnmodTimer.Stop();

            var ext = new TimeExt.timeExt(UnmodTimer.CurrentTime, UnmodDuration, false);
            ext.ShowDialog();

            if(ext.DialogResult == DialogResult.OK)
            {
                UnmodDuration += ext.Result;
                UnmodTimer.EditDuration(UnmodDuration);
                unmodTimeLabel.Text = $"{UnmodTimer.CurrentTime.ToValString()}/{UnmodDuration.ToValString()}";
            }
        }

        private void unmodPauseButton_Click(object sender, EventArgs e) => UnmodTimer.Stop();

        private void unmodReset_Click(object sender, EventArgs e)
        {
            unmodTopicTextBox.Text = "Enter topic here";
            unmodCountryTextBox.Text = "Enter country here";
            UnmodTimer.Reset();
            unmodProgressBar.Value = 0;
        }

        private void unmodFinishButton_Click(object sender, EventArgs e)
        {
            LoadSingleSpeak(UnmodProposer, Council.UnmodSummaryTime);
            motionsTab.SelectTab(singleTab);
        }

        private void unmodTab_Click(object sender, EventArgs e)
            => ActiveControl = unmodTab;
    }
}
