using System;
using System.Windows.Forms;

namespace leeyi45.acmun
{
    class Clock
    {
        static Clock() 
            => OneSecond = new TimeSpan(0, 0, 1);

        static readonly TimeSpan OneSecond;

        public Clock()
        {
            Internal = new Timer { Interval = 1000 };

            Internal.Tick += TickHandler;
            
            CurrentTime = new TimeSpan(0, 0, 0);
        }

        public Clock(TimeSpan Duration) : this()
            => this.Duration = Duration;

        Timer Internal;

        public TimeSpan CurrentTime { get; private set; }

        public TimeSpan Duration { get; private set; }

        public bool Running { get; private set; } = false;

        void TickHandler(object sender, EventArgs e)
        {
            Tick?.Invoke(this, EventArgs.Empty);

            CurrentTime = CurrentTime.Add(OneSecond);

            if (CurrentTime > Duration)
            {
                Stop();
                TimeUp?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Start()
        {
            if (Running) return;
            Running = true;
            Started?.Invoke(this, EventArgs.Empty);
            Internal.Start();
        }

        public void Stop()
        {
            if (!Running) return;
            Internal.Stop();
            Running = false;
            Stopped?.Invoke(this, EventArgs.Empty);
        }

        public void Reset()
        {
            Stop();
            CurrentTime = TimeSpan.Zero;
            ResetTriggered?.Invoke(this, EventArgs.Empty);
        }

        public void Restart()
        {
            Reset();
            Start();
        }

        public void EditDuration(TimeSpan NewDuration)
        {
            Stop();
            Duration = NewDuration;
        }

        public event EventHandler TimeUp;

        public event EventHandler Tick;

        public event EventHandler Stopped;

        public event EventHandler Started;

        public event EventHandler ResetTriggered;
    }
}
