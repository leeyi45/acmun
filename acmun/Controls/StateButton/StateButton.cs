﻿using System.ComponentModel;
using System.Windows.Forms;

namespace leeyi45.acmun.Controls
{
    public partial class StateButton : Button
    {
        public StateButton()
        {
            InitializeComponent();
        }

        private bool init = false;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        [Category("Text")]
        [Description("Text to be displayed when the button is enabled")]
        public string TrueText
        {
            get => _trueText;
            set
            {
                _trueText = value;
                if (!init)
                {
                    Text = Enabled ? TrueText : FalseText;
                    if (!Enabled && string.IsNullOrEmpty(FalseText)) return;
                    init = true;
                }
            }
        }

        private string _trueText;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        [Category("Text")]
        [Description("Text to be displayed when the button is disabled")]
        public string FalseText
        {
            get => _falseText;
            set
            {
                _falseText = value;
                if (!init)
                {
                    Text = Enabled ? TrueText : FalseText;
                    if (Enabled && string.IsNullOrEmpty(TrueText)) return;
                    init = true;
                }
            }
        }

        private string _falseText;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public new bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                Text = Enabled ? TrueText : FalseText;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        public new string Text
        {
            get => base.Text;
            private set => base.Text = value;
        }
    }
}
