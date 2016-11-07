using System;
using System.Linq;
using BotvaSpider.Core;
using BotvaSpider.Data;
using System.Collections.Generic;


namespace BotvaSpider.Automation.Fights
{
    /// <summary>
    /// Fighter
    /// </summary>
    class Fighter : MachineWrapper
    {
        readonly Dictionary<RivalSource, RivalSourceBase> _sources = new Dictionary<RivalSource, RivalSourceBase>();


        /// <summary>
        /// Initializes a new instance of the <see cref="Fighter"/> class.
        /// </summary>
        /// <param name="machine">The machine.</param>
        public Fighter(FightMachine machine)
            : base(machine)
        {

            Add(new HotListRivalSource(machine));
            Add(new RandomRivalSource(machine));
            Add(new FarmRivalSource(machine, machine.Farm));
            Add(new FarmRivalSource(machine, machine.ListFarm));
            Add(new StaffListRivalSource(machine,RandomSearchMode.RobberyList,RivalSource.StaffRobberyList));
            Add(new StaffListRivalSource(machine, RandomSearchMode.GloryList, RivalSource.StaffGloryList));
            Add(new StaffListRivalSource(machine, RandomSearchMode.RevengeList, RivalSource.StaffRevengeList));
        }



        /// <summary>
        /// Finds the rival.
        /// </summary>
        /// <returns></returns>
        public bool DoFight()
        {
            var sources = AppCore.AttackSettings.RivalSources.Where(e => e.Enabled).ToArray();
            if (sources == null || sources.Length == 0)
            {
                Log.Warn("Нету активных источников противника.",
                    "Не можем атаковать по причине остуствия активных источнков в Настройках > Атака.");
                return false;
            }

            foreach (var setting in sources)
            {
                if (!_sources.ContainsKey(setting.Source)) continue;
                _sources[setting.Source].DoFight();

                if (Machine.LastFight != null && 
                    (Machine.LastFight.NexFightTime - DateTime.Now).TotalMinutes > 1)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Adds the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        private void Add(RivalSourceBase source)
        {
            _sources.Add(source.Type, source);
        }
    }
}
