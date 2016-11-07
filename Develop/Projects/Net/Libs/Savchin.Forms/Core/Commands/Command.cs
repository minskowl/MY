using System.ComponentModel;

namespace Savchin.Forms.Core.Commands
{
    /// <summary>
    /// Command base class
    /// </summary>
    public abstract class Command : ICommand, INotifyPropertyChanged
    {
        #region Fields

        private bool _enabled = true;
        private bool _checked;
        private string _displayName;

        #endregion

        #region Properties




        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            protected set
            {
                if (_displayName == value) return;
                _displayName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DisplayName"));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Command"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public virtual bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                if (_enabled == value) return;
                _enabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Enabled"));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </value>
        public bool IsChecked
        {
            get
            {
                return _checked;
            }
            protected set
            {
                if (_checked == value)
                    return;
                _checked = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsChecked"));
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        #endregion

        #region Implementation of ICommand
        /// <summary>
        /// Executes the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        public void Execute(object target)
        {
            Execute(target, null);
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public abstract void Execute(object parameter, object target);

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanExecute(object parameter, object target)
        {
            return Enabled;
        }

        #endregion

        #region Implementation of INotifyPropertyChanged

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}