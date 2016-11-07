using System.IO;

namespace Savchin.Web.Chm
{
    public class HeadingNode : Node
    {
        private readonly NodeCollection childNodes = new NodeCollection();
        /// <summary>
        /// Gets the child nodes.
        /// </summary>
        /// <value>The child nodes.</value>
        public NodeCollection ChildNodes
        {
            get { return childNodes; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadingNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="location">The location.</param>
        public HeadingNode(string name, string location)
            : base(name, location)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadingNode"/> class.
        /// </summary>
        public HeadingNode()
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
            childNodes.Write(writer, basePath);
        }
    }
}
