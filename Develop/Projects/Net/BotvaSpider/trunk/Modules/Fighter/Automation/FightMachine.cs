using System;
using System.Collections.Generic;
using BotvaSpider.Core;
using BotvaSpider.Farming;
using BotvaSpider.Gears;

namespace BotvaSpider.Automation.Fights
{
    /// <summary>
    /// FightMachine
    /// </summary>
    public class FightMachine : MachineBase
    {
        #region Properties







        /// <summary>
        /// Gets or sets the previous coulomb.
        /// </summary>
        /// <value>The previous coulomb.</value>
        public Coulomb PreviousCoulomb { get; set; }

        /// <summary>
        /// Gets or sets the name of the rival user.
        /// </summary>
        /// <value>The name of the rival user.</value>
        public List<string> TopKills { get; private set; }
        private readonly MilkingFarm farm = new MilkingFarm();
        /// <summary>
        /// Gets the farm.
        /// </summary>
        /// <value>The farm.</value>
        public MilkingFarm Farm
        {
            get { return farm; }
        }
        private readonly WarFarm farmList = new WarFarm();
        /// <summary>
        /// Gets the list farm.
        /// </summary>
        /// <value>The list farm.</value>
        public WarFarm ListFarm
        {
            get { return farmList; }
        }


        #endregion



        /// <summary>
        /// Initializes a new instance of the <see cref="FightMachine"/> class.
        /// </summary>
        public FightMachine()
        {
            TopKills = new List<string>();
            logined = new FightLoginedState(this);

            Log = AppCore.LogFights;
        }


        /// <summary>
        /// Starts the fight.
        /// </summary>
        protected override void Run()
        {


        start:
            try
            {
                Login();
                // CreateScreenshot("test");
                if (CancelingAction) goto exit;
                if (state.State == MachineState.Error) goto start;

                do
                {
                    UpdatePlayerStatus();

                    if (!Farm.Initialized) Farm.Initialize(Player, Controller);
                    if (!ListFarm.Initialized) ListFarm.Initialize(Player, Controller);

                    DoTraning();

                    DoScheduleSleep();

                    if (!DoFight())//Can't fight waite 5 minutes
                    {
                        freeTime = DateTime.Now.AddMinutes(5);
                    }
                    DoTraning();
                    if (CancelingAction) goto exit;
                    if (state.State == MachineState.Error) goto start;


                    if (AppCore.BotvaSettings.UsePatrol && freeTime > DateTime.Now)
                    {
                        DoPatrool();
                    }

                    if (CancelingAction) goto exit;
                    if (state.State == MachineState.Error) goto start;

                    if (AppCore.MinerSettings.VisitMine && freeTime > DateTime.Now)
                    {
                        DoSearchCristals();
                    }

                    DoSleep();

                    if (CancelingAction) goto exit;
                    if (state.State == MachineState.Error) goto start;

                } while (true);

            }
            catch (LoginRequiredException)
            {
                goto start;
            }
            catch (Exception ex)
            {
                if (HandleException(ex)) goto start;
            }

        exit:
            state = logouted;
            isRunning = false;
        }

        private void DoPatrool()
        {
            DateTime? sleepUntil = null;
            do
            {
                sleepUntil = TryGoToPatrol(10);
                //Patrols ended
                if (!sleepUntil.HasValue) return;

                Sleep(sleepUntil.Value, false);

            } while (DateTime.Now < freeTime);
        }
        private void DoSearchCristals()
        {
            do
            {
                SearchCrystal();
            } while (DateTime.Now < freeTime);
        }

        private void DoSleep()
        {
            if (DoScheduleSleep()) return;

            if (LastFight != null)
            {
                Sleep(freeTime, true);
                return;
            }
        }

        private bool DoScheduleSleep()
        {
            var sleepRange = AppCore.BotvaSettings.GetSleepRange();
            if (sleepRange.IsEmpty) return false;

            Sleep(sleepRange.To.ToDateTime(DateTime.Today), true);
            return true;

        }


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing) farm.Save();
        }
    }
}