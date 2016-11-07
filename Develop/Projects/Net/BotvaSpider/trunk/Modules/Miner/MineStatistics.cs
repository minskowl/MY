using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BotvaSpider.Automation.Mining;

namespace BotvaSpider.Controls.Statistics
{
    internal class MineStatistics
    {
        public string Interval { get; set; }
       
        [DisplayName("�-�� �������")]
        public int Count { get; set; }
        [DisplayName("�-�� �������")]
        public int CountAttempt { get; set; }
        [DisplayName("�-�� ��������")]
        public int CountSuccess { get; set; }
        [DisplayName("�� ��������")]
        public double AvgSuccess
        {
            get
            {
                return CountAttempt == 0 ? 0
                           : (double)CountSuccess / CountAttempt;
            }
        }
        [DisplayName("�� %")]
        public double AvgPercentage { get; set; }

        [DisplayName("�-�� ���������")]
        public int CountCristals { get; set; }
        [DisplayName("�� ���������")]
        public double AvgCristals { get; set; }

        [DisplayName("�-�� � �������")]
        public int CountSmallTicket { get; set; }
        [DisplayName("�� � �������")]
        public double AvgSmallTicket { get; set; }

        [DisplayName("�-�� � �������")]
        public int CountBigTicket { get; set; }
        [DisplayName("�� � �������")]
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