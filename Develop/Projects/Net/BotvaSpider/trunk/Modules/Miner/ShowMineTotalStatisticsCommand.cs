using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotvaSpider.Controls;
using BotvaSpider.Controls.Statistics;
using BotvaSpider.Core;
using Savchin.Forms.Core.Commands;

namespace BotvaSpider.Commands
{
    class ShowMineTotalStatisticsCommand : Command
    {
        public override void Execute(object parameter, object target)
        {
            // var form= new Form\

            var form = FormContainer.Create(new MineTotalStatistics(), "Статистика по шахте");
            form.Width = 700;
            form.Show(AppCore.FormMain);
        }
    }
}
