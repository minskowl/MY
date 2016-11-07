using System;
using CodeRocket.Commands.Project;
using CodeRocket.Common;


namespace CodeRocket.Commands.Generate
{

    class GenerateToSolutionDirCommand : ProjectCommand
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            try
            {
                AppCore.Current.ActiveManager.GenerateToSolutionDir();
            }
            catch (Exception ex)
            {
                throw new CommandException("GenerateToSolutionDirCommand", ex);
            }
        }
    }
}
