using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Savchin.Forms.Core;

namespace Savchin.CodeGeneration
{
    /// <summary>
    /// Application settings class
    /// </summary>
    [Serializable]
    public class Settings : SettingsBase<Settings>
    {
        [XmlArrayItem(ElementName = "Project", Type = typeof(String)), XmlArray()]
        public List<string> RecentProjects;



        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            RecentProjects = new List<string>();
        }

        /// <summary>
        /// Addecents the project.
        /// </summary>
        /// <param name="projectPath">The project path.</param>
        public void AddRecentProject(string projectPath)
        {
            if (!RecentProjects.Contains(projectPath))
                RecentProjects.Add(projectPath);
        }
    }
}
