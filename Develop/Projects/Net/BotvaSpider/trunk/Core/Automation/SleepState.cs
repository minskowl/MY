namespace BotvaSpider.Automation
{
    class SleepState : MachineStateBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SleepState"/> class.
        /// </summary>
        /// <param name="automaton">The automaton.</param>
        public SleepState(MachineBase automaton)
            : base(automaton)
        {
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public override MachineState State
        {
            get { return MachineState.Sleep; }
        }




    }
}