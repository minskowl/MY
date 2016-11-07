using BotvaSpider.Configuration;
using BotvaSpider.Core;
using BotvaSpider.Data;

namespace BotvaSpider.Automation.Fights
{
    class HotListRivalSource : RivalSourceBase
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public override Core.RivalSource Type
        {
            get { return Core.RivalSource.FromHotList; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HotListRivalSource"/> class.
        /// </summary>
        /// <param name="machine">The machine.</param>
        public HotListRivalSource(FightMachine machine)
            : base(machine)
        {
        }



        /// <summary>
        /// Tries the find.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        protected override GetRivalResult TryFind(RivalSourceSettings settings)
        {
            if (Attempt > Machine.TopKills.Count) return GetRivalResult.System;

            var userName = Machine.TopKills[Attempt - 1];
            var cow = Machine.Farm.FindCow(userName);

            var result = cow == null ? GetRivalToFight(userName) : GetRivalToFight(cow);

            switch (result)
            {

                case GetRivalResult.Holiday:
                case GetRivalResult.ManyFights:
                case GetRivalResult.System:
                case GetRivalResult.ToDelete:
                case GetRivalResult.WasBlocked:
                case GetRivalResult.InWhiteList:
                case GetRivalResult.OK:
                    Machine.TopKills.Remove(userName);
                    break;
            }
            return result;
        }
    }
}
