namespace Savchin.Threading
{
    /// <summary>
    /// Delegate that will be executed after completion of the
    /// execution of the AsyncDelegateTask or WaitableAsyncDelegateTask
    /// </summary>
    public delegate void AsyncTaskCompleted();

    /// <summary>
    /// Delegate representing the asynchronous code block
    /// </summary>
    public delegate void AsyncDelegate();

    /// <summary>
    /// Delegate that will be executed after completion of execution
    /// of the asynchronous code blocks
    /// </summary>
    /// <param name="objAsync"></param>
    public delegate void AsyncCodeBlockExecutionCompleteCallback(async objAsync);
}
