using CodeRocket.Common;
using Savchin.Forms.Core.Commands;
using Savchin.Forms.Docking;

namespace CodeRocket.Commands
{
    class ShowConsoleCommand : Command
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            if (parameter == null || !(parameter is DockContent))
                return;

            ((DockContent)parameter).Show(AppCore.Current.Form.DockPanel);
        }
    }
}
