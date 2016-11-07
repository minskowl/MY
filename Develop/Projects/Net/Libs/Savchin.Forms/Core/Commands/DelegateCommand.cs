using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Forms.Core.Commands
{
    /// <summary>
    /// ExecuteDeleagate
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <param name="target">The target.</param>
    public delegate void ExecuteDeleagate(object parameter, object target);
    /// <summary>
    /// CanExecuteDeleagate
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <param name="target">The target.</param>
    /// <returns></returns>
    public delegate bool CanExecuteDeleagate(object parameter, object target);

    /// <summary>
    /// DelegateCommand
    /// </summary>
    public class DelegateCommand : Command
    {
        private readonly ExecuteDeleagate _execute;
        private readonly CanExecuteDeleagate _canExecute;
        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public DelegateCommand(ExecuteDeleagate execute, CanExecuteDeleagate canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        #region Overrides of Command


        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            _execute(parameter, target);
        }

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        /// <returns>
        ///   <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanExecute(object parameter, object target)
        {
            return _canExecute == null ? base.CanExecute(parameter, target) : base.CanExecute(parameter, target) && _canExecute(parameter, target);
        }
        #endregion
    }
}
