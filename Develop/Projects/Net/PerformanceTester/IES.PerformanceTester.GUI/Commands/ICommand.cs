namespace IES.PerformanceTester.Gui.Commands
{
    /// <summary>
    /// ICommand interface
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes this instance.
        /// </summary>
        void Execute();
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        void Execute(object parameter);

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute(object parameter);

    }
}