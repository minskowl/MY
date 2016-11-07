using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotvaSpider.Automation
{
    public abstract class AutomatonBase<AI> : IEventSink 
    {
        protected Event CurrentEvent{ get; private set;}
        protected AI state;
        private readonly Dictionary<AI, Dictionary<Action, AI>> edges = new Dictionary<AI, Dictionary<Action, AI>>();

        /// <summary>
        /// Adds the edge.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The e.</param>
        /// <param name="target">The target.</param>
        protected void AddEdge(AI source, Action e, AI target)
        {
            Dictionary<Action, AI> row;
            if (edges.ContainsKey(source))
            {
                row = edges[source];
            }
            else
            {
                row = new Dictionary<Action, AI>();
                edges.Add(source, row);
            }

            row.Add(e, target);
        }

        /// <summary>
        /// Casts the event.
        /// </summary>
        /// <param name="e">The e.</param>
        public virtual void CastEvent(Event e)
        {
            try
            {
                state = edges[state][e.Action];
                CurrentEvent = e;
            }
            catch (KeyNotFoundException ex)
            {
                throw new IllegalStateException(String.Format("Edge is not defined  from State {0} by event {1}", state, e.Action.ToString()), ex);
            }
        }
    }
}