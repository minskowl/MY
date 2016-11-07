using BotvaSpider.Configuration;
using BotvaSpider.Core;
using BotvaSpider.Data;
using WatiN.Core;

namespace BotvaSpider.Automation.Fights
{
    class RandomRivalSource : RivalSourceBase
    {

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public override RivalSource Type
        {
            get { return RivalSource.FromRandom; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomRivalSource"/> class.
        /// </summary>
        /// <param name="machine">The machine.</param>
        public RandomRivalSource(FightMachine machine)
            : base(machine)
        {
        }


        /// <summary>
        /// Finds the rival.
        /// </summary>
        /// <returns></returns>
        protected override GetRivalResult TryFind(RivalSourceSettings settings)
        {
            return AppCore.AcountSettings.CoolStatus ?
                Controller.SearchRival(settings.LevelFilter.CreateFull(Player.Level)) :
                Controller.SearchRandomRival(RandomSearchMode.Equal);
        }
    }
}
