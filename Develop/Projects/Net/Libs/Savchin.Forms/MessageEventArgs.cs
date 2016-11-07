using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Savchin.Forms
{
    /// <summary>
    /// MessageDelegate
    /// </summary>
    public delegate void MessageDelegate(object sender, MessageEventArgs e);

    public class MessageEventArgs : EventArgs
    {
        private Message message;

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public Message Message
        {
            get { return message; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public MessageEventArgs(Message message)
        {
            this.message = message;
        }
    }
}