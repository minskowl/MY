using System;

namespace Savchin.Threading.Tasks
{
    /// <summary>
    /// Waitable Task that contains the asynchronous code block, 
    /// which will be executed when the waitable task object is 
    /// executed on the Managed IOCP ThreadPool
    /// </summary>
    public class WaitableAsyncDelegateTask : WaitableGenericTask
    {
        #region Properties

        private AsyncTaskCompleted _asyncTaskCompleted = null;
        /// <summary>
        /// Gets or sets the task completed.
        /// </summary>
        /// <value>The task completed.</value>
        public AsyncTaskCompleted TaskCompleted
        {
            get { return _asyncTaskCompleted; }
            set { _asyncTaskCompleted = value; }
        }



        private Exception _exception = null;
        /// <summary>
        /// Gets the code exception.
        /// </summary>
        /// <value>The code exception.</value>
        public Exception CodeException
        {
            get { return _exception; }
        }

        private Exception _executionCompletionDelegateException = null;
        /// <summary>
        /// Gets the execution completion delegate exception.
        /// </summary>
        /// <value>The execution completion delegate exception.</value>
        public Exception ExecutionCompletionDelegateException
        {
            get { return _executionCompletionDelegateException; }
        }



        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="WaitableAsyncDelegateTask"/> class.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public WaitableAsyncDelegateTask(object obj)
            : base(null, obj)
        {
        }

        /// <summary>
        /// Executes the specified tp.
        /// </summary>
        /// <param name="tp">The tp.</param>
        public override void Execute(ThreadPool tp)
        {
            AsyncDelegate ad = this.UserObject as AsyncDelegate;
            if (ad != null)
            {
                try
                {
                    ad();
                }
                catch (Exception ex)
                {
                    _exception = ex;
                }

                try
                {
                    if (_asyncTaskCompleted != null) _asyncTaskCompleted();
                }
                catch (Exception exInner)
                {
                    _executionCompletionDelegateException = exInner;
                }
            }
            this._ev.Set();
        }
        //public override void Done()
        //{
        //    if (_asyncTaskCompleted != null) _asyncTaskCompleted();
        //    base.Done();
        //}
    }
}