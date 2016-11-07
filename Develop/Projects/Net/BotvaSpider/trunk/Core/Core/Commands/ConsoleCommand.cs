using System;
using System.Linq;
using System.Windows.Forms;
using BotvaSpider.Core;
using Savchin.Forms.Docking;
using Savchin.Forms.Core.Commands;

namespace BotvaSpider.Commands
{
    /// <summary>
    /// ConsoleCommand
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConsoleCommand<T> : Command where T : DockContent, new()
    {

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            foreach (var console in Application.OpenForms.OfType<T>())
            {
                console.Activate();
                return;
            }
            var form = new T();
            form.Show(AppCore.FormMain.DockPanel);
        }
    }
}
