
namespace Savchin.Threading.Tasks
{
    /// <summary>
    /// Base class for all Task objects that can be processed by ThreadPool.
    /// </summary>
    public abstract class Task : ITask
    {

        protected bool _active = true;
        /// <summary>
        /// Specifies to the ThreadPool whether to Execute this task based on whether
        /// this Task is Active or not. This allows for cancellation of Tasks after
        /// they are dispatched to ThreadPool for execution
        /// </summary>
        /// <value></value>
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        protected object _id;
        /// <summary>
        /// Unique identification of the task object
        /// </summary>
        public object Id
        {
            get { return _id; }
        }

        private object _object;
        /// <summary>
        /// Get/Set the user object associated with this Task instance 
        /// </summary>
        public object UserObject
        {
            get
            {
                return _object;
            }
            set
            {
                _object = value;
            }
        }

        /// <summary>
        /// Creates a generic task object with an identification and user object
        /// </summary>
        /// <param name="id">Unique identity of this task object</param>
        /// <param name="obj">User object that can stored and retrieved from this task object</param>
        protected Task(object id, object obj)
        {
            _id = id;
            _object = obj;
        }

        public abstract void Execute(ThreadPool tp);
        /// <summary>
        /// Indicates the task that its execution has been completed.
        /// </summary>
        public void Done()
        {

        }

    }
}
