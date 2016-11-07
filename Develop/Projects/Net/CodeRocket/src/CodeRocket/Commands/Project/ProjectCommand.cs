using CodeRocket.Common;
using Savchin.CodeGeneration;
using Savchin.Forms.Core.Commands;

namespace CodeRocket.Commands.Project
{
    internal abstract class ProjectCommand : Command
    {

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        protected GenerateProject Project
        {
            get { return AppCore.Current.ActiveManager == null ? null : AppCore.Current.ActiveManager.Project; }
        }

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        public override bool  CanExecute(object parameter, object target)
        {
            Enabled = Project != null;
            return base.CanExecute(parameter, target);
        }
    }
}