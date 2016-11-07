using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Commands;
using BotvaSpider.Consoles;
using BotvaSpider.Controls;
using BotvaSpider.Core;
using Savchin.Forms.Core.Commands;

namespace BotvaSpider.Miner
{
    public class MinerModule : IModule
    {
        /// <summary>
        /// Initilaizes this instance.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="statisticsMenu">The statistics menu.</param>
        public void Initilaize(MainFormBase form, ToolStripMenuItem statisticsMenu)
        {

            form.AddConsole<MiningConsole>("Шахта");

            var statisticsMinerMenu = new ToolStripMenuItem("Шахта");
            statisticsMenu.DropDownItems.Add(statisticsMinerMenu);

            //TODO: Uncomment
    //        statisticsMinerMenu.DropDownItems.Add("Распределение кристалов в шахте")
    //            .BindCommand(new ShowMineDistributionStatisticsCommand());

    //        statisticsMinerMenu.DropDownItems.Add("Суммарная статистика")
    //.BindCommand(new ShowMineTotalStatisticsCommand());

        }

    }
}
