using System;
using System.Collections.Generic;

namespace IES.PerformanceTester.Tests
{
    public interface ITestRunner : IDisposable
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TestBase"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        bool Enabled { get; set; }
        bool Runned { get; }
        /// <summary>
        /// Gets the tests.
        /// </summary>
        /// <value>The tests.</value>
        IEnumerable<TestBase> Tests { get; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        string Key { get; set; }

        /// <summary>
        /// Gets or sets the thread count.
        /// </summary>
        /// <value>The thread count.</value>
        int ThreadCount { get; set; }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();
    }
}