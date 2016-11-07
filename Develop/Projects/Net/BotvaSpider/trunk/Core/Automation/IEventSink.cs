namespace BotvaSpider.Automation
{
    public interface IEventSink
    {

        /// <summary>
        /// Casts the event.
        /// </summary>
        /// <param name="e">The e.</param>
        void CastEvent(Event e);

    }
}