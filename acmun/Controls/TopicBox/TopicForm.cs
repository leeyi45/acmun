using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace leeyi45.acmun.Controls
{
    partial class TopicBox
    {
        public partial class TopicForm : Form
        {
            public TopicForm()
            {
                InitializeComponent();

                button1.Click += Button1_Click;
                button2.Click += Button2_Click;
                textBox1.KeyDown += TextBox1_KeyDown;
                StartPosition = FormStartPosition.CenterParent;

                DialogResult = DialogResult.Cancel;
            }

            private void TextBox1_KeyDown(object sender, KeyEventArgs e)
            {
                switch(e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            DialogResult = DialogResult.OK;
                            Close();
                            return;
                        }
                    case Keys.Escape:
                        {
                            if(string.IsNullOrEmpty(textBox1.Text))
                            {
                                DialogResult = DialogResult.Cancel;
                                Close();
                            }
                            else textBox1.Clear();
                            break;
                        }
                }
            }

            private void Button2_Click(object sender, EventArgs e)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }

            private void Button1_Click(object sender, EventArgs e)
            {
                DialogResult = DialogResult.OK;
                Close();
            }

            public static (DialogResult, string) Show(string topic, string existing)
            {
                var form = new TopicForm();
                form.label1.Text = $"Enter new topic for {topic}:";
                form.textBox1.Text = existing;
                return (form.ShowDialog(), form.textBox1.Text);
            }
        }
    }
}
