using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace leeyi45.acmun
{
    public partial class DurationPicker : Form
    {
        public DurationPicker(string label, string title, TimeSpan PrevDuration)
        {
            InitializeComponent();

            Text = title;
            timeSelector1.Text = label;
            timeSelector1.Value = PrevDuration;

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;

            StartPosition = FormStartPosition.CenterParent;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Result = timeSelector1.Value;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Result = TimeSpan.Zero;
            Close();
        }

        private TimeSpan Result;

        public static TimeSpan ShowDialog(string title, string label, TimeSpan Prev)
        {
            var thing = new DurationPicker(label, title, Prev);
            thing.ShowDialog();
            return thing.Result;
        }

    }
}
