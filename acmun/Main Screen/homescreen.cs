﻿using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static leeyi45.acmun.Council;

namespace leeyi45.acmun.Main_Screen
{
    public partial class homescreen : Form
    {
        public homescreen()
        {
            InitializeComponent();

            //new Roll_Call.rollCallScreen(Council.Name).ShowDialog();

            InitializeGSL();
            InitializeUnmod();
            InitializeMod();
            InitializeSingle();
            InitializeMotions();
            InitializeDebate();
            InitializeVote();

            acmun.Controls.LabelBox.ParentForm = this;
            acmun.Controls.TopicBox.ParentForm = this;

            councilLabel.TopicChanged += CouncilLabel_TopicChanged;
            quorumLabel.Click += QuorumLabel_Click;
        }

        private void QuorumLabel_Click(object sender, EventArgs e) 
            => new Roll_Call.rollCallScreen(Council.Name).ShowDialog();

        private void CouncilLabel_TopicChanged(object sender, EventArgs e)
            => Council.Name = councilLabel.Text;

        private void homescreen_Shown(object sender, EventArgs e)
            => new Roll_Call.rollCallScreen(Council.Name).ShowDialog();

        public void UpdateQuorum()
        {
            if (InvokeRequired) Invoke(new Action(UpdateQuorum));
            else
            {
                var present = VoteCount;
                var obsCount = PresentCount - VoteCount;

                Maj50 = (int)Math.Max(1, Math.Floor(present / 2.0 + (FiftyPlus1 ? 1 : 0)));
                Maj67 = (int)Math.Max(1, Math.Floor(present / 3 * 2.0 + (TwoThirdPlus1 ? 1 : 0)));

                quorumLabel.Text = $"Simple Majority (50%) at {Maj50}, Super Majority (67%) at {Maj67}"+
                $", Total Present: {PresentCount}{(obsCount == 0 ? "" : $", Observer States: {PresentCount - VoteCount}")}";

                var shortf = Present.Select(x => x.Shortf).ToArray();

                var boxes = new Controls.CountrySelector[] { gslSelector, modSelector, debateASelector, debateFSelector};

                foreach(var each in boxes) each.ComboBoxResetItems(shortf);

                singleListBox.Items.Clear();
                singleListBox.Items.AddRange(shortf);

                voteCountryBox.Items.Clear();
                voteCountryBox.Items.AddRange(Present.Where(x => !x.Observer).Select(x => x.Shortf).ToArray());

                councilLabel.Text = Council.Name;
                topicLabel.Text = $"Issue: {Topics[CurrentTopic]}";
            }
        }

        public void LoadState(CouncilState state)
        {
            gslSelector.Speakers = state.GSLList.ToList();
            modSelector.Speakers = state.ModList.ToList();
            CurrentMod = state.CurrentMod;
            CurrentUnmod = state.CurrentUnmod;
        }

        #region ToolStripMenuItems
        private void stateXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xmlFilePicker.ShowDialog() == DialogResult.Cancel) return;
            else
            {
                CouncilState.LoadState(xmlFilePicker.FileName);
                MessageBox.Show("Loaded the file", "File Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void speakingTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeList.TimeList.Show(DelList, Council.Name);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void rollCallToolStripMenuItem_Click(object sender, EventArgs e)
            => new Roll_Call.rollCallScreen(Council.Name).ShowDialog();

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Program.GenerateDefaultXML();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Vote_Settings.VoteSet().ShowDialog();
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox.AboutBox().ShowDialog();
        }

        private void settingsXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xmlFilePicker.ShowDialog() == DialogResult.Cancel) return;
            else
            {
                LoadCouncil(xmlFilePicker.FileName);
                MessageBox.Show("Loaded the file", "File Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void saveStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CouncilState.SaveState(gslSelector.Speakers, modSelector.Speakers, CurrentMod, CurrentUnmod);
        }
        #endregion
    }
}
