using System;

namespace Savchin.Web.UI
{

    public enum DialogResult
    {
        Abort,
        Cancel,
        Ignore,
        No,
        None,
        OK,
        Retry,
        Yes
    }
    public enum MessageButtons
    {
        OK,
        OKCancel,
        YesNo,
        RetryCancel,
        AbortRetryIgnore,
        YesNoCancel
    }

    public class MessageBoxEventArgs
    {
        private DialogResult result;

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>The result.</value>
        public DialogResult Result
        {
            get { return result; }
        }

        private String commandName;
        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <value>The name of the command.</value>
        public string CommandName
        {
            get { return commandName; }
        }

        private String commandArgument;
        /// <summary>
        /// Gets the command argument.
        /// </summary>
        /// <value>The command argument.</value>
        public string CommandArgument
        {
            get { return commandArgument; }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxEventArgs"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="commandArgument">The command argument.</param>
        internal MessageBoxEventArgs(DialogResult result, String commandName, string commandArgument)
        {
            this.result = result;
            this.commandName = commandName;
            this.commandArgument = commandArgument;
        }
    }
}
