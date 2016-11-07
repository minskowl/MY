#region Version & Copyright
/* 
 * $Id: GridCommandEventArgs.cs 24224 2007-11-20 15:08:13Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Security.Permissions;
using System.Web;

namespace Savchin.Web.UI
{

    public delegate void DeleteCommandEventHandler(object sender, DeleteCommandEventArgs e);
    public delegate void GridCommandEventHandler(object sender, GridCommandEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class DeleteCommandEventArgs : EventArgs
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommandEventArgs"/> class.
        /// </summary>
        /// <param name="e">The <see cref="DeleteCommandEventArgs"/> instance containing the event data.</param>
         public DeleteCommandEventArgs(DeleteCommandEventArgs e)
            : this(e.CommandArgument)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridCommandEventArgs"/> class.
        /// </summary>
        /// <param name="argument">The argument.</param>
        public DeleteCommandEventArgs(string argument)
        {
            this.argument = argument;
        }

        private readonly string argument;
        // Properties
        /// <summary>
        /// Gets the command argument.
        /// </summary>
        /// <value>The command argument.</value>
        public string CommandArgument
        {
            get { return argument; }
        }

        private bool isSuccess;
        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <value>The name of the command.</value>
        public bool IsSuccessful
        {
            get { return isSuccess; }
            set { isSuccess = value; }
        }
    }


    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class GridCommandEventArgs : EventArgs
    {
        // Fields
        private readonly string argument;
        private readonly string commandName;

        // Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="GridCommandEventArgs"/> class.
        /// </summary>
        /// <param name="e">The <see cref="NewWayMedia.Common.Controls.GridCommandEventArgs"/> instance containing the event data.</param>
        public GridCommandEventArgs(GridCommandEventArgs e)
            : this(e.CommandName, e.CommandArgument)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridCommandEventArgs"/> class.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="argument">The argument.</param>
        public GridCommandEventArgs(string commandName, string argument)
        {
            this.commandName = commandName;
            this.argument = argument;
        }

        // Properties
        /// <summary>
        /// Gets the command argument.
        /// </summary>
        /// <value>The command argument.</value>
        public string CommandArgument
        {
            get{return argument;}
        }

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <value>The name of the command.</value>
        public string CommandName
        {
            get{return commandName;}
        }

        private bool isSuccess;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is successful.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is successful; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessful
        {
            get { return isSuccess; }
            set { isSuccess = value; }
        }
        private bool isProcessed;
        /// <summary>
        /// Gets or sets a value indicating whether this command is processed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is processed; otherwise, <c>false</c>.
        /// </value>
        public bool IsProcessed
        {
            get { return isProcessed; }
            set { isProcessed = value; }
        }
        private string message;

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get { return message; }
            set { message = value; }
        }


    }



}
