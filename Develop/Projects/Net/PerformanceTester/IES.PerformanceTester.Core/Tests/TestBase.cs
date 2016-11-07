using System;
using System.Diagnostics;
using System.Threading;
using log4net;
using Savchin.Utils;

namespace IES.PerformanceTester.Tests
{
    /// <summary>
    /// TestBase
    /// </summary>
    public abstract class TestBase
    {
        public readonly ILog Log;


        #region Properties

        /// <summary>
        /// Gets or sets the thread.
        /// </summary>
        /// <value>The thread.</value>
        public Thread Thread { get; private set; }
        public int  ThreadId { get; private set; }
        private int counterErrors;
        /// <summary>
        /// Gets the counter errors.
        /// </summary>
        /// <value>The counter errors.</value>
        public int CounterErrors
        {
            get { return counterErrors; }
        }

        private long countersAttempts;
        /// <summary>
        /// Gets the counters attempts.
        /// </summary>
        /// <value>The counters attempts.</value>
        public long CountersAttempts
        {
            get { return countersAttempts; }
        }
        /// <summary>
        /// Gets the count tests.
        /// </summary>
        /// <value>The count tests.</value>
        protected abstract int CountTests { get; }


        /// <summary>
        /// Gets or sets the performance log.
        /// </summary>
        /// <value>The performance log.</value>
        public ILog PerformanceLog
        {
            get { return _perforLog; }
            set
            {
                _perforLog = value;
                if (value != null)
                    _watch = new Stopwatch();
            }
        }

        private ILog _perforLog;
        private Stopwatch _watch;

        #endregion

        // private int 
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBase"/> class.
        /// </summary>
        protected TestBase()
        {
            Log = LogManager.GetLogger(GetType());
            Thread = new Thread(RunTests);



        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            counterErrors = 0;
            countersAttempts = 0;

            Thread.Start();
        }
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            Thread.Abort();
        }

        private void RunTests()
        {
            if (!Initialize()) return;
            var testNumber = 0;
            ThreadId = Thread.CurrentThread.ManagedThreadId;
        doTest:
            try
            {
                while (true)
                {
                    countersAttempts = CountersAttempts + 1;
                    testNumber = Randomizer.GetIntegerBetween(0, CountTests);

                    Log.DebugFormat("Thread {0} start test {1}", ThreadId, testNumber);

                    if (_perforLog != null) _watch.Start();

                    DoTest(testNumber);

                    if (_perforLog != null)
                    {
                        _watch.Stop();
                        PerformanceLog.InfoFormat("{0};{1}", testNumber, _watch.ElapsedTicks);
                        _watch.Reset();
                    }

                }
            }
            catch (ThreadAbortException)
            {
                Log.InfoFormat("Thread {0} was stopped", ThreadId);
            }
            catch (OutOfMemoryException ex)
            {
                Log.Fatal(string.Format("OutOfMemoryException in thread {0}", ThreadId), ex);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error in tests thread {0}", ThreadId), ex);
                counterErrors = CounterErrors + 1;
                if (CounterErrors < 50) goto doTest;
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected virtual bool Initialize()
        {
            return true;
        }

        protected abstract void DoTest(int testNumber);
    }
}