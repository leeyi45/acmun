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
    public partial class TimeBar : UserControl
    {
        public TimeBar()
        {
            Internal = new Clock();
            InitializeComponent();

            Internal.TimeUp += Internal_TimeUp;
            Internal.Tick += Internal_Tick;
            Internal.RunningChanged += Internal_RunningChanged;
        }

        private void Internal_RunningChanged(object sender, EventArgs e)
        {
            RunningChanged?.Invoke(this, EventArgs.Empty);
        }

        private Clock Internal;

        private void Internal_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            label1.Text = $"{Internal.ElapsedTime.ToValString()}/{Duration.ToValString()}";
        }

        private void Internal_TimeUp(object sender, EventArgs e) 
            => label1.ForeColor = Color.Red;

        public TimeSpan Duration
        {
            get => Internal.Duration;
            set
            {
                Internal.EditDuration(value);
                progressBar1.Maximum = (int)value.TotalSeconds;
                label1.Text = $"{Internal.ElapsedTime.ToValString()}/{value.ToValString()}";
            }
        }

        public TimeSpan ElapsedTime => Internal.ElapsedTime;

        public void Start() => Internal.Start();

        public void Stop() => Internal.Stop();

        public void Reset()
        {
            Internal.Reset();
            label1.ForeColor = Color.Black;
            progressBar1.Value = 0;
            label1.Text = $"00:00/{Duration.ToValString()}";
        }

        public void Restart()
        {
            Reset();
            Start();
        }

        public bool Running => Internal.Running;

        public event EventHandler RunningChanged;
    }
}
