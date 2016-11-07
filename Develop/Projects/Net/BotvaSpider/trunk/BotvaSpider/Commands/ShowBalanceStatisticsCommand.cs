using BotvaSpider.Controls;
using BotvaSpider.Controls.Statistics;
using BotvaSpider.Core;
using Savchin.Forms.Core.Commands;

namespace BotvaSpider.Commands
{
    class ShowBalanceStatisticsCommand : Command
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter, object target)
        {
            var form = FormContainer.Create(new BalanceControl(), "Баланс игрока.");
            form.Width = 700;
            form.Show(AppCore.FormMain);
        }
    }
}
