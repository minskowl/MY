using System;
using System.Linq;
using BotvaSpider.Core;
using BotvaSpider.Data;

namespace BotvaSpider.Farming
{
    public class MilkingFarm : FarmBase
    {

        #region Initailize
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="controller"></param>
        public override void Initialize(Player player, GameController controller)
        {
            AppCore.LogFights.Debug("Инициализируем ферму");
            base.Initialize(player, controller);


            var filter = AppCore.AttackSettings.GetSettings(RivalSource.FromFarm).LevelFilter.CreateFull(player.Level);

            var newCows = ObjectProvider.Instance.GetCows(filter).Where(e => e.AverageBenefit >= AppCore.AttackSettings.MinBenefit).ToList();
            AddNew(newCows);

            FillResults(controller);
            AddPotentioal(filter);

            foreach (var cow in cows)
            {
                cow.LastBenefit = (int)cow.AverageBenefit;
            }
        }

        private void AddPotentioal(LevelFilter filter)
        {
            try
            {
                var poterial = ObjectProvider.Instance.GetPotentialCows(filter, AppCore.AttackSettings.MinBenefit).
                    Where(e => e.AverageBenefit > AppCore.AttackSettings.MinBenefit).ToArray();

                var cowsIds = (from p in Cows
                               select p.UserID).ToList();
                var toUpdate = (from p in poterial
                                where cowsIds.Contains(p.UserID)
                                select p).ToArray();

                foreach (var cowU in toUpdate)
                {
                    var updated = (from c in Cows
                                   where c.UserID == cowU.UserID
                                   select c).FirstOrDefault();

                    if (updated != null) updated.AverageBenefit = (updated.AverageBenefit + cowU.AverageBenefit) / 2;
                }

                var toAdd = (from p in poterial
                             where !cowsIds.Contains(p.UserID)
                             select p).ToArray();
                Cows.AddRange(toAdd);
            }
            catch(Exception ex)
            {
                AppCore.LogSystem.Warn("Проблемы в инициалиции фермы",ex );
            }
        }

        /// <summary>
        /// Gets the best cow.
        /// </summary>
        /// <returns></returns>
        public override Cow GetBestCow()
        {
            RefreshState();

            var maxLastBenefit = GetMaxLastBenefit();

            if (maxLastBenefit.HasValue && maxLastBenefit > AppCore.AttackSettings.MinBenefit)
            {
                var result = Cows.Where(c => c.State == CowState.Ready && c.LastBenefit == maxLastBenefit.Value).FirstOrDefault();
                if (result != null) return result;
            }


            var maxAverageBenefit = GetMaxAverageBenefit();
            if (maxAverageBenefit.HasValue && maxAverageBenefit > AppCore.AttackSettings.MinBenefit)
            {
                return Cows.Where(c => c.State == CowState.Ready && c.AverageBenefit == maxAverageBenefit.Value).FirstOrDefault();

            }
            return null;
        }





        #endregion





        private int? GetMaxLastBenefit()
        {
            try
            {
                return Cows.Where(c => c.State == CowState.Ready).Max(c => c.LastBenefit);
            }
            catch
            {
                return (int?)null;
            }
        }


        private decimal? GetMaxAverageBenefit()
        {
            try
            {
                return Cows.Where(c => c.State == CowState.Ready).Max(c => c.AverageBenefit);
            }
            catch
            {
                return (decimal?)null;
            }
        }




    }
}