using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotvaSpider.Core
{


    /// <summary>
    /// TagAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class TagAttribute : Attribute
    {
        private readonly object tag;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagAttribute"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public TagAttribute(object tag)
        {
            this.tag = tag;
        }
        /// <summary>
        /// Gets the prefix.
        /// </summary>
        /// <value>The prefix.</value>
        public object Tag
        {
            get { return tag; }
        }
    }
}
