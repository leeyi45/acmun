using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace leeyi45.acmun.Controls
{
    public partial class ComboButton : UserControl
    {
        public ComboButton()
        {
            InitializeComponent();

            button1.Click += Button1_Click;

            var size = TextRenderer.MeasureText("test", comboBox1.Font);
            MinimumSize = new Size(comboBox1.MinimumSize.Width + 34, size.Height + 10);
            //MaximumSize = new Size(comboBox1.MaximumSize.Width + 34, size.Height + 10);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AddButtonClick?.Invoke(this, EventArgs.Empty);
        }

        public new Font Font
        {
            get => comboBox1.Font;
            set
            {
                comboBox1.Font = value;
                var size = TextRenderer.MeasureText("test", value);
                Height = size.Height + 10;
            }
        }

        public int ItemHeight
        {
            get => comboBox1.ItemHeight;
            set => comboBox1.ItemHeight = value;
        }

        public ComboBox.ObjectCollection Items => comboBox1.Items;

        public int SelectedIndex
        {
            get => comboBox1.SelectedIndex;
            set => comboBox1.SelectedIndex = value;
        }

        public object SelectedItem
        {
            get => comboBox1.SelectedItem;
            set => comboBox1.SelectedItem = value;
        }

        public void ResetItems(string[] items) => comboBox1.ResetItems(items);

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {            
            base.SetBoundsCore(x, y, width, TextRenderer.MeasureText("test", Font).Height + 10, specified);
        }

        public event EventHandler AddButtonClick;
    }
}
