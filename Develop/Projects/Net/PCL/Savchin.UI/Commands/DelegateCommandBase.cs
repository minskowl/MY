using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Input;
using CI.Common.Logging;
using CI.UI.Core;


namespace CITrader.Controls.Commands
{
    public abstract class DelegateCommandBase : ICommand
    {
        private static ILogger _logger;
        private static ILogger Logger
        {
            get { return _logger; // ?? (_logger = ServiceLocator.Current.GetInstance<ILogger>());
            }
        }

        private bool _isActive;
        private List<WeakEventHandler> _canExecuteChangedHandlers;
        private EventHandler _isActiveChanged;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (_isActive == value)
                    return;
                _isActive = value;
                OnIsActiveChanged();
            }
        }

        public virtual event EventHandler IsActiveChanged
        {
            add
            {
                EventHandler eventHandler = _isActiveChanged;
                EventHandler comparand;
                do
                {
                    comparand = eventHandler;
                    eventHandler = Interlocked.CompareExchange(ref _isActiveChanged, comparand + value, comparand);
                }
                while (eventHandler != comparand);
            }
            remove
            {
                EventHandler eventHandler = _isActiveChanged;
                EventHandler comparand;
                do
                {
                    comparand = eventHandler;
                    eventHandler = Interlocked.CompareExchange(ref _isActiveChanged, comparand - value, comparand);
                }
                while (eventHandler != comparand);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                WeakEventHandlerManager.AddWeakReferenceHandler(ref _canExecuteChangedHandlers, value, 2);
            }
            remove
            {
                WeakEventHandlerManager.RemoveWeakReferenceHandler(_canExecuteChangedHandlers, value);
            }
        }



        protected virtual void OnCanExecuteChanged()
        {
            WeakEventHandlerManager.CallWeakReferenceHandlers(this, _canExecuteChangedHandlers);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        protected virtual void OnIsActiveChanged()
        {
            var eventHandler = _isActiveChanged;
            if (eventHandler != null)
                eventHandler(this, EventArgs.Empty);
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        protected abstract void Execute(object parameter);
        /// <summary>
        /// Logs the specified execute method.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="parameter">The parameter.</param>
        protected static void Log(Delegate executeMethod, object parameter)
        {
            Logger?.Info("Invoke command {0}:{1}{2}",
                executeMethod.Target,
                executeMethod.GetMethodInfo().Name,
                $": parameter '{parameter?.ToString() ?? string.Empty}'");
        }

        protected abstract bool CanExecute(object parameter);
    }
}