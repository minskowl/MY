using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Controls;
using BotvaSpider.Controls.Statistics;
using BotvaSpider.Core;
using BotvaSpider.Farming;
using Savchin.Forms.Core.Commands;

namespace BotvaSpider.Commands
{
    /// <summary>
    /// ShowFightLogCommand
    /// </summary>
    internal class ShowFightLogCommand : Command
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            Execute((Cow)parameter);
        }

        /// <summary>
        /// Executes the specified cow.
        /// </summary>
        /// <param name="cow">The cow.</param>
        public void Execute(Cow cow)
        {
            var control = new FightStatisticsControl();
            var form = FormContainer.Create(control, "Ћог боев " + cow.UserName);
            form.StartPosition = FormStartPosition.CenterParent;
            form.Show(AppCore.FormMain);
            control.Show(cow.UserID);
        }
    }
}
