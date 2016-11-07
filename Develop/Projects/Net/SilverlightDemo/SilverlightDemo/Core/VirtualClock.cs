using System;
using EffectiveSoft.SilverlightDemo.Controls;
using EffectiveSoft.SilverlightDemo.Objects;

namespace EffectiveSoft.SilverlightDemo.Core
{
    public class VirtualClock : ClockBase
    {
        private static readonly DateTime zeroTime = new DateTime(2008, 1, 1, 1, 0, 0);
        private DateTime currentTime;

        private readonly static VirtualClock instance = new VirtualClock();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static VirtualClock Instance
        {
            get { return instance; }
        }

        private VirtualClock()
        {
            currentTime = zeroTime;
            SetClockInterval(new TimeSpan(0, 0, 0, 1));

        }



        public override IClockModel Start()
        {
            if (IsRunning)
                return this;

            timer.Start();
            isRunning = true;

            return this;
        }

        public override IClockModel Stop()
        {
            timer.Stop();
            isRunning = false;

            return this;
        }

        /// <summary>
        /// Continues this instance.
        /// </summary>
        /// <returns></returns>
        public override IClockModel Continue()
        {
            return this.Start();
        }

        /// <summary>
        /// Updates the clock data.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        protected override void UpdateClockData(DateTime dateTime)
        {
            currentTime=currentTime.AddMinutes(1);
            if (currentTime.Hour > 12)
                currentTime = zeroTime;

            this.ClockData.Update(currentTime);
        }
    }
}
