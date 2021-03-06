﻿using System;
using System.Windows.Forms;
using System.Drawing;

namespace leeyi45.acmun.Main_Screen
{
    partial class homescreen
    {
        private void InitializeUnmod()
        {
            UnmodTimeBar.RunningChanged += UnmodTimerRunningChanged;

            unmodStartButton.Click += unmodStartButton_Click;
            unmodPauseButton.Click += unmodPauseButton_Click;
            unmodFinishButton.Click += unmodFinishButton_Click;
            unmodCountryTextBox.Click += UnmodCountryTextBox_Click;

            unmodTopicTextBox.TopicChanged += UnmodTopicTextBox_TopicChanged;

            LoadUnmod(UnmodCaucus.DefaultUnmod);
        }

        private void UnmodCountryTextBox_Click(object sender, EventArgs e)
        {
            var index = IndexPicker.IndexPicker.ShowDialog(Council.PresentShortf, "Select new proposer", "Proposer");
            if(index != -1)
            {
                CurrentUnmod.Proposer = Council.Present[index];
                unmodCountryTextBox.Text = CurrentUnmod.Proposer.Shortf;
            }
        }

        private void UnmodTopicTextBox_TopicChanged(object sender, EventArgs e)
            => CurrentUnmod.Topic = unmodTopicTextBox.Text;

        private TimeSpan UnmodDuration;

        private Delegation UnmodProposer;

        private UnmodCaucus CurrentUnmod;

        private void LoadUnmod(UnmodCaucus caucus)
        {
            UnmodProposer = caucus.Proposer;

            unmodTopicTextBox.Text = caucus.Topic;
            unmodCountryTextBox.Text = caucus.Proposer?.Shortf ?? "";

            UnmodTimeBar.Duration = caucus.Duration;
            UnmodDuration = caucus.Duration;

            CurrentUnmod = caucus;
        }

        private void UnmodTimerRunningChanged(object sender, EventArgs e)
        {
            unmodStartButton.Enabled = !UnmodTimeBar.Running;
            unmodPauseButton.Enabled = UnmodTimeBar.Running;
        }

        private void unmodStartButton_Click(object sender, EventArgs e)
            => UnmodTimeBar.Start();

        private void unmodExtendButton_Click(object sender, EventArgs e)
        {
            UnmodTimeBar.Stop();

            var ext = new TimeExt.timeExt(UnmodTimeBar.ElapsedTime, UnmodDuration, false);
            ext.ShowDialog();

            if(ext.DialogResult == DialogResult.OK)
            {
                UnmodDuration += ext.Result;
                UnmodTimeBar.Duration = UnmodDuration;
            }
        }

        private void unmodPauseButton_Click(object sender, EventArgs e) => UnmodTimeBar.Stop();

        private void unmodReset_Click(object sender, EventArgs e)
        {
            unmodTopicTextBox.Text = "Enter topic here";
            unmodCountryTextBox.Text = "Enter country here";
            UnmodTimeBar.Reset();
        }

        private void unmodFinishButton_Click(object sender, EventArgs e)
        {
            LoadSingleSpeak(UnmodProposer, Council.UnmodSummaryTime);
            mainScreen.SelectTab(singleTab);
        }
    }
}
