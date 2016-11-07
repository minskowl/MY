using System;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Savchin.CodeGeneration
{

    [Serializable]
    public class BookMark
    {
        private String _Name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute()]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private String _FullPath;
        /// <summary>
        /// Gets or sets the full path.
        /// </summary>
        /// <value>The full path.</value>
        [XmlAttribute()]
        public string FullPath
        {
            get { return _FullPath; }
            set { _FullPath = value; }
        }

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="BookMark"/> class.
        /// </summary>
        public BookMark()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookMark"/> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="FullPath">The full path.</param>
        public BookMark(String Name, String FullPath)
        {
            this.Name = Name;
            this.FullPath = FullPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookMark"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        public BookMark(TreeNode node)
        {
            Name = node.Text;
            FullPath = node.FullPath;
        } 
        #endregion




    }

}
