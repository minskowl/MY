using System.Windows.Input;

namespace Savchin.UI.Commands
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public interface ICommandEx : ICommand
    {
        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        void RaiseCanExecuteChanged();
    }

    public interface IDelegateCommand : ICommandEx
    {
        /// <summary>
        /// Executes this instance.
        /// </summary>
        void Execute();
        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute();
    }

    public interface IDelegateCommand<in T> : ICommandEx
    {
        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        void Execute(T parameter);
        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute(T parameter);
    }
}
