using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;

namespace leeyi45.acmun.Main_Screen
{
    partial class homescreen
    {
        private void InitializeMotions()
        {
            MotionCreator = new MotionCreator.MotionCreator();
            Motions = new List<Motion>();

            motionsDataGrid.SelectionChanged += motionsDataGrid_SelectionChanged;
            motionsDataGrid.CellValueChanged += motionsDataGrid_CellValueChanged;
            motionsDataGrid.CellClick += motionsDataGrid_CellContentClick;

            motionPassDButton.Click += motionPassDButton_Click;
            motionFailDButton.Click += motionFailDButton_Click;
            motionLoadVoteButton.Click += motionLoadVoteButton_Click;

            motionsPassMenuItem.Click += MotionsPassMenuItem_Click;
            motionsPassDMenuItem.Click += MotionsPassDMenuItem_Click;
            motionsFailMenuItem.Click += MotionsFailMenuItem_Click;
            motionsFailDMenuItem.Click += MotionsFailDMenuItem_Click;
            motionsNullMenuItem.Click += MotionsNullMenuItem_Click;
        }

        #region Motion State Drop Down Code
        private void MotionsFailMenuItem_Click(object sender, EventArgs e)
            => UpdateMotionState(acmun.VoteState.Fail);

        private void MotionsPassMenuItem_Click(object sender, EventArgs e) 
            => UpdateMotionState(acmun.VoteState.Pass);

        private void MotionsPassDMenuItem_Click(object sender, EventArgs e)
            => UpdateMotionState(acmun.VoteState.PassD);

        private void MotionsFailDMenuItem_Click(object sender, EventArgs e) 
            => UpdateMotionState(acmun.VoteState.FailD);

        private void MotionsNullMenuItem_Click(object sender, EventArgs e) 
            => UpdateMotionState(acmun.VoteState.Null);

        private void UpdateMotionState(VoteState state)
        {
            SelectedMotion.State = state;
            motionsDataGrid.Rows[_SelectedMotion].Cells[5].Value = state.ToValString();
        }
        #endregion

        private void MotionRow(Motion motion, int RowIndex = -1)
        {
            try
            {
                motionsDataGrid.Rows[RowIndex].SetValues(new object[] { motion.Proposer.Shortf, motion.Name,
                motion.HasDuration ? motion.Duration.ToValString() : "-",
                motion.HasSpeakTime ? motion.SpeakTime.ToValString() : "-",
                motion.HasTopic ? "-" : motion.Topic});

                motionsDataGrid.Rows[RowIndex].Cells[4].ReadOnly = motion.HasTopic;
            }
            catch(ArgumentOutOfRangeException)
            {
                motionsDataGrid.Rows.Add(motion.Proposer.Shortf, motion.Name,
                    motion.HasDuration ? motion.Duration.ToValString() : "-",
                    motion.HasSpeakTime ? motion.SpeakTime.ToValString() : "-",
                    motion.HasTopic ? "-" : motion.Topic
                );
            }
        }

        private void motionsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Motions.Count == 0 || e.RowIndex == -1) return;

