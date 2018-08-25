using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace leeyi45.acmun.Controls
{
    class ComboBox : System.Windows.Forms.ComboBox
    {
        public ComboBox()
        {
            MouseClick += ClickHandler;
            KeyDown += KeyDownHandler;
        }

        private void ClickHandler(object sender, MouseEventArgs e)
        {
            if (!DroppedDown) DroppedDown = true;
        }

        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (SelectedItem != null)
                {
                    ItemSelected?.Invoke(this, SelectedIndex);
                    SelectedIndex = -1;
                }
                Text = "";

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                SelectedIndex = -1;
                Text = "";
                DroppedDown = false;
                e.Handled = true;
            }
        }

        public void ResetItems(string[] items)
        {
            Items.Clear();
            Items.AddRange(items);
        }

        public event ComboBoxSelectHandler ItemSelected;
    }

    public delegate void ComboBoxSelectHandler(object sender, int Index);
}
