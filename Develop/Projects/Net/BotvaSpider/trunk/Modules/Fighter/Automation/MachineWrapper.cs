using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Logging;
using WatiN.Core;

namespace BotvaSpider.Automation.Fights
{
    internal class MachineWrapper
    {
        private  readonly FightMachine _machine;



        /// <summary>
        /// Gets the log.
        /// </summary>
        /// <value>The log.</value>
        public Logger Log
        {
            get { return Machine.Log; }
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <value>The controller.</value>
        public GameController Controller
        {
            get { return Machine.Controller; }
        }

        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        public IE Browser
        {
            get { return Controller.Browser; }
        }

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value>The player.</value>
        public Player Player
        {
            get { return Machine.Player; }
        }

        /// <summary>
        /// Gets the machine.
        /// </summary>
        /// <value>The machine.</value>
        public FightMachine Machine
        {
            get { return _machine; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MachineWrapper"/> class.
        /// </summary>
        /// <param name="machine">The machine.</param>
        public MachineWrapper(FightMachine machine)
        {
            _machine = machine;
        }
    }
}