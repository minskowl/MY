namespace BotvaSpider.Automation
{
    class ErrorState : MachineStateBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorState"/> class.
        /// </summary>
        /// <param name="automaton">The automaton.</param>
        public ErrorState(MachineBase automaton)
            : base(automaton)
        {
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public override MachineState State
        {
            get { return MachineState.Error; }
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        public override void Login()
        {
            if (Controller.Login())
                eventSink.CastEvent(Event.Login);
            else
                eventSink.CastEvent(Event.Error);
        }



  
    }
}