using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeBase.BussinesLayer.Core
{
    /// <summary>
    /// LoggerAttribute
    /// </summary>
    public class LoggerAttribute : Attribute
    {
        private Severity severity;

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>The severity.</value>
        public Severity Severity
        {
            get { return severity; }
            set { severity = value; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerAttribute"/> class.
        /// </summary>
        /// <param name="severity">The severity.</param>
        public LoggerAttribute(Severity severity)
        {
            this.severity = severity;
        }
    }
}
