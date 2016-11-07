using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace NantRunner.Core
{
    [Serializable]
    public class Category
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

        private List<Task> tasks = new List<Task>();


        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>The tasks.</value>
        [XmlArrayItem("Task", typeof(Task))]
        public List<Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }
    }
}
