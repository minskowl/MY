#region Copyright
// <author>Adityanand Pasumarthi, 2005-2006</author>
#endregion

using System;
using System.Collections;
using System.Threading;
using Savchin.Threading.IOCP;

namespace Savchin.Threading
{
    /// <summary>
    /// Class for dispatching objects to a Managed IOCP based ThreadPool
    /// for processing.
    /// </summary>
    public class ThreadPool
    {

        #region Public Properties


        #region Private Data Members

        private ThreadPoolThreadExceptionHandler _tpThreadExceptionHandler;
        private ManagedIOCP<ITask> _mIOCP;
        private AutoResetEvent _ev = new AutoResetEvent(false);
        #endregion


        private short _concurrentThreads;
        /// <summary>
        /// Get/Set the Maximum concurrent threads allowed to be active in parallel
        /// in this ThreadPool
        /// </summary>
        public short ConcurrentThreads
        {
            get { return _concurrentThreads; }
            set { _concurrentThreads = value; }
        }

        private short _maxThreads;
        /// <summary>
        /// Get/Set the Maximum Threads allowed in this ThreadPool
        /// </summary>
        public short MaxThreads
        {
            get { return _maxThreads; }
            set { _maxThreads = value; }
        }

        private readonly string _name;
        /// <summary>
        /// Get/Set the name of this ThreadPool
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Get the number of active threads in this ThreadPool
        /// </summary>
        public int ActiveThreads
        {
            get
            {
                return _mIOCP.ActiveThreads;
            }
        }
        #endregion

        /// <summary>
        /// Delegate for receiving any unhandled exception notifications
        /// from threads used by a ThreadPool instance
        /// </summary>
        /// <param name="ex">
        /// Exception object that was uncaught in a running thread inside 
        /// of a ThreadPool instance
        /// </param>
        public delegate void ThreadPoolThreadExceptionHandler(Exception ex);


        #region Public Constructors
        /// <summary>
        /// Creates an instance of the ThreadPool with specified number
        /// of Maximum Threads allowed in the pool (maxThreads) and the
        /// Maximum concurrent threads allowed to be active in parallel
        /// </summary>
        /// <param name="maxThreads">Maximum Threads allowed in this ThreadPool</param>
        /// <param name="concurrentThreads">Maximum concurrent threads allowed to be active in parallel
        /// in this ThreadPool</param>
        public ThreadPool(short maxThreads, short concurrentThreads)
            : this(maxThreads, concurrentThreads, null, null)
        {
        }

        /// <summary>
        /// Creates an instance of the ThreadPool
        /// </summary>
        /// <param name="maxThreads">Maximum Threads allowed in this ThreadPool</param>
        /// <param name="concurrentThreads">
        /// Maximum concurrent threads allowed to be active in parallel in this ThreadPool
        /// </param>
        /// <param name="name">Name of this ThreadPool</param>
        public ThreadPool(short maxThreads, short concurrentThreads, string name)
            : this(maxThreads, concurrentThreads, name, null)
        {
        }

        /// <summary>
        /// Creates an instance of the ThreadPool
        /// </summary>
        /// <param name="maxThreads">Maximum Threads allowed in this ThreadPool</param>
        /// <param name="concurrentThreads">
        /// Maximum concurrent threads allowed to be active in parallel in this ThreadPool
        /// </param>
        /// <param name="name">Name of this ThreadPool</param>
        /// <param name="tpThEx">
        /// Delegate to be called when an uncaught exception is encountered 
        /// on a thread in this ThreadPool
        /// </param>
        public ThreadPool(short maxThreads, short concurrentThreads, string name, ThreadPoolThreadExceptionHandler tpThEx)
        {
            _maxThreads = maxThreads;
            _concurrentThreads = concurrentThreads;
            _tpThreadExceptionHandler = tpThEx;
            _name = name;
            _mIOCP = new ManagedIOCP<ITask>(_concurrentThreads);
            for (short index = 1; index <= _maxThreads; index++)
            {
                Thread th = new Thread(new ThreadStart(this.ThreadFunc));
                th.IsBackground = true;
                th.Start();
                _ev.WaitOne();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Dispatch a task object to be processed by threads in this thread pool.
        /// </summary>
        /// <param name="task"></param>
        public void Dispatch(ITask task)
        {
            _mIOCP.Dispatch(task);
        }

        /// <summary>
        /// Close the processing of objects by this Thread Pool. Effectively
        /// makes this Thread Pool instance invalid.
        /// </summary>
        public void Close()
        {
            _mIOCP.Close();
        }
        #endregion


        #region Private Methods
        private void ThreadFunc()
        {
            IOCPHandle<ITask> hIOCP = _mIOCP.Register();
            _ev.Set();
            try
            {
                while (true)
                {
                    ITask task = hIOCP.Wait();
                    if (task.Active == true)
                    {
                        task.Execute(this);
                        task.Done();
                    }
                }
            }
            catch (Exception ex)
            {
                // Raise an event to the registered even handlers 
                // for ThreadExceptions
                //
                Console.WriteLine(string.Format("Thread ID: {0} -- {1}, {2}",
                    AppDomain.GetCurrentThreadId(), ex.Message, ex.StackTrace));
                if (_tpThreadExceptionHandler != null) _tpThreadExceptionHandler(ex);
            }
            _mIOCP.UnRegister();
        }
        #endregion

    }
}
