using System;
using System.Threading;
using BotvaSpider.Configuration;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Farming;
using BotvaSpider.Gears;
using WatiN.Core;

namespace BotvaSpider.Automation.Fights
{
    /// <summary>
    /// 
    /// </summary>
    class FightLoginedState : LoginedStateBase
    {


        private readonly FightMachine fightMachine;

        private readonly Fighter _fighter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FightLoginedState"/> class.
        /// </summary>
        /// <param name="automaton">The automaton.</param>
        public FightLoginedState(FightMachine automaton)
            : base(automaton)
        {
            fightMachine = automaton;
            _fighter = new Fighter(automaton);
        }

        #region Implementation

        /// <summary>
        /// Searches the rival.
        /// </summary>
        public override bool DoFight()
        {
            if (!WaitUntilCanFight())
                return false;

            var result = _fighter.DoFight();
            //RestoreCoulomb();

            return result;
        }



        private bool hasPatrools = true;
        /// <summary>
        /// Tries the go to patrol.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns></returns>
        public override DateTime? TryGoToPatrol(int minutes)
        {
            if (!hasPatrools)
            {
                return null;
            }
            if (! Player.WardrobeHasEmtySlots)
            {
                Log.Warn("В одевалке нет свободных мест.", "Вдозор ходим только с пустыми слотами чтобы сложить билеты на поляны.");
                return null;
            }
            Player.PrepareForAction(PlayerAction.GoToPatrol);

            if (Controller.TryGoPatrol(minutes))
            {
                hasPatrools = true;
                var sleepTime = Controller.GetLeftTime();
                return (sleepTime.HasValue) ? DateTime.Now + sleepTime.Value : DateTime.Now;
            }

            hasPatrools = false;
            return null;


        }



        #endregion




        private bool WaitUntilCanFight()
        {
            var newValueHasPatrools = Controller.GetHasPatrools();
            if (newValueHasPatrools.HasValue) hasPatrools = newValueHasPatrools.Value;

            if (Controller.GetAttackButton().Exists) return true;

            var sleepTime = Controller.GetLeftTime();
            if (Browser.Url.StartsWith(Controller.UrlMine))
            {
                Log.Debug("Ждем добычи кристала.");
                if (sleepTime.HasValue)
                {
                    Sleep(sleepTime.Value, true);
                }
                Machine.Miner.GetCrystal();
                Controller.GoTo(Controller.UrlDozor);
                sleepTime = Controller.GetLeftTime();
            }


            //Patrool sleep timer

            if (sleepTime.HasValue)
            {
                Log.Debug("Ждем конца дозора.");
                Sleep(sleepTime.Value, false);
                Controller.GoTo(Controller.UrlDozor);
            }


            while (!Controller.GetAttackButton().Exists)
            {
                if (Machine.CancelingAction) return false;

                var sleepUntilFight = Controller.GetSleepTimeAfterFight();
                if (sleepUntilFight.HasValue)
                {


                    if (AppCore.GameSettings.BotvaSettings.UsePatrol && sleepUntilFight.Value.TotalMinutes > 10)
                    {
                        var sleepUntil = Machine.TryGoToPatrol(10);
                        if (sleepUntil.HasValue) Sleep(sleepUntil.Value, false);
                    }
                    if (AppCore.MinerSettings.VisitMine && sleepUntilFight.Value.TotalMinutes > automaton.Miner.AttemptMinutes)
                    {
                        SearchCrystal();
                    }
                    else
                    {
                        Sleep(sleepUntilFight.Value, true);
                    }

                }
                else
                {
                    Log.Debug("Ожидаем боя.");
                    Sleep(new TimeSpan(0, 1, 0), false);
                }
                if (Machine.CancelingAction) return false;
                Controller.GoTo(Controller.UrlDozor);

            }
            return true;
        }







    }
}