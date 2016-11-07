using System;
using System.ComponentModel;
using System.Threading;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Logging;
using WatiN.Core;


namespace BotvaSpider.Automation
{
    public enum MachineState
    {
        [Description("В Ботве")]
        Loggined,
        [Description("Вышли из ботвы")]
        Logouted,
        Fighting,
        Fight,
        [Description("Ошибка")]
        Error,
        [Description("Сон")]
        Sleep
    }

    /// <summary>
    /// MachineStateBase
    /// </summary>
    public abstract class MachineStateBase : StateBase<MachineBase>, IFightClub
    {

        #region Properties
        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        protected IE Browser
        {
            get { return automaton.Browser; }
        }
        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <value>The controller.</value>
        protected GameController Controller
        {
            get { return automaton.Controller; }
        }
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public abstract MachineState State { get; }

        /// <summary>
        /// Gets the log.
        /// </summary>
        /// <value>The log.</value>
        public Logger Log
        {
            get { return automaton.Log; }
        }

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value>The player.</value>
        public Player Player
        {
            get { return automaton.Player; }
        }
        /// <summary>
        /// Gets the machine.
        /// </summary>
        /// <value>The machine.</value>
        protected MachineBase Machine
        {
            get { return automaton; }
        }
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="MachineStateBase"/> class.
        /// </summary>
        /// <param name="automaton">The automaton.</param>
        protected MachineStateBase(MachineBase automaton)
            : base(automaton, automaton)
        {
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        public virtual void Login()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Searches the rival.
        /// </summary>
        /// <returns></returns>
        public virtual bool DoFight()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Updates the player status.
        /// </summary>
        public virtual void UpdatePlayerStatus()
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Searches the crystal.
        /// </summary>
        public virtual void SearchCrystal()
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Logins this instance.
        /// </summary>
        public virtual bool DoTraning()
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Sleeps the specified interval.
        /// </summary>
        /// <param name="interval">The interval.</param>
        /// <param name="canDisguise">if set to <c>true</c> [can disguise].</param>
        public void Sleep(TimeSpan interval, bool canDisguise)
        {
            Sleep(DateTime.Now + interval, canDisguise);
        }

        /// <summary>
        /// Sleeps the specified span.
        /// </summary>
        /// <param name="until">The until.</param>
        /// <param name="canDisguise">if set to <c>true</c> [can disguise].</param>
        public virtual void Sleep(DateTime until, bool canDisguise)
        {
            if (until <= DateTime.Now) return;

            CastEvent(Event.Sleep);
            Thread.Sleep(until - DateTime.Now);
            CastEvent(Event.Awake);
        }

        /// <summary>
        /// Tries the go to patrol.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns></returns>
        public virtual DateTime? TryGoToPatrol(int minutes)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Does the farm.
        /// </summary>
        public virtual void DoFarm()
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Needs the money.
        /// </summary>
        public virtual void NeedMoney()
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public void Logout()
        {
            Controller.Logout();
            eventSink.CastEvent(Event.Logout);
        }







    }
}