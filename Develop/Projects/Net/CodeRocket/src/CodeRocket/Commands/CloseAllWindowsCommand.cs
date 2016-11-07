using System;
using CodeRocket.Commands.Project;
using CodeRocket.Common;
using Savchin.Forms.Core.Commands;

namespace CodeRocket.Commands
{
    class CloseAllWindowsCommand : ProjectCommand
    {

        #region Overrides of Command

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            AppCore.Current.ActiveManager.FileTabs.ClearTabs();
        }

        #endregion
    }
}
