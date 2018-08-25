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

        public bool Elapsed { get; private set; } = false;

        void TickHandler(object sender, EventArgs e)
        {
            Tick?.Invoke(this, EventArgs.Empty);

            CurrentTime = CurrentTime.Add(OneSecond);

            if (CurrentTime > Duration)
            {
                Stop();
                Elapsed = true;
                TimeUp?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Start()
        {
            if (Running || Elapsed) return;
            Running = true;
            RunningChanged?.Invoke(this, EventArgs.Empty);
            Internal.Start();
        }

        public void Stop()
        {
            if (!Running) return;
            Running = false;
            RunningChanged?.Invoke(this, EventArgs.Empty);
            Internal.Stop();
        }

        public void Reset()
        {
            Stop();
            Elapsed = false;
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
            if (Duration > NewDuration) Elapsed = false;
            Duration = NewDuration;
        }

        public event EventHandler TimeUp;

        public event EventHandler Tick;

        public event EventHandler RunningChanged;

        public event EventHandler ResetTriggered;
    }
}
