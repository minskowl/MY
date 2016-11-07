using System;
using System.Collections.Generic;

namespace IES.PerformanceTester.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class TestRunner : ITestRunner
    {
        #region Properties

        private readonly Type _testType;
        private readonly List<TestBase> _tests = new List<TestBase>();
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TestBase"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TestRunner&lt;T&gt;"/> is runned.
        /// </summary>
        /// <value><c>true</c> if runned; otherwise, <c>false</c>.</value>
        public bool Runned { get; private set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; set; }

        private int _threadCount;


        /// <summary>
        /// Gets or sets the thread count.
        /// </summary>
        /// <value>The thread count.</value>
        public int ThreadCount
        {
            get { return _threadCount; }
            set
            {
                if (_threadCount == value) return;

                _threadCount = value;
                if (Runned) ChangeThreads();
            }
        }

        /// <summary>
        /// Gets the tests.
        /// </summary>
        /// <value>The tests.</value>
        public IEnumerable<TestBase> Tests
        {
            get { return _tests; }
        } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunner"/> class.
        /// </summary>
        /// <param name="testType">Type of the test.</param>
        /// <param name="name">The name.</param>
        public TestRunner(Type testType, string name)
        {
            if (testType == null) throw new ArgumentNullException("testType");
            _testType = testType;
            Key = name;
        }

        #region Interface

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            if (!Enabled) return;

            StartThreads();
            Runned = true;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            foreach (var test in Tests)
            {
                test.Thread.Abort();
            }
            Runned = false;
        }
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            Stop();
            _tests.Clear();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Clear();
        }
        #endregion


        private void StartThreads()
        {
            ChangeThreads();
            foreach (var test in Tests)
            {
                test.Start();
            }
        }

        private void ChangeThreads()
        {
            if (_threadCount > _tests.Count)
            {
                CreateThreads(_threadCount - _tests.Count);
            }
            else if (_threadCount < _tests.Count)
            {
                RemoveThreads(_tests.Count - _threadCount);
            }
        }
        private void CreateThreads(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var test = (TestBase)Activator.CreateInstance(_testType);

                if (Runned) test.Start();
                _tests.Add(test);
            }
        }
        private void RemoveThreads(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var test = _tests[0];
                test.Stop();
                _tests.RemoveAt(0);
            }
        }

    }
}