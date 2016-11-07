using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotvaSpider.Core;
using BotvaSpider.Data;
using log4net.Core;
using WatiN.Core.Interfaces;

namespace BotvaSpider.Farming
{
    /// <summary>
    /// 
    /// </summary>
    public class WarFarm : FarmBase
    {
        UserImporter importer = new UserImporter();
        /// <summary>
        /// Initializes a new instance of the <see cref="WarFarm"/> class.
        /// </summary>
        public WarFarm()
        {
            IsWarMode = true;
        }

        /// <summary>
        /// Initializes from list.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="controller"></param>
        public override void Initialize(Player player, GameController controller)
        {
            cows = new List<Cow>();
            foreach (var cowInfo in AppCore.AttackSettings.Cows)
            {
                if (string.IsNullOrEmpty(cowInfo.UserName)) continue;

                var farmCows = ObjectProvider.Instance.GetCowsByName(cowInfo.UserName);
                if (farmCows.Count == 0)
                    farmCows = AddNewCow(cowInfo.UserName, controller, player);

                foreach (var cow in farmCows)
                {
                    cow.MilkingCoulomb = cowInfo.Coulomb;
                    cow.RivalHealth = 1000;
                    cow.RivalInjuryHealth = 1000;
                    Cows.Add(cow);
                }

            }

            FillResults(controller);
        }

        private List<Cow> AddNewCow(string cowName, GameController controller, Player player)
        {
            var user = controller.SearchUser(cowName);
            if (user == null) return new List<Cow>();
            if (player.Race != user.Race)
            {
                user.UserType = UserType.Cow;
                importer.Import(user);
                return ObjectProvider.Instance.GetCowsByName(cowName);
            }
            else
            {
                return new List<Cow>
                           {
                               new Cow
                                   {
                                       AverageBenefit = 1000,
                                       LastBenefit =   1000,
                                       Level = user.Level,
                                       UserName = user.Name,
                                       UserType = UserType.Cow,
                                       State = CowState.Ready,
                                       
                                   }
                           };
            }



        }

        /// <summary>
        /// Gets the best cow.
        /// </summary>
        /// <returns></returns>
        public override Cow GetBestCow()
        {
            RefreshState();
            Cow bestCow = null;
            decimal maxBenefit = -1;
            var readyCows = Cows.Where(c => c.State == CowState.Ready).ToArray();
            foreach (var cow in readyCows)
            {
                var cristalBenefit = (cow.MilkingCount > 0 && cow.Cristals > 0)
                                         ? (cow.Cristals / cow.MilkingCount) * 800
                                         : 0;
                var benefit = cow.RivalHealth + cow.RivalInjuryHealth +
                    cow.AverageBenefit + cristalBenefit;
                if (benefit <= maxBenefit) continue;

                bestCow = cow;
                maxBenefit = benefit;
            }
            return bestCow;
        }



    }
}
