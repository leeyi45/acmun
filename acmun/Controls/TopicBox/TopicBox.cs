using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace leeyi45.acmun.Controls
{
    partial class TopicBox : TextBox
    {
        public TopicBox()
        {
            Click += TopicBox_Click;
        }

        private void TopicBox_Click(object sender, EventArgs e)
        {
           (var info, var str) = TopicForm.Show(Topic, Text);
            if(info == DialogResult.OK)
            {
                Text = str;
                TopicChanged?.Invoke(this, EventArgs.Empty);
            }            
        }

        public string Topic { get; set; }

        public event EventHandler TopicChanged;
    }
}
