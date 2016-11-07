using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Savchin.EventSpy.Core
{
    class EventListenerManager
    {
        readonly Dictionary<Control, Dictionary<string, EventListener>> eventMaps = new Dictionary<Control, Dictionary<string, EventListener>>();

        /// <summary>
        /// Determines whether the specified control has listener.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <returns>
        /// 	<c>true</c> if the specified control has listener; otherwise, <c>false</c>.
        /// </returns>
        public bool HasListener(Control control, string eventName)
        {
            Dictionary<string, EventListener> eventMap;
            if (!eventMaps.TryGetValue(control, out eventMap))
            {
                return false;
            }
            return eventMap.ContainsKey(eventName);

        }

        /// <summary>
        /// Gets the listener.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="eventName">Name of the event.</param>
        public EventListener GetOrCreateListener(Control control, string eventName)
        {
            Dictionary<string, EventListener> eventMap;
            EventListener result;
            if (!eventMaps.TryGetValue(control, out eventMap))
            {
                eventMap = new Dictionary<string, EventListener>();
                result = new EventListener(control, eventName);
                eventMap.Add(eventName, result);
                eventMaps.Add(control, eventMap);
                return result;
            }

            if (!eventMap.TryGetValue(eventName, out result))
            {
                result = new EventListener(control, eventName);
                eventMap.Add(eventName, result);
            }
            return result;
        }

        internal void RemoveListener(Control control, string eventName)
        {
            Dictionary<string, EventListener> eventMap;
            EventListener result;
            if (!eventMaps.TryGetValue(control, out eventMap))
            {
                return;
            }

            if (eventMap.ContainsKey(eventName))
            {
                eventMap.Remove(eventName);
            }
          
        }


    }
}
