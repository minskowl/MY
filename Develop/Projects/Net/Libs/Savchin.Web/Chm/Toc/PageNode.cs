using System.IO;

namespace Savchin.Web.Chm
{
    public class PageNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="location">The location.</param>
        public PageNode(string name, string location)
            : base(name, location)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PageNode"/> class.
        /// </summary>
        public PageNode()
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
            writer.WriteLine(@"</LI>");

        }
    }
}
