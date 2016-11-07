namespace Savchin.Forms.Core.Commands.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    internal class CommandBinding : ICommandBinding
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public Command Command { get; private set; }
        /// <summary>
        /// Gets or sets the parameter source.
        /// </summary>
        /// <value>The parameter source.</value>
        public ParameterSource ParameterSource { get; private set; }

        /// <summary>
        /// Realeases the tracking.
        /// </summary>
        public virtual void RealeaseTracking()
        {
            Command.PropertyChanged -= OnCommandPropertyChanged;
            Command = null;
            ParameterSource = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBinding"/> class.
        /// </summary>
        /// <param name="command">The command.</param>
        public CommandBinding(Command command)
            : this(command, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBinding"/> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="parameterSource">The parameter source.</param>
        public CommandBinding(Command command, ParameterSource parameterSource)
        {
            Command = command;
            ParameterSource = parameterSource;
            Command.PropertyChanged += OnCommandPropertyChanged;
        }

        /// <summary>
        /// Called when [command property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCommandPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }
}