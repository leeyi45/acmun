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
    public partial class TimeSelector : UserControl
    {
        public TimeSelector()
        {
            InitializeComponent();

            minSelector.ValueChanged += valueChanged;
            secSelector.ValueChanged += valueChanged;

            mainLabel.Enter += enter;
            //mainLabel.BackColor = Parent.BackColor;
        }

        public TimeSpan Value
        {
            get => _value;
            set
            {
                _value = value;
                minSelector.Value = value.Minutes;
                secSelector.Value = value.Seconds;
            }
        }

        private TimeSpan _value;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get => mainLabel.Text;
            set => mainLabel.Text = value;
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public new int Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                mainLabel.Width = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TextBackColor
        {
            get => mainLabel.BackColor;
            set => mainLabel.BackColor = value;
        }

        public new bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                minSelector.Enabled = value;
                secSelector.Enabled = value;
            }
        }

        private bool _enabled = true;

        private void enter(object sender, EventArgs e) => ActiveControl = null;

        private void valueChanged(object sender, EventArgs e)
        {
            if (minSelector.Value == 0) secSelector.Minimum = 1;
            else secSelector.Minimum = 0;

            Value = new TimeSpan(0, (int)minSelector.Value, (int)secSelector.Value);
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ValueChanged;
    }
}
