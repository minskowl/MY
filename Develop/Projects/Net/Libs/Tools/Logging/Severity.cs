namespace Savchin.Logging
{
    /// <summary>
    /// Defines 
    /// </summary>
    public enum Severity
    {
        /// <summary>
        /// Specifies that message contains debug information which is used during development phase
        /// </summary>
        Debug,
        /// <summary>
        /// Provides detailed information about object live time events
        /// </summary>
        Info,
        /// <summary>
        /// Error was detected but this was expected error and it was properly processed
        /// </summary>
        Warning,
        /// <summary>
        /// Error was detected but it was not processed and it was catched by error processing mechanism
        /// </summary>
        Error,
        /// <summary>
        /// Fatal error was detected application. After this message is sent application is terminated
        /// </summary>
        FatalError
    }
}