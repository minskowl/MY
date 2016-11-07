using System;
using System.Threading;
using BotvaSpider.Automation.Mining;
using BotvaSpider.BookKeeping;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Fighting;
using BotvaSpider.Logging;
using Savchin.Core;
using WatiN.Core;

namespace BotvaSpider.Automation
{
    public abstract class MachineBase : AutomatonBase<IFightClub>, IFightClub, IDisposable
    {
        private readonly Thread runingThread;
        protected MailBox mailBox;
        private Accountant accountant;

        private bool initialized = false;
        protected bool cancelingAction;
        protected bool isRunning = false;

        protected IFightClub logouted;
        protected IFightClub logined;
        protected IFightClub sleep;
        protected IFightClub error;



        /// <summary>
        /// Occurs when [fight occured].
        /// </summary>
        public event EventHandler<FightResultEventArgs> FightOccured;




        #region Properties
        /// <summary>
        /// Gets the sleep minutes between fights.
        /// </summary>
        /// <value>The sleep between fights.</value>
        private int SleepBetweenFights
        {
            get { return AppCore.AcountSettings.CoolStatus ? 5 : 15; }
        }

        protected DateTime freeTime;


        /// <summary>
        /// Gets or sets the last fight.
        /// </summary>
        /// <value>The last fight.</value>
        public FightResult LastFight { get; private set; }

        private int errorsCountMining;
        /// <summary>
        /// Gets a value indicating whether this instance can mining.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can mining; otherwise, <c>false</c>.
        /// </value>
        public bool CanMining
        {
            get { return errorsCountMining < AppCore.BotvaSettings.MaxDangerousErrors; }
        }

        private int errorCount = 0;
        /// <summary>
        /// Gets the error count.
        /// </summary>
        /// <value>The error count.</value>
        public int ErrorCount
        {
            get { return errorCount; }
        }

        private int badErrorCount = 0;
        /// <summary>
        /// Gets the error count.
        /// </summary>
        /// <value>The error count.</value>
        public int BadErrorCount
        {
            get { return badErrorCount; }
        }
        public Logger Log { get; protected set; }

        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        internal IE Browser { get; private set; }


        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <value>The controller.</value>
        public GameController Controller { get; private set; }
        /// <summary>
        /// Gets or sets the coach.
        /// </summary>
        /// <value>The coach.</value>
        public Coach Coach { get; private set; }
        /// <summary>
        /// Gets or sets me.
        /// </summary>
        /// <value>Player.</value>
        public Player Player { get; internal set; }
        /// <summary>
        /// Gets or sets the miner.
        /// </summary>
        /// <value>The miner.</value>
        public Miner Miner { get; internal set; }



        /// <summary>
        /// Gets the mail box.
        /// </summary>
        /// <value>The mail box.</value>


        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public MachineState State
        {
            get { return state.State; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is fighting.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is fighting; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning
        {
            get { return isRunning; }
        }
        /// <summary>
        /// Gets the money.
        /// </summary>
        /// <value>The money.</value>
        public int Money
        {
            get { return Player == null ? 0 : Player.Money; }
        }


        /// <summary>
        /// Gets a value indicating whether [canceling fight].
        /// </summary>
        /// <value><c>true</c> if [canceling fight]; otherwise, <c>false</c>.</value>
        public bool CancelingAction
        {
            get { return cancelingAction; }
        }
        #endregion

        /// <summary>
        /// Occurs when [state changed].
        /// </summary>
        public event EventHandler<MachineStateEventArgs> StateChanged;
        static MachineBase()
        {
            AppCore.GameSettings.WatinSettings.Setup();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineBase"/> class.
        /// </summary>
        protected MachineBase()
        {



            logouted = new LogoutedState(this);

            error = new ErrorState(this);
            sleep = new SleepState(this);



            state = logouted;

            runingThread = new Thread(Run);
            runingThread.IsBackground = true;
            runingThread.SetApartmentState(ApartmentState.STA);

        }

        /// <summary>
        /// Gets the status message.
        /// </summary>
        /// <returns></returns>
        public string GetStatusMessage()
        {
            return Player != null
                       ? string.Format("Статус: {1} Золото: {0} Кристалы:{2} ",
                       Player.Money, State.GetDescription(), Player.Cristals) :
                           string.Format("Статус: {0} Золото: - Кристалы: - ", State.GetDescription());

        }

        #region Actions
        /// <summary>
        /// Does the fight.
        /// </summary>
        public bool DoFight()
        {
            try
            {
                return state.DoFight();
            }
            catch (NoMoneyException)
            {
                return false;
            }
        }


        /// <summary>
        /// Raises the <see cref="E:FightOccured"/> event.
        /// </summary>
        /// <param name="args">The <see cref="FightResultEventArgs"/> instance containing the event data.</param>
        public virtual void OnFightOccured(FightResultEventArgs args)
        {
            LastFight = args.Result;

            freeTime = LastFight.NexFightTime;


            if (args.Result.Win)
            {
                Accountant.RegisterBalanceItem(new BalanceItem
                {
                    Category = BalanceCategory.Fights,
                    Item = "Бой",
                    Gold = Math.Abs(LastFight.Money),
                    Cristal = Math.Abs(LastFight.Crystals),
                    SmallTicket = false,
                    BigTicket = false,
                    IsProfit = LastFight.Win
                });
            }

            if (FightOccured != null)
                FightOccured(this, args);
        }

        /// <summary>
        /// Does the traning.
        /// </summary>
        /// <returns></returns>
        public bool DoTraning()
        {
            return state.DoTraning();
        }


        /// <summary>
        /// Updates the player status.
        /// </summary>
        public virtual void UpdatePlayerStatus()
        {
            state.UpdatePlayerStatus();
        }


        /// <summary>
        /// Searches the crystal.
        /// </summary>
        public void SearchCrystal()
        {
        tryAgain:
            if (!CanMining) return;
            Log.Debug("В шахту за кристалами");
            try
            {
                state.SearchCrystal();
            }
            catch (Exception ex)
            {
                if (Controller.IsInternetError())
                {
                    if (HandleInternetError(ex))
                        goto tryAgain;

                    return;
                }
                Log.Warn("Ошибка копания в шахте", ex);
                errorsCountMining++;
                if (errorsCountMining >= AppCore.BotvaSettings.MaxDangerousErrors)
                {
                    Log.Error("Модуль копания в шахте заблокирован.", "Слишком много ошибок", ex);
                }
            }
        }



        /// <summary>
        /// Tries the go to patrol.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns></returns>
        public DateTime? TryGoToPatrol(int minutes)
        {
            var time = state.TryGoToPatrol(minutes);
            if (time.HasValue) Log.DebugFormat("Отправились в дозор до {0}", time);
            return time;
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        public void Login()
        {
            Initialize();
            state.Login();

            if (state.State == MachineState.Loggined)
            {
                if (accountant.NeedNotification) mailBox.Start();
                accountant.Start();
                if (AppCore.AttackSettings.IgnoreWarsClan) Controller.AddCurrentWarsInWhiteList();

            }


        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public virtual void Logout()
        {
            state.Logout();

            mailBox.Stop();
            accountant.Stop();
        }
        #endregion

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            errorCount = 0;
            badErrorCount = 0;
            cancelingAction = false;
            isRunning = true;

            runingThread.Start();
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        protected abstract void Run();
        /// <summary>
        /// Stops the fighting.
        /// </summary>
        public void Stop()
        {
            cancelingAction = true;

            if (!runingThread.IsAlive) return;

            runingThread.Abort();
            runingThread.Join(new TimeSpan(0, 0, 10));

        }
        /// <summary>
        /// Does the farm.
        /// </summary>
        public void DoFarm()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Sleeps the specified span.
        /// </summary>
        /// <param name="until">The until.</param>
        /// <param name="canDisguise">if set to <c>true</c> [can disguise].</param>
        public void Sleep(DateTime until, bool canDisguise)
        {
            state.Sleep(until, canDisguise);
        }
        /// <summary>
        /// Needs the money.
        /// </summary>
        public void NeedMoney()
        {
            state.NeedMoney();
        }

        protected void Initialize()
        {
            if (initialized) return;
            CreateTransitionMap();

            Controller = new GameController(Log);
            Player = new Player(Controller);
            Miner = new Miner(Player, Controller);
            Coach = new Coach(Player, Controller);
            mailBox = new MailBox(Log);

            accountant = new Accountant(mailBox, Player);

            Browser = Controller.Browser;


            initialized = true;
        }

        #region Handling Exception
        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>True- machine works False- machine stop</returns>
        protected bool HandleException(Exception ex)
        {
            //Stop machine
            if (ex is ThreadAbortException)
            {
                mailBox.Stop();
                if (accountant != null) accountant.Stop();
                Player.PrepareForAction(PlayerAction.Sleep);
                return false;
            }


            CastEvent(Event.Error);


            return CanRun(ex);
        }

        private bool CanRun(Exception ex)
        {

            if (Controller.IsInternetError())
            {
                if (HandleInternetError(ex) && ReloadAccountPage())
                {
                    ;
                    return true;
                }
                return false;
            }

            badErrorCount++;
            Controller.HandleException("!!!!!!!!! Необработаное исключение", ex);

            if (badErrorCount >= AppCore.BotvaSettings.MaxDangerousErrors)
            {
                Log.Error("!!!! Произошло слишком много опасных ошибок.", "Выполнение бота остановлено по причине большого кол-ва ОПАСНЫХ ошибок.\n");
                return false;
            }
            return true;
        }

        private bool HandleInternetError(Exception ex)
        {
            errorCount++;
            if (errorCount >= AppCore.BotvaSettings.MaxInternetErrors)
            {
                Log.Error("!!!! Произошло слишком много ошибок.", "Выполнение бота остановлено по причине большого кол-ва ошибок.\n Вполне возможно сайт Ботвы отсутствова по техническим причинам.");
                return false;
            }
            Log.Warn("Ошибка Интернета", ex);
            Thread.Sleep(new TimeSpan(0, 1, 0));
            return true;

        }

        private bool ReloadAccountPage()
        {
            do
            {
                try
                {
                    Controller.GoTo(Controller.UrlAccount);
                    return true;
                }
                catch (Exception ex)
                {
                    AppCore.LogSystem.Error("Немогу перегрузить страничку аккаунта.", ex);
                    errorCount++;
                    Thread.Sleep(new TimeSpan(0, 1, 0));
                }
            } while (errorCount < AppCore.BotvaSettings.MaxInternetErrors);

            return false;
        }

        #endregion
        /// <summary>
        /// Casts the event.
        /// </summary>
        /// <param name="e">The e.</param>
        public override void CastEvent(Event e)
        {
            base.CastEvent(e);
            OnStateChanged(new MachineStateEventArgs(state.State));
        }

        /// <summary>
        /// Raises the <see cref="E:StateChanged"/> event.
        /// </summary>
        /// <param name="args">The <see cref="MachineStateEventArgs"/> instance containing the event data.</param>
        protected virtual void OnStateChanged(MachineStateEventArgs args)
        {
            if (StateChanged != null)
                StateChanged(this, args);
        }

        private void CreateTransitionMap()
        {
            //Logouted state
            AddEdge(logouted, Action.Login, logined);
            AddEdge(logouted, Action.Error, error);

            //Login state
            AddEdge(logined, Action.Logout, logouted);
            AddEdge(logined, Action.Sleep, sleep);
            AddEdge(logined, Action.Error, error);

            //Sleep State
            AddEdge(sleep, Action.Awake, logined);
            AddEdge(sleep, Action.Logout, logouted);
            AddEdge(sleep, Action.Error, error);

            //Error State
            AddEdge(error, Action.Login, logined);
            AddEdge(error, Action.Error, error);
        }

        #region Implementation of IDisposable

        /// <summary>
        ///                     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            try
            {
                cancelingAction = true;



                if (Controller != null && Controller.Browser != null)
                {
                    Controller.Browser.Dispose();
                }
            }
            catch (Exception ex)
            {
                AppCore.LogSystem.Log.Error("Error to dispose FightMachine", ex);
            }
        }

        #endregion
    }
}