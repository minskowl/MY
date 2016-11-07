using System;
using CodeRocket.Commands.Project;
using CodeRocket.Common;

namespace CodeRocket.Commands.Generate
{
    class GenerateToOutputCommand : ProjectCommand
    {
        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target"></param>
        /// <returns>
        /// 	<c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanExecute(object parameter, object target)
        {
            Enabled = AppCore.Current.Form.ProjectBrowser.SelectedGenerations.Count > 0;
            return base.CanExecute(parameter,target);
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            try
            {
                AppCore.Current.ActiveManager.GenerateToOutPutDir();
            }
            catch (Exception ex)
            {
                throw new CommandException("GenerateToOutputCommand", ex);
            }
        }
    }
}
