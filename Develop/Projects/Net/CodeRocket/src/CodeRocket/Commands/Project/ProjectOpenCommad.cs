using System.Windows.Forms;
using CodeRocket.Common;
using Savchin.Forms.Core.Commands;

namespace CodeRocket.Commands.Project
{
    class ProjectOpenCommad : Command
    {

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {

            OpenFileDialog dialog = AppCore.Current.OpenFileDialog;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            AppCore.Current.LoadProject(dialog.FileName);

        }
    }
}
