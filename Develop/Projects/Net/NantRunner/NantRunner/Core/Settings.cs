using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms.Design;
using System.Xml.Serialization;

namespace NantRunner.Core
{
    [Serializable]
    public class Settings
    {
        private string nantPath;
        /// <summary>
        /// Gets or sets the nant path.
        /// </summary>
        /// <value>The nant path.</value>
        [Editor(typeof(FolderNameEditor),typeof(UITypeEditor))]
        public string NantPath
        {
            get { return nantPath; }
            set { nantPath = value; }
        }

        private List<Category> categories = new List<Category>();

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        [XmlArrayItem("Category", typeof(Category))]
        public List<Category> Categories
        {
            get { return categories; }
            set { categories = value; }
        }


    }
}
