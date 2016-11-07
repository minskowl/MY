using System.IO;
using System.Web;
using Savchin.IO;

namespace Savchin.Web.Chm
{
    /// <summary>
    /// Node
    /// </summary>
    public abstract class Node
    {
        private string name;
        private string location;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="location">The location.</param>
        protected Node(string name, string location)
        {
            this.name = name;
            this.location = location;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        protected Node()
        {
        }

        /// <summary>
        /// Writes the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="basePath">The base path.</param>
        public virtual void Write(TextWriter writer, string basePath)
        {
            writer.Write(string.Format(@"<OBJECT type=""text/sitemap"">	
        <param name=""Name"" value=""{0}"">
		<param name=""Local"" value=""{1}"">
        </OBJECT>",
 HttpUtility.HtmlEncode(name),
 PathHelper.GetRealitive(basePath, location)));
        }
    }
}
