namespace Savchin.Forms.Core.Commands.Handlers
{
    interface ICommandBinding
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        Command Command { get; }
        /// <summary>
        /// Gets or sets the parameter source.
        /// </summary>
        /// <value>The parameter source.</value>
        ParameterSource ParameterSource { get; }

        /// <summary>
        /// Realeases the tracking.
        /// </summary>
        void RealeaseTracking();
       
    }
}
