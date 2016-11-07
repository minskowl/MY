using System;

namespace Savchin.Threading.Tasks
{
    /// <summary>
    /// Task that contains the asynchronous code block, which
    /// will be executed when the Task object is executed on 
    /// the Managed IOCP ThreadPool
    /// </summary>
    public class AsyncDelegateTask : Task
    {

        #region Properties
        private Exception _exception = null;
        private Exception _executionCompletionDelegateException = null;
        private AsyncTaskCompleted _asyncTaskCompleted = null;

        /// <summary>
        /// Gets or sets the task completed.
        /// </summary>
        /// <value>The task completed.</value>
        public AsyncTaskCompleted TaskCompleted
        {
            get
            {
                return _asyncTaskCompleted;
            }
            set
            {
                _asyncTaskCompleted = value;
            }
        }

        public Exception CodeException
        {
            get { return _exception; }
        }

        public Exception ExecutionCompletionDelegateException
        {
            get { return _executionCompletionDelegateException; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncDelegateTask"/> class.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public AsyncDelegateTask(object obj)
            : base(null, obj)
        {
        }

        /// <summary>
        /// Executes the specified tp.
        /// </summary>
        /// <param name="tp">The tp.</param>
        public override void Execute(Savchin.Threading.ThreadPool tp)
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
        }



    }
}