using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace leeyi45.acmun.IndexPicker
{
    public partial class IndexPicker : Form
    {
        private IndexPicker(string[] items, string text, string title)
        {
            InitializeComponent();

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;

            comboBox1.Items.AddRange(items);
            label1.Text = text;
            Text = title;

            StartPosition = FormStartPosition.CenterParent;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Result = -1;
            Close();
        }

        private void Button1_Click(object sender, EventArgs e) => Close();

        private int Result
        {
            get => comboBox1.SelectedIndex;
            set => comboBox1.SelectedIndex = value;
        }

        public static int ShowDialog(string[] items, string text, string title)
        {
            var dialog = new IndexPicker(items, text, title);
            dialog.ShowDialog();
            return dialog.Result;
        }
    }
}
