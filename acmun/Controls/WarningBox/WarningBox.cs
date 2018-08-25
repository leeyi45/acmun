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
    public partial class WarningBox : UserControl
    {
        private WarningBox()
        {
            InitializeComponent();
        }

        private int ShowDialog(string mainText, string[] buttonText)
        {
            TextBox.Text = mainText;
            var buttons = new[] { leftButton, middleButton, rightButton };

            for (int i = 0; i < 3; i++)
            {
                if (string.IsNullOrWhiteSpace(buttonText[i])) buttons[i].Enabled = false;
                else buttons[i].Text = buttonText[i];
            }

            do { } while (!Clicked);

            return Result;
        }

        private bool Clicked = false;

        private int Result;

        private void button1_click(object sender, EventArgs e) { Result = 0; Clicked = true; }
        private void button2_click(object sender, EventArgs e) { Result = 1; Clicked = true; }
        private void button3_click(object sender, EventArgs e) { Result = 2; Clicked = true; }

        public static int ShowMessage(string mainText, string button1_text, string button2_text, string button3_text)
        {
            using(var box = new WarningBox())
            {
                return box.ShowDialog(mainText, new[] { button1_text, button2_text, button3_text });
            }
        }

    }
}
