using System;
using System.Diagnostics;
using Savchin.IO;


namespace IES.PerformanceTester.Core
{
    public enum DisplayMode
    {
        Default,
        KBytes,
        MBytes
    }

    /// <summary>
    /// MemoryUsageDataRow
    /// </summary>
    public class MemoryUsageDataRow : IDisposable
    {

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>The current.</value>
        public float Current { get; set; }
        /// <summary>
        /// Gets or sets the previous.
        /// </summary>
        /// <value>The previous.</value>
        public float Previous { get; set; }
        /// <summary>
        /// Gets the previous delta.
        /// </summary>
        /// <value>The previous delta.</value>
        public float PreviousDelta
        {
            get
            {
                return Current - Previous;
            }
        }
        /// <summary>
        /// Gets or sets the freeze.
        /// </summary>
        /// <value>The freeze.</value>
        public float Freeze { get; set; }
        /// <summary>
        /// Gets the freeze delta.
        /// </summary>
        /// <value>The freeze delta.</value>
        public float FreezeDelta
        {
            get { return Current - Freeze; }
        }

        /// <summary>
        /// Gets or sets the min.
        /// </summary>
        /// <value>The min.</value>
        public float Min { get; set; }
        /// <summary>
        /// Gets or sets the max.
        /// </summary>
        /// <value>The max.</value>
        public float Max { get; set; }


        private PerformanceCounter _counter;
        private DisplayMode _mode;


        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryUsageDataRow"/> class.
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <param name="mode">The mode.</param>
        public MemoryUsageDataRow(PerformanceCounter counter, DisplayMode mode)
        {
            _counter = counter;
            _mode = mode;

            Label = counter.CounterName;
            Min = Single.MaxValue;
            Max = Single.MinValue;
        }

        /// <summary>
        /// Freezes this instance.
        /// </summary>
        public void MakeFreeze()
        {
            if (_counter == null) return;

            MakeMeasure();
            Freeze = Current;
            Max = Current;
            Min = Current;
        }

        /// <summary>
        /// Makes the measure.
        /// </summary>
        public void MakeMeasure()
        {
            if (_counter == null) return;

            Previous = Current;
            Current = GetNewValue();
            if (Current > Max) Max = Current;
            if (Current < Min) Min = Current;
        }



        public void Dispose()
        {
            if (_counter == null) return;
            _counter.Dispose();
            _counter = null;
        }

        /// <summary>
        /// Gets the new value.
        /// </summary>
        /// <returns></returns>
        private float GetNewValue()
        {
            switch (_mode)
            {

                case DisplayMode.KBytes:
                    return _counter.NextValue() / StorageSize.SizeKb;
                case DisplayMode.MBytes:
                    return (float)Math.Round(_counter.NextValue() / StorageSize.SizeMb, 2);
                default:
                    return _counter.NextValue();
            }
        }
    }
}