using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Savchin.Web.Chm
{
    public class IndexKey : Node
    {
        private readonly NodeCollection subKeys = new NodeCollection();

        /// <summary>
        /// Gets the sub keys.
        /// </summary>
        /// <value>The sub keys.</value>
        public NodeCollection SubKeys
        {
            get { return subKeys; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadingNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="location">The location.</param>
        public IndexKey(string name, string location)
            : base(name, location)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadingNode"/> class.
        /// </summary>
        public IndexKey()
        {
        }



        /// <summary>
        /// Writes the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="basePath">The base path.</param>
        public override void Write(TextWriter writer, string basePath)
        {
            writer.Write(@"<LI>");
            base.Write(writer, basePath);
            subKeys.Write(writer, basePath);
        }
    }
}
