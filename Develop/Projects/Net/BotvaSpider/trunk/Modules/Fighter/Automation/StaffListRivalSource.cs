using BotvaSpider.Configuration;
using BotvaSpider.Core;

namespace BotvaSpider.Automation.Fights
{
    class StaffListRivalSource : RivalSourceBase
    {
        private readonly RandomSearchMode mode;
        private readonly RivalSource _type;
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public override RivalSource Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomRivalSource"/> class.
        /// </summary>
        /// <param name="machine">The machine.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="type">The type.</param>
        public StaffListRivalSource(FightMachine machine, RandomSearchMode mode, RivalSource type)
            : base(machine)
        {
            this.mode = mode;
            this._type = type;
        }

        /// <summary>
        /// Tries the find.
        /// </summary>
        /// <param name="settings">The settings.</param>
        protected override GetRivalResult TryFind(RivalSourceSettings settings)
        {

            return Controller.SearchRandomRival(mode);
        }
    }
}
