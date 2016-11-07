using CodeRocket.Common;


namespace CodeRocket.Commands.Project
{
    class ProjectCloseCommand : ProjectCommand
    {

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            AppCore.Current.ActiveManager.CloseProject();
        }
    }
}
