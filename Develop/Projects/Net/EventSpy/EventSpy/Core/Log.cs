using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.EventSpy.Core
{
    public enum Logger
    {
        Output,
        Test
    }
    public interface ILog
    {
        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        void AddMessage(Logger logger, string message);
    }
}
