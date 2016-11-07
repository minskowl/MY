using System;
using System.Data;
using System.Windows.Forms;
using BotvaSpider.Core;
using BotvaSpider.Data;

namespace BotvaSpider.Controls.Statistics
{
    public partial class FightStatisticsControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FightStatisticsControl"/> class.
        /// </summary>
        public FightStatisticsControl()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
      
        }

        /// <summary>
        /// Shows the specified user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void Show(int userID)
        {
            var fights = ObjectProvider.Instance.GetFightLog(userID);

            dataGridView1.DataSource = fights;

            ShowStatistics(fights);


        }

        private void ShowStatistics(DataTable fights)
        {
            try
            {
                totalStatistics.AddRow("Боев", fights.Rows.Count);
                totalStatistics.AddRow("Кол-во пройгрышей", fights.Compute("COUNT(Date)", "Money<0 OR Cristals<0"));
                totalStatistics.AddRow("Награблено золота", fights.Compute("SUM(Money)", "Money>0"));
                totalStatistics.AddRow("Утеряно золота", fights.Compute("SUM(Money)", "Money<0"));
                totalStatistics.AddRow("Итого золота золота", fights.Compute("SUM(Money)", null));
                totalStatistics.AddRow("Ср. кол-во золота за бой", fights.Compute("AVG(Money)", "Money>0"));

                totalStatistics.AddRow("Награблено кристалов", fights.Compute("SUM(Cristals)", "Cristals>0"));
                totalStatistics.AddRow("Утеряно кристалов", fights.Compute("SUM(Cristals)", "Cristals<0"));
                totalStatistics.AddRow("Итого кристалов", fights.Compute("SUM(Cristals)", null));
                totalStatistics.AddRow("Ср кол-во кристалов за бой", fights.Compute("AVG(Cristals)", null));

                totalStatistics.AddRow("Кол-во опыта", fights.Compute("SUM(Expirience)", null));
                totalStatistics.ShowData();
               
            }
            catch (Exception ex)
            {
                AppCore.LogSystem.Error("Ошибка расчета статистики", ex);
            }
        }




    }
}