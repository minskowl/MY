using System.Windows.Forms;
using CodeRocket.Common;

namespace CodeRocket.Commands.Project
{
    class ProjectSaveAsCommand : ProjectCommand
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            var dialog = AppCore.Current.SaveFileDialog;

            dialog.FileName = Project.ProjectFileName;
            if (dialog.ShowDialog() == DialogResult.OK)
                Project.Save(dialog.FileName);
        }
    }
}