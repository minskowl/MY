using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Forms.Core.Commands
{
    /// <summary>
    /// SimpleCommand
    /// </summary>
    public abstract class SimpleCommand : Command
    {

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public abstract void Execute();
    

        #region Overrides of Command

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            Execute();
        }

        #endregion
    }
}
