using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BotvaSpider.BookKeeping;
using BotvaSpider.Core;
using BotvaSpider.Data;
using Savchin.TimeManagment;

namespace BotvaSpider.Controls.Statistics
{
    public partial class BalanceControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BalanceControl"/> class.
        /// </summary>
        public BalanceControl()
        {
            InitializeComponent();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            var range = dateRangeControl1.Value;

            var data = ObjectProvider.Instance.GetBalanceItems(new DateRange(range.From, range.To.AddMilliseconds(1)));
            Compute(gridTotal, data);
            Compute(gridMine, data.Where(d => d.Category == BalanceCategory.Mine));
            Compute(gridFights, data.Where(d => d.Category == BalanceCategory.Fights));
            ComputeGlade(gridGlade, data.Where(d => d.Category == BalanceCategory.Glades));
        }

        private static void Compute(LabelValueGrid grid, IEnumerable<BalanceItem> data)
        {

            grid.Clear();

            grid.AddRow("Прибыль золота.", data.Sum(d => d.IsProfit ? d.Gold : 0));
            grid.AddRow("Затраты золота.", data.Sum(d => d.IsProfit ? 0 : d.Gold));
            grid.AddRow("Получено золота.", data.Sum(d => d.IsProfit ? d.Gold : -d.Gold));

            grid.AddRow("Прибыль кристалов.", data.Sum(d => d.IsProfit ? d.Cristal : 0));
            grid.AddRow("Затраты кристалов.", data.Sum(d => d.IsProfit ? 0 : d.Cristal));
            grid.AddRow("Получено кристалов.", data.Sum(d => d.IsProfit ? d.Cristal : -d.Cristal));


            grid.AddRow("Малых билетов.", data.Sum(d => d.IsProfit && d.SmallTicket ? 1 : 0));
            grid.AddRow("Больших билетов.", data.Sum(d => d.IsProfit && d.BigTicket ? 1 : 0));

            grid.ShowData();
        }
        private static void ComputeGlade(LabelValueGrid grid, IEnumerable<BalanceItem> data)
        {

            grid.Clear();

            grid.AddRow("Прибыль кристалов.", data.Sum(d => d.Cristal));
            grid.AddRow("Прибыль Малых билетов.", data.Sum(d => d.SmallTicket ? 1 : 0));
            grid.AddRow("Прибыль Больших билетов.", data.Sum(d  => d.BigTicket ? 1 : 0));


            grid.AddRow("Прибыль кристалов на Малых.", data.Where(d => d.Item == "Малая поляна").Sum(d => d.Cristal));
            grid.AddRow("Больших кристалов на Больших .", data.Where(d => d.Item == "Большая поляна").Sum(d => d.Cristal));

            grid.ShowData();
        }

    }
}
