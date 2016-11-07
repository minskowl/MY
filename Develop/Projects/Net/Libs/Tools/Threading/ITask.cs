namespace Savchin.Threading
{
    /// <summary>
    /// Interface used by ThreadPool class for executing
    /// Tasks dispatched to it
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Executes the corresponding task 
        /// </summary>
        /// <param name="tp">ThreadPool onto which this Task is dispatched</param>
        void Execute(ThreadPool tp);
        /// <summary>
        /// Specifies to the ThreadPool whether to Execute this task based on whether
        /// this Task is Active or not. This allows for cancellation of Tasks after
        /// they are dispatched to ThreadPool for execution
        /// </summary>
        bool Active {get;set;}
        /// <summary>
        /// Indicates the task that its execution has been completed.
        /// </summary>
        void Done();
    }
}