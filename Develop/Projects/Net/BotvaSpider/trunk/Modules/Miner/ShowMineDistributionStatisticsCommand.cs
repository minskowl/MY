using System;
using BotvaSpider.Controls;
using BotvaSpider.Controls.Statistics;
using BotvaSpider.Core;
using Savchin.Forms.Core.Commands;

namespace BotvaSpider.Commands
{
    class ShowMineDistributionStatisticsCommand : Command
    {
        public override void Execute(object parameter, object target)
        {
            var form = FormContainer.Create(new MineCristalDistributionControl(), "Распределение крсталов в шахте");
            form.Width = 700;
            form.Show(AppCore.FormMain);
        }

     
    }
}
