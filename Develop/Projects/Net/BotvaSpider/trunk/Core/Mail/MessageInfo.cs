using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotvaSpider.Core
{
    public enum EventType
    {
        Fight,
        Attack
    }
    public enum EventSeverity
    {
        High,
        Normal,
        Low
    }
    public class MessageEventArgs : EventArgs
    {
        public MessageInfo Message { get; private  set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public MessageEventArgs(MessageInfo message)
        {
            Message = message;
        }
    }
    public class MessageInfo : IEquatable<MessageInfo>
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        public EventType EventType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is win.
        /// </summary>
        /// <value><c>true</c> if this instance is win; otherwise, <c>false</c>.</value>
        public bool IsWin { get; set; }
        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>The severity.</value>
        public EventSeverity Severity { get; set; }


        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(MessageInfo other)
        {
            return Date == other.Date && EventType == other.EventType && Title == other.Title;
        }
    }
}
