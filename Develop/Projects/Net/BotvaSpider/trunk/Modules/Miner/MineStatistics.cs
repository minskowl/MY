using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BotvaSpider.Automation.Mining;

namespace BotvaSpider.Controls.Statistics
{
    internal class MineStatistics
    {
        public string Interval { get; set; }
       
        [DisplayName("К-во походов")]
        public int Count { get; set; }
        [DisplayName("К-во попыток")]
        public int CountAttempt { get; set; }
        [DisplayName("К-во успешных")]
        public int CountSuccess { get; set; }
        [DisplayName("Ср успешных")]
        public double AvgSuccess
        {
            get
            {
                return CountAttempt == 0 ? 0
                           : (double)CountSuccess / CountAttempt;
            }
        }
        [DisplayName("Ср %")]
        public double AvgPercentage { get; set; }

        [DisplayName("К-во кристалов")]
        public int CountCristals { get; set; }
        [DisplayName("Ср кристалов")]
        public double AvgCristals { get; set; }

        [DisplayName("К-во м билетов")]
        public int CountSmallTicket { get; set; }
        [DisplayName("Ср м билетов")]
        public double AvgSmallTicket { get; set; }

        [DisplayName("К-во б билетов")]
        public int CountBigTicket { get; set; }
        [DisplayName("Ср б билетов")]
        public double AvgBigTicket { get; set; }
  

        /// <summary>
        /// Computes the specified d.
        /// </summary>
        /// <param name="d">The d.</param>
        public MineStatistics(ICollection<SearchCristalResult> d)
        {
            if (d.Count == 0) return;

            Count = d.Count;
            CountAttempt = d.Where(e => e.DoAttempt).Count();
            CountSuccess = d.Where(e => e.HasBenefit).Count();
            AvgPercentage = d.Average(e => e.Percentage);


            var sucess = d.Where(e => e.HasBenefit).ToArray();
            if (sucess.Length == 0) return;
            CountCristals = sucess.Sum(e => e.Cristals);
            AvgCristals = sucess.Average(e => e.Cristals);

            CountSmallTicket = sucess.Sum(e => e.SmallTicket ? 1 : 0);
            AvgSmallTicket = sucess.Average(e => e.SmallTicket ? 1 : 0);

            CountBigTicket = sucess.Sum(e => e.BigTicket ? 1 : 0);
            AvgBigTicket = sucess.Average(e => e.BigTicket ? 1 : 0);

        }
    }
}