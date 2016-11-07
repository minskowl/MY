using System;
using Savchin.Core;
using Savchin.EventSpy.Core;
using Savchin.Forms.Core.Commands;

namespace Savchin.EventSpy.Commands
{
    class StartFormCommand : Command
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            EventSpyCore.StartUpForm.ShowDialog();
        }
    }
}
