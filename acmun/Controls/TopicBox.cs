using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace leeyi45.acmun.Controls
{
    class TopicBox : TextBox
    {
        public TopicBox()
        {
            Enter += topicbox_enter;
            Leave += topicbox_leave;
        }

        private bool entered = false;

        private void topicbox_enter(object sender, EventArgs e)
        {
            if (entered) return;
            else
            {
                entered = true;
                BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void topicbox_leave(object sender, EventArgs e)
        {
            if (!entered) return;
            BorderStyle = BorderStyle.None;
            entered = false;
        }
    }
}
