using System;
using System.Threading;
using BotvaSpider.Configuration;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Farming;
using BotvaSpider.Fighting;
using BotvaSpider.Gears;
using Savchin.Core;

namespace BotvaSpider.Automation.Fights
{
    /// <summary>
    /// 
    /// </summary>
    internal abstract class RivalSourceBase : MachineWrapper
    {


        #region Properties
        protected readonly UserImporter importer = new UserImporter();

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public abstract RivalSource Type { get; }


        private int _attempt;

        /// <summary>
        /// Gets the attempt.
        /// </summary>
        /// <value>The attempt.</value>
        protected int Attempt
        {
            get { return _attempt; }
        }
        #endregion



        /// <summary>
        /// Initializes a new instance of the <see cref="RivalSourceBase"/> class.
        /// </summary>
        /// <param name="machine">The machine.</param>
        protected RivalSourceBase(FightMachine machine)
            : base(machine)
        {

        }

        /// <summary>
        /// Does the fight.
        /// </summary>
        public void DoFight()
        {
            do
            {
                var rival = FindRival();
                if (rival == null) return;

                KillRival(rival);
            }
            while (Machine.LastFight == null || (Machine.LastFight.NexFightTime - DateTime.Now).TotalMinutes < 1);
        }

        /// <summary>
        /// Finds the rival.
        /// </summary>
        /// <returns></returns>
        private Rival FindRival()
        {
            Log.InfoFormat("Исчем противника по {0}.", Type.GetDescription());
            var settings = GetSettings();

            if (AppCore.BotvaSettings.AutoDisguise)
            {
                var columoumb = settings.Coulomb > 0
                                    ? settings.Coulomb
                                    :AppCore.BotvaSettings.Wardrobe.GetCoulomb(PlayerAction.Fight);
                Player.PutOn(columoumb);
            }

            Controller.OpenUrl(Controller.UrlDozor);


            _attempt = 0;

            do
            {
                _attempt++;

                var result = TryFind(settings);
                if (result == GetRivalResult.System) break;


                if (result == GetRivalResult.OK)
                {
                    var rival = CreateRival();
                    if (rival != null)
                    {
                        var canKill = CanKillRival(rival, settings);
                        OnRivalCreated(rival, canKill);
                        if (canKill)
                        {
                            return rival;
                        }

                    }


                    Controller.GoTo(Controller.UrlDozor);
                }
                else if (result == GetRivalResult.FrequentlyFight)
                {
                    Thread.Sleep(new TimeSpan(0, 1, 0));
                }


            } while (_attempt < settings.MaxAttemptCount);

            return null;

        }


        /// <summary>
        /// Kills the rival.
        /// </summary>
        private bool KillRival(Rival rival)
        {
            Log.Debug("Бой с " + rival.Name, rival.ToString(), rival);


            var result = Controller.KillRival();
            if (result == null) return false;

            var sleepInterval = Controller.GetSleepTimeAfterFight();
            result.NexFightTime = (sleepInterval.HasValue) ? DateTime.Now + sleepInterval.Value : DateTime.Now;

            result.Coulomb = Player.Coulomb;
            result.SkillDifference = Player.GetSkillDifference(rival);
            result.Rival.Source = rival.Source;
            result.RivalSource = Type;

            Machine.OnFightOccured(new FightResultEventArgs(result));

            if (rival.Farm != null && !ReferenceEquals(rival.Farm, Machine.Farm))
                rival.Farm.AddResult(result);

            Machine.Farm.AddResult(result);
            importer.Import(result);
            if (result.Win)
                Log.Info("Победа в бое c " + rival.Name, result.ToString(), result);
            else
                Log.Warn("Пройгрыш боя c " + rival.Name, result.ToString(), result);

            return true;
        }
        /// <summary>
        /// Called when [rival created].
        /// </summary>
        /// <param name="rival">The rival.</param>
        /// <param name="canKill">if set to <c>true</c> [can kill].</param>
        protected virtual void OnRivalCreated(Rival rival, bool canKill)
        {

        }

        protected abstract GetRivalResult TryFind(RivalSourceSettings settings);

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns></returns>
        public RivalSourceSettings GetSettings()
        {
            return AppCore.AttackSettings.GetSettings(Type);
        }

        /// <summary>
        /// Determines whether this instance [can kill rival] the specified rival.
        /// </summary>
        /// <param name="rival">The rival.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can kill rival] the specified rival; otherwise, <c>false</c>.
        /// </returns>
        protected bool CanKillRival(Rival rival, RivalSourceSettings settings)
        {
            if (!settings.LevelFilter.CreateFull(Player.Level).IsInRange(rival.Level))
            {
                AppCore.LogFights.Debug(string.Format("Противник {0} негодиться для атаки.", rival.Name),
                    "Не подходит по уровню", rival);
                return false;
            }
            return Player.CanKill(rival) &&
                    Controller.CanKillRival(rival);

        }
        /// <summary>
        /// Creates the rival.
        /// </summary>
        /// <returns></returns>
        protected Rival CreateRival()
        {
            return Rival.Create(Browser, Type);
        }

        // private Coulomb previousCoulomb;
        /// <summary>
        /// Gets the rival to fight.
        /// </summary>
        /// <param name="cow">The cow.</param>
        /// <returns></returns>
        protected GetRivalResult GetRivalToFight(Cow cow)
        {
            if (AppCore.GameSettings.BotvaSettings.AutoDisguise)
            {
                var coulomb = (cow.MilkingCoulomb == Coulomb.Undefined) ?
                    AppCore.BotvaSettings.Wardrobe.GetCoulomb(PlayerAction.Fight) : cow.MilkingCoulomb;
                //  previousCoulomb = Player.Coulomb;
                Player.PutOn(coulomb);
            }
            return Controller.GetRivalToFight(cow.UserName);
        }
        ///// <summary>
        ///// Restores the coulomb.
        ///// </summary>
        //protected void RestoreCoulomb()
        //{
        //    if (!AppCore.BotvaSettings.AutoDisguise) return;

        //    var coloumb = AppCore.BotvaSettings.Wardrobe.GetCoulomb(PlayerAction.Sleep);
        //    if (coloumb == Coulomb.Undefined) coloumb = previousCoulomb;

        //    Player.PutOn(coloumb);
        //}

        /// <summary>
        /// Gets the rival to fight.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        protected GetRivalResult GetRivalToFight(string userName)
        {
            Player.PrepareForAction(PlayerAction.Fight);
            return Controller.GetRivalToFight(userName);
        }

        /// <summary>
        /// Updates the user from rival.
        /// </summary>
        /// <param name="rival">The rival.</param>
        protected void UpdateUserFromRival(Rival rival)
        {
            var user = ObjectProvider.Instance.GetUserByName(rival.Name);
            user.Level = rival.Level;
            user.Skills.FillSkils(rival.Skills);
            ObjectProvider.Instance.UpdateUser(user);
        }
    }
}
