using System;
using System.Collections;
using System.Collections.Generic;

namespace IES.PerformanceTester.Tests
{
    /// <summary>
    /// TestRunnersCollection
    /// </summary>
    public class TestRunnersCollection : IEnumerable<ITestRunner>, IDisposable
    {
        private readonly Dictionary<string, ITestRunner> _storage = new Dictionary<string, ITestRunner>();

        /// <summary>
        /// Gets the <see cref="ITestRunner"/> with the specified key.
        /// </summary>
        /// <value></value>
        public ITestRunner this[string key]
        {
            get
            {
                return (_storage.ContainsKey(key)) ? _storage[key] : null;
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get { return _storage.Count; }
        }

        /// <summary>
        /// Adds the specified runner.
        /// </summary>
        /// <param name="runner">The runner.</param>
        public void Add(ITestRunner runner)
        {
            _storage.Add(runner.Key, runner);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            foreach (var runner in _storage.Values)
            {
                if (!runner.Runned) runner.Start();
            }
        }
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            foreach (var runner in _storage.Values)
            {
                runner.Stop();
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<ITestRunner> GetEnumerator()
        {
            return _storage.Values.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Clear();
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            foreach (var value in _storage.Values)
            {
                value.Dispose();
            }
            _storage.Clear();
        }
    }
}