using System;
using System.Windows.Threading;

namespace EffectiveSoft.SilverlightDemo.Controls
{
    public abstract class ClockBase : IClockModel
    {

        private ClockData clockData;
        protected bool isRunning;
        protected DispatcherTimer timer;

        public event EventHandler<ClockDataEventArgs> ClockChanging;
        public event EventHandler<ClockDataEventArgs> ClockChanged;

        public ClockBase()
        {
            clockData = new ClockData();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            this.SetClockInterval(new TimeSpan(0, 0, 0, 0, 1));
        }

        public virtual void SetClockInterval(TimeSpan timeSpan)
        {
            timer.Interval = timeSpan;
        }

        public ClockData ClockData
        {
            get
            {
                return this.clockData;
            }
            set
            {
                this.OnClockChangind(new ClockDataEventArgs(this.clockData));

                this.clockData = value;

                this.OnClockChanged(new ClockDataEventArgs(this.clockData));
            }
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        public abstract IClockModel Start();
        public abstract IClockModel Stop();
        public abstract IClockModel Continue();
        protected abstract void UpdateClockData(DateTime dateTime);

        protected virtual void OnClockChangind(ClockDataEventArgs e)
        {
            EventHandler<ClockDataEventArgs> temp = this.ClockChanging;
            if (temp != null)
                temp(this, e);
        }

        protected virtual void OnClockChanged(ClockDataEventArgs e)
        {
            EventHandler<ClockDataEventArgs> temp = this.ClockChanged;
            if (temp != null)
                temp(this, e);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.OnClockChangind(new ClockDataEventArgs(this.ClockData));

            this.UpdateClockData(DateTime.Now);

            this.OnClockChanged(new ClockDataEventArgs(this.ClockData));
        }
    }
}
