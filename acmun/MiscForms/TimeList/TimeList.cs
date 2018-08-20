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
        public TimeList()
        {
            InitializeComponent();

            closeButton.Click += CloseButton_Click;
        }

        private void CloseButton_Click(object sender, EventArgs e) => Close();

        public static void Show(Delegation[] dels)
        {
            var form = new TimeList();

            foreach(var each in dels)
            {
                form.dataGridView1.Rows.Add(new object[] { each.Shortf, each.SpeakingTime, each.SpeechCount });
            }

            form.ShowDialog();
        }
    }
}
