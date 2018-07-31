using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace leeyi45.acmun.Controls
{
    class LabelBox : TextBox
    {
        public LabelBox() : base()
        {
            base.Cursor = Cursors.Arrow;
            base.ReadOnly = true;
            base.TabStop = false;
            base.BorderStyle = BorderStyle.None;
            TextAlign = HorizontalAlignment.Center;

            Enter += Disable;
        }

        [DefaultValue(typeof(HorizontalAlignment), "Center")]
        public new HorizontalAlignment TextAlign
        {
            get => base.TextAlign;
            set => base.TextAlign = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new BorderStyle BorderStyle => base.BorderStyle;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool TabStop => base.TabStop;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ReadOnly => base.ReadOnly;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Cursor Cursor => base.Cursor;

        private void Disable(object sender, EventArgs e)
        {
            if (ParentForm != null) ParentForm.ActiveControl = null;
        }

        public static Form ParentForm { get; set; }
    }
}
