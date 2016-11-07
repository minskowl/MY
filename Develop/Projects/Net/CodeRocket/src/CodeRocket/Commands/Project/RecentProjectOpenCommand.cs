using System;
using CodeRocket.Common;
using Savchin.Core;
using Savchin.Forms.Core.Commands;

namespace CodeRocket.Commands.Project
{
    class RecentProjectOpenCommand : Command
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            var fileName = parameter as string;
            if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fileName))
                return;


            AppCore.Current.LoadProject(fileName);
        }
    }
}