            switch(e.ColumnIndex)
            {
                case 0:
                    { //Proposer Column
                        var result = IndexPicker.IndexPicker.ShowDialog(Council.PresentShortf, "Pick the proposer", "Proposer");
                        if (result == -1) return;

                        SelectedMotion.Proposer = Council.Present[result];
                        motionsDataGrid.Rows[_SelectedMotion].Cells[0].Value = SelectedMotion.Proposer.Shortf;
                        break;
                    }
                case 1:
                    { //Motion Column
                        var result = IndexPicker.IndexPicker.ShowDialog(Council.MotionsAsList.Select(x => x.Name).ToArray(), 
                            "Pick the type of motion", "Motion");
                        if (result == -1) return;

                        SelectedMotion.Internal = Council.MotionsAsList[result];
                        MotionRow(SelectedMotion, _SelectedMotion);
                        break;
                    }
                case 2:
                    {
                        //Duration Column
                        if (!SelectedMotion.HasDuration) return;

                        var result = DurationPicker.ShowDialog("New Duration", "Select the new duration", SelectedMotion.Duration);
                        if (result == TimeSpan.Zero) return;

                        SelectedMotion.Duration = result;
                        MotionRow(SelectedMotion, _SelectedMotion);
                        break;
                    }
                case 3:
                    {
                        //Speaking Time Column
                        if (!SelectedMotion.HasSpeakTime) return;
                        var result = DurationPicker.ShowDialog("New speaking time", "Select the new speaking time", SelectedMotion.SpeakTime);
                        if (result == TimeSpan.Zero) return;

                        SelectedMotion.Duration = result;
                        MotionRow(SelectedMotion, _SelectedMotion);
                        break;
                    }
                case 4:
                    {
                        //Topic Column
                        if (!SelectedMotion.HasTopic)
                        {
                            motionsDataGrid.Rows[_SelectedMotion].Cells[4].Value = "-";
                            return;
                        }

                        SelectedMotion.Topic = motionsDataGrid.Rows[_SelectedMotion].Cells[4].Value.ToString();
                        break;
                    }
                case 5:
                    { //Pass fail column

                        foreach (var each in motionsStateMenuStrip.Items)
                            ((ToolStripMenuItem)each).Checked = false;

                        ((ToolStripMenuItem)motionsStateMenuStrip.Items[(int)SelectedMotion.State]).Checked = true;
                        motionsStateMenuStrip.Show(PointToClient(Cursor.Position));
                        break;
                    }
            }
        }

        private MotionCreator.MotionCreator MotionCreator;

        private List<Motion> Motions;

        private int _SelectedMotion = -1;

        public Motion SelectedMotion
        {
            get => Motions[_SelectedMotion];
            set => Motions[_SelectedMotion] = value;
        }

        #region Buttons
        private void motionAddButton_Click(object sender, EventArgs e)
        {
            MotionCreator.ShowDialog();

            if(MotionCreator.DialogResult == DialogResult.Yes)
            {
                var motion = MotionCreator.Result;

                MotionRow(motion);

                Motions.Add(motion);
                _SelectedMotion = Motions.Count - 1;
                motionsDataGrid.Rows[Motions.Count - 1].Selected = true;
                motionNoMotionTextBox.Visible = false;
            }
        }

        private void motionClearButton_Click(object sender, EventArgs e)
        {
            motionsDataGrid.Rows.Clear();
            Motions.Clear();
            _SelectedMotion = -1;
            motionNoMotionTextBox.Visible = true;
        }

        private void motionRemoveButton_Click(object sender, EventArgs e)
        {
            if (motionsDataGrid.Rows.Count == 0) return;
            else
            {
                motionsDataGrid.Rows.RemoveAt(_SelectedMotion);
                Motions.RemoveAt(_SelectedMotion);
                if (motionsDataGrid.Rows.Count == 0) motionNoMotionTextBox.Visible = true;
                _SelectedMotion = -1;
            }
            
        }

        private void motionPassButton_Click(object sender, EventArgs e)
        {
            if (motionsDataGrid.Rows.Count == 0 || _SelectedMotion == -1) return;

            var motion = SelectedMotion;

            if (motion.State != acmun.VoteState.Null) return;

            motion.State = acmun.VoteState.Pass;
            motionsDataGrid.SelectedRows[0].Cells[5].Value = "Pass";

            ExecuteMotion(motion);
        }

        private void motionFailButton_Click(object sender, EventArgs e)
        {
            if (motionsDataGrid.Rows.Count == 0 || _SelectedMotion == -1) return;

            var motion = SelectedMotion;

            if (motion.State != acmun.VoteState.Null) return;

            motion.State = acmun.VoteState.Fail;
            motionsDataGrid.SelectedRows[0].Cells[5].Value = "Fail";
        }

        private void motionSortButton_Click(object sender, EventArgs e)
        {
            if (motionsDataGrid.Rows.Count <= 1) return;

            Motions.Sort();
            motionsDataGrid.Rows.Clear();

            for(int i = 0; i < Motions.Count; i++)
            {
                var motion = Motions[i];

                motionsDataGrid.Rows.Add(motion.Proposer.Shortf, motion.Name,
                    motion.HasDuration ? motion.Duration.ToValString() : "-",
                    motion.HasSpeakTime ? motion.SpeakTime.ToValString() : "-",
                    motion.HasTopic ? "-" : motion.Topic
                    );
            }
        }

        private void motionPassDButton_Click(object sender, EventArgs e)
        {
            if (motionsDataGrid.Rows.Count == 0 || _SelectedMotion == -1) return;

            var motion = SelectedMotion;

            if (motion.State != acmun.VoteState.Null) return;

            motion.State = acmun.VoteState.PassD;
            motionsDataGrid.SelectedRows[0].Cells[5].Value = "Passed by discretion";

            ExecuteMotion(motion);
        }

        private void motionFailDButton_Click(object sender, EventArgs e)
        {
            if (motionsDataGrid.Rows.Count == 0 || _SelectedMotion == -1) return;

            var motion = SelectedMotion;

            if (motion.State != acmun.VoteState.Null) return;

            motion.State = acmun.VoteState.FailD;
            motionsDataGrid.SelectedRows[0].Cells[5].Value = "Failed by discretion";
        }

        private void motionLoadVoteButton_Click(object sender, EventArgs e)
        {
            if (Motions.Count < 1 || SelectedMotion.IsDefault) return;
            LoadVote(SelectedMotion.Internal.VoteData);
            mainScreen.SelectTab(votingTab);
        }
        #endregion

        private void motionsDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            //if (_SelectedMotion < 0) return;
            try
            {
                _SelectedMotion = motionsDataGrid.SelectedRows[0].Index;
                motionNGroupBox.Enabled = SelectedMotion.State == VoteState.Null;
                motionDGroupBox.Enabled = SelectedMotion.State == VoteState.Null;
            }
            catch(ArgumentOutOfRangeException) { _SelectedMotion = -1; }
        }

        private void motionsDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            switch(e.ColumnIndex)
            {
                case 0:
                    {
                        
                        break;
                    }
            }
        }

        private void ExecuteMotion(Motion motion)
        {
            switch (motion.TypeId)
            {
                case "mod":
                    {
                        LoadMod((ModCaucus)motion);
                        mainScreen.SelectTab(modTab);
                        break;
                    }
                case "unmod":
                    {
                        LoadUnmod((UnmodCaucus)motion);
                        mainScreen.SelectTab(unmodTab);
                        break;
                    }
                case "reso_int":
                    {
                        var caucus = new UnmodCaucus("Reading time for resolution", motion.Proposer, Council.ResoReadTime);
                        LoadUnmod(caucus);
                        mainScreen.SelectTab(unmodTab);
                        break;
                    }
            }
        }
    }
}
