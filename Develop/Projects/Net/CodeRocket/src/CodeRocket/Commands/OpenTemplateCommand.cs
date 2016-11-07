using CodeRocket.Common;
using Savchin.Core;
using Savchin.Forms.Core.Commands;

namespace CodeRocket.Commands
{
    public class OpenTemplateCommand : Command
    {

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            AppCore.Current.FileTabs.AddFile((string)parameter); 
        }
    }
}
