using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Savchin.EventSpy.Core
{
    class EventListener
    {
        private string message;
        private MulticastDelegate handler;
        /// <summary>
        /// Initializes a new instance of the <see cref="EventListener"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="eventName">Name of the event.</param>
        public EventListener(Control control, string eventName)
        {
            message = control.GetType().FullName + " " + control.Name + "." + eventName;
        }

        /// <summary>
        /// Gets the event handler.
        /// </summary>
        /// <value>The event handler.</value>
        public MulticastDelegate Listener
        {
            get { return handler; }
        }


        /// <summary>
        /// Createdelegates this instance.
        /// </summary>
        /// <returns></returns>
        public MulticastDelegate CreateDelegate(EventInfo info)
        {
            var handlername = info.EventHandlerType.Name;
            try
            {
                handler = (MulticastDelegate)Delegate.CreateDelegate(
                       info.EventHandlerType,
                       this,
                       handlername);

            }
            catch (Exception)
            {
                EventSpyCore.Output.AddMessage(Logger.Output, "Need implemet " + info.EventHandlerType.Name);
            }
            return handler;
        }

        #region Handlers
        /// <summary>
        /// Handlers the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void EventHandler(object sender, EventArgs args)
        {
            EventSpyCore.Output.AddMessage(Logger.Output, message);
        }
        /// <summary>
        /// Forms the closing event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        public void FormClosingEventHandler(object sender, FormClosingEventArgs args)
        {
            EventSpyCore.Output.AddMessage(Logger.Output, message);
        }

        /// <summary>
        /// Forms the closed event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.Forms.FormClosedEventArgs"/> instance containing the event data.</param>
        public void FormClosedEventHandler(object sender, FormClosedEventArgs args)
        {
            EventSpyCore.Output.AddMessage(Logger.Output, message);
        }
        #endregion
    }
}
