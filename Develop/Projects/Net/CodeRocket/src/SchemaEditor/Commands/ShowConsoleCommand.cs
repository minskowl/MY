using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Savchin.Controls.Docking;
using Savchin.Core;
using SchemaEditor.Core;

namespace SchemaEditor.Commands
{
    class ShowConsoleCommand : Command
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter)
        {
            if (parameter == null || !(parameter is DockContent))
                return;

            ((DockContent)parameter).Show(AppCore.FormMain.DockPanel);
        }
    }
}
