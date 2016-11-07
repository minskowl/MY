using System;
using System.ComponentModel;

namespace BotvaSpider.Automation
{
 

    public class MachineStateEventArgs : EventArgs
    {
        private readonly MachineState state;
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public MachineState State
        {
            get { return state; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MachineStateEventArgs"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        public MachineStateEventArgs(MachineState state)
        {
            this.state = state;
        }


    }
}