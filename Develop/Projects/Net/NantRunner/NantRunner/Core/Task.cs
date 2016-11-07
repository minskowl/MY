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
    public class Task
    {
        private string name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute()]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string filePath;
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path.</value>
        [XmlAttribute()]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        private string iconPath;
        /// <summary>
        /// Gets or sets the icon path.
        /// </summary>
        /// <value>The icon path.</value>
        [XmlAttribute()]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string IconPath
        {
            get { return iconPath; }
            set { iconPath = value; }
        }
        private string targetName;
        /// <summary>
        /// Gets or sets the name of the target.
        /// </summary>
        /// <value>The name of the target.</value>
        [XmlAttribute()]
        public string TargetName
        {
            get { return targetName; }
            set { targetName = value; }
        }




    }
}
