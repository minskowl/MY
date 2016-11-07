using System;
using System.Threading;
using BotvaSpider.Configuration;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Farming;

namespace BotvaSpider.Automation.Fights
{
    class FarmRivalSource : RivalSourceBase
    {
        private readonly FarmBase _farm;
        private Cow _cow;

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public override Core.RivalSource Type
        {
            get { return _farm.IsWarMode ? Core.RivalSource.FromList : Core.RivalSource.FromFarm; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmRivalSource"/> class.
        /// </summary>
        /// <param name="machine">The machine.</param>
        /// <param name="farm">The farm.</param>
        public FarmRivalSource(FightMachine machine, FarmBase farm)
            : base(machine)
        {
            _farm = farm;
        }




        /// <summary>
        /// Called when [rival created].
        /// </summary>
        /// <param name="rival">The rival.</param>
        /// <param name="canKill">if set to <c>true</c> [can kill].</param>
        protected override void OnRivalCreated(Rival rival, bool canKill)
        {
            if (rival.Level > _cow.Level)
            {
                UpdateUserFromRival(rival);
                _cow.Level = rival.Level;
            }
            if (!canKill)
            {
                _farm.RemoveCow(_cow);
            }
        }

        protected override GetRivalResult TryFind(RivalSourceSettings settings)
        {
            _cow = _farm.GetBestCow();
            if (_cow == null) return GetRivalResult.System;

            var result = GetRivalToFight(_cow);
            _farm.Process(_cow, result);


            return result;
        }
    }
}
