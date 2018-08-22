using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace leeyi45.acmun.TimeList
{
    public partial class TimeList : Form
    {
        private TimeList()
        {
            InitializeComponent();

            closeButton.Click += CloseButton_Click;
            StartPosition = FormStartPosition.CenterParent;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            switch (e.ColumnIndex)
            {
                case 1:
                    {
                        if(TimeSpan.TryParse((string)cell.Value, out var t))
                        {
                            dels[e.RowIndex].SpeakingTime = t;   
                        }
                        else
                        {
                            MessageBox.Show("Invalid value for speaking time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    }
                case 2:
                    {
                        if (int.TryParse((string)cell.Value, out var t))
                        {
                            dels[e.RowIndex].SpeechCount = t;
                        }
                        else
                        {
                            MessageBox.Show("Invalid value for speech count", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e) => Close();

        private Delegation[] dels;

        public static void Show(Delegation[] dels, string council)
        {
            var form = new TimeList();

            form.label1.Text = $"Speaking time for delegations in {council}";

            foreach(var each in dels)
            {
                form.dataGridView1.Rows.Add(new object[] { each.Shortf, each.SpeakingTime, each.SpeechCount });
            }

            form.dels = dels;
            form.ShowDialog();
        }
    }
}
