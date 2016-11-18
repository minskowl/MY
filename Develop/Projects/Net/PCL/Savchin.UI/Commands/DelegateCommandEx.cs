using System;
using Savchin.UI.Commands;

namespace CITrader.Controls.Commands
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public class DelegateCommandEx : DelegateCommandBase, IDelegateCommand
    {
        private readonly Action _executeMethod;
        private readonly Func<bool> _canExecuteMethod;
        public DelegateCommandEx(Action executeMethod)
            : this(executeMethod, null)
        {
        }

        public DelegateCommandEx(Action executeMethod, Func<bool> canExecuteMethod)
        {
            if (executeMethod == null)
                throw new ArgumentNullException("executeMethod");

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public void Execute()
        {
            Execute(null);
        }

        public bool CanExecute()
        {
            return CanExecute(null);
        }

        protected override bool CanExecute(object parameter)
        {
            return _canExecuteMethod == null || _canExecuteMethod();
        }
        protected override void Execute(object parameter)
        {
            Log(_executeMethod, parameter);
            _executeMethod();
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public class DelegateCommandEx<T> : DelegateCommandBase, IDelegateCommand<T>
    {
        private readonly Action<T> _executeMethod;
        private readonly Func<T, bool> _canExecuteMethod;

        public DelegateCommandEx(Action<T> executeMethod)
            : this(executeMethod, o => true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommandEx&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        public DelegateCommandEx(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            if (executeMethod == null)
                throw new ArgumentNullException("executeMethod");

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }


        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void Execute(object parameter)
        {
            Execute(Convert(parameter));
        }

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        protected override bool CanExecute(object parameter)
        {
            return CanExecute(Convert(parameter));
        }


        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void Execute(T parameter)
        {
            Log(_executeMethod, parameter);
            _executeMethod(parameter);
        }



        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute(T parameter)
        {
            return _canExecuteMethod == null || _canExecuteMethod(parameter);
        }

        private static T Convert(object parameter)
        {
            return parameter == null ? default(T) : (T)parameter;
        }

    }
}
