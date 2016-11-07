using System;
using CodeRocket.Commands.Project;
using CodeRocket.Common;
using Savchin.Core;

namespace CodeRocket.Commands.Generate
{

    class GenerateToSolutionCommand : ProjectCommand
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
                AppCore.Current.ActiveManager.GenerateToSolution();
            }
            catch (Exception ex)
            {
                throw new CommandException("GenerateToSolutionCommand", ex);
            }
        }
    }
}
