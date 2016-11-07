using System;

namespace BotvaSpider.Automation
{
    public abstract class StateBase<AI>
        where AI : class
    {

        protected readonly AI automaton;
        protected readonly IEventSink eventSink;




        /// <summary>
        /// Initializes a new instance of the <see cref="StateBase&lt;AI&gt;"/> class.
        /// </summary>
        /// <param name="automaton">The automaton.</param>
        /// <param name="eventSink">The event sink.</param>
        protected StateBase(AI automaton, IEventSink eventSink)
        {
            if (automaton == null || eventSink == null)
            {
                throw new NullReferenceException();
            }
            this.automaton = automaton;
            this.eventSink = eventSink;
    
        }

        protected void CastEvent(Event e)
        {
            eventSink.CastEvent(e);
        }
    }
}