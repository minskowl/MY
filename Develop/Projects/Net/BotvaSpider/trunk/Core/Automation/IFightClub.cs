using System;

namespace BotvaSpider.Automation
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFightClub
    {
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        MachineState State { get; }
        /// <summary>
        /// Logins this instance.
        /// </summary>
        void Login();
        /// <summary>
        /// Logouts this instance.
        /// </summary>
        void Logout();


        /// <summary>
        /// Does the fight.
        /// </summary>
        /// <returns></returns>
        bool DoFight();

        /// <summary>
        /// Does the traning.
        /// </summary>
        /// <returns></returns>
        bool DoTraning();

        /// <summary>
        /// Updates the player status.
        /// </summary>
        void UpdatePlayerStatus();
      
        /// <summary>
        /// Searches the crystal.
        /// </summary>
        void SearchCrystal();
        /// <summary>
        /// Tries the go to patrol.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns>sleep until time</returns>
        DateTime? TryGoToPatrol(int minutes);

        /// <summary>
        /// Does the farm.
        /// </summary>
        void DoFarm();

        /// <summary>
        /// Needs the money.
        /// </summary>
        void NeedMoney();

        /// <summary>
        /// Sleeps the specified span.
        /// </summary>
        /// <param name="until">The until.</param>
        /// <param name="canDisguise">if set to <c>true</c> [can disguise].</param>
        void Sleep(DateTime until, bool canDisguise);
    }
}