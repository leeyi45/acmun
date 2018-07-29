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
        int forCount = 0;
        int againstCount = 0;
        int abstainCount = 0;
        bool veto = false;

        private RadioButton[] voteRadioButtons;

        private Vote CurrentVote;

        private void InitializeVote()
        {
            voteRadioButtons = new[] { voteRadioButton1, voteRadioButton2, voteRadioButton3 };

            voteForButton.Click += voteForButton_Click;
            voteAgainstButton.Click += voteAgainstButton_Click;
            voteAbstainButton.Click += voteAbstainButton_Click;

            voteForRemoveButton.Click += VoteForRemoveButton_Click;
            voteAgainstRemoveButton.Click += VoteAgainstRemoveButton_Click;
            voteAbstainRemoveButton.Click += VoteAbstainRemoveButton_Click;

            voteCountButton.Click += VoteCountButton_Click;
            voteClearAllButton.Click += VoteClearAllButton_Click;
            voteDivideButton.Click += VoteDivideButton_Click;
            voteResultButton.Click += VoteResultButton_Click;

            voteAbstainCheckBox.CheckedChanged += voteAbstainCheckBox_CheckedChanged;
            voteWithRightsCheckBox.CheckedChanged += VoteWithRightsCheckBox_CheckedChanged;
            voteAutoCountCheckBox.CheckedChanged += VoteAutoCountCheckBox_CheckedChanged;

            LoadVote(Vote.Default);

            voteRadioButton1.CheckedChanged += VoteRadioButton_CheckedChanged;
            voteRadioButton2.CheckedChanged += VoteRadioButton_CheckedChanged;
            voteRadioButton3.CheckedChanged += VoteRadioButton_CheckedChanged;
        }

        private void VoteAutoCountCheckBox_CheckedChanged(object sender, EventArgs e) 
            => voteCountButton.Enabled = !voteAutoCountCheckBox.Checked;

        private void VoteWithRightsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            voteForRightsButton.Enabled = voteWithRightsCheckBox.Checked;
            voteAgainstRightsButton.Enabled = voteWithRightsCheckBox.Checked;
        }

        private void VoteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0;
            for (; i < 2; i++)
            {
                if (voteRadioButtons[i].Checked) break;
            }

            CurrentVote.Type = (Vote.VoteType)i;
        }

        private void LoadVote(Vote vote)
        {
            forCount = 0;
            againstCount = 0;
            abstainCount = 0;

            voteTopicBox.Text = vote.Topic;
            voteVetoCheckBox.Checked = vote.P5Veto;
            voteAbstainCheckBox.Checked = vote.AllowAbstentions;
            voteRadioButtons[(int)vote.Type].Checked = true;

            voteForListBox.Items.Clear();
            voteAgainstListBox.Items.Clear();
            voteAbstainListBox.Items.Clear();
            voteWithRightsCheckBox.Checked = false;
            UpdateVoteCount();

            CurrentVote = vote;
        }

        private void VoteDivideButton_Click(object sender, EventArgs e)
        {

        }

        private void VoteClearAllButton_Click(object sender, EventArgs e)
        {
            voteForListBox.Items.Clear();
            forCount = 0;

            voteAgainstListBox.Items.Clear();
            againstCount = 0;

            voteAbstainListBox.Items.Clear();
            abstainCount = 0;

            UpdateVoteCount();
        }

        private void VoteCountButton_Click(object sender, EventArgs e)
            => UpdateVoteCount();

        private void VoteAbstainRemoveButton_Click(object sender, EventArgs e)
        {
            if (voteAbstainListBox.SelectedItem == null) return;
            var country = (string)voteAbstainListBox.SelectedItem;
            voteAbstainListBox.Items.RemoveAt(voteAbstainListBox.SelectedIndex);
            abstainCount--;
            VoteRemoveButtons(country);
        }

        private void VoteAgainstRemoveButton_Click(object sender, EventArgs e)
        {
            if (voteAgainstListBox.SelectedItem == null) return;
            var country = (string)voteAgainstListBox.SelectedItem;
            voteAgainstListBox.Items.RemoveAt(voteAgainstListBox.SelectedIndex);
            againstCount--;
            VoteRemoveButtons(country);
        }

        private void VoteForRemoveButton_Click(object sender, EventArgs e)
        {
            if (voteForListBox.SelectedItem == null) return;
            var country = (string)voteForListBox.SelectedItem;
            voteForListBox.Items.RemoveAt(voteForListBox.SelectedIndex);
            forCount--;
            VoteRemoveButtons(country);
        }

        private void voteAbstainButton_Click(object sender, EventArgs e)
        {
            if (voteCountryBox.SelectedItem == null) return;
            voteAbstainListBox.Items.Add(voteCountryBox.SelectedItem);
            abstainCount++;
            VoteButtons();
        }

        private void voteAgainstButton_Click(object sender, EventArgs e)
        {
            if (voteCountryBox.SelectedItem == null) return;
            voteAgainstListBox.Items.Add(voteCountryBox.SelectedItem);
            againstCount++;

            if (Council.Voters[voteCountryBox.SelectedIndex].P5Veto) veto = true;

            VoteButtons();
        }

        private void voteForButton_Click(object sender, EventArgs e)
        {
            if (voteCountryBox.SelectedItem == null) return;
            voteForListBox.Items.Add(voteCountryBox.SelectedItem);
            forCount++;
            VoteButtons();
        }

        private void VoteResultButton_Click(object sender, EventArgs e)
        {
            switch (CurrentVote.Type)
            {
                case Vote.VoteType.Consensus:
                    {
                        if (againstCount > 0) VoteResult("A consensus was not reached!", false);
                        break;
                    }
                case Vote.VoteType.Procedural:
                    {
                        if (forCount < CurrentVote.ToPass) VoteResult("A simple majority was not reached!", false;);
                        break;
                    }
                case Vote.VoteType.Substantive:
                    {
                        if (forCount < CurrentVote.ToPass) VoteResult("A two thirds majority was not reached!", false);
                        else if (veto) VoteResult("A veto has been issued!", false);
                        break;
                    }
            }

            VoteResult("The vote has been passed", true);
        }

        private void VoteResult(string Message, bool pass)
        {
            CurrentVote.Passed = pass;
        }

        private void VoteButtons()
        {
            voteCountryBox.Items.Remove(voteCountryBox.SelectedItem);
            ButtonUpdate();
        }

        private void VoteRemoveButtons(string country)
        {
            voteCountryBox.Items.Add(country);
            ButtonUpdate();
        }

        private void ButtonUpdate()
        {
            if (voteAutoCountCheckBox.Checked) UpdateVoteCount();
        }

        private void UpdateVoteCount()
        {
            voteForCountLabel.Text = $"Voted For: {forCount}";
            voteAgainstCountLabel.Text = $"Voted Against: {againstCount}";
            voteAbstainCountLabel.Text = $"Voted to Abstain: {abstainCount}";
            voteTotalCountLabel.Text = $"Total Votes: {forCount + againstCount + abstainCount}";
        }

        private void voteAbstainCheckBox_CheckedChanged(object sender, EventArgs e)
            => voteAbstainButton.Enabled = voteAbstainCheckBox.Checked;
    }
}
