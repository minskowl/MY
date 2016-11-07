﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using ICSharpCode.SharpDevelop.Project;
using Savchin.CodeEditor.GUI;

namespace Savchin.Project
{
    /// <summary>
    /// Parameter object for loading an existing project.
    /// </summary>
    public class ProjectLoadInformation
    {
        public Solution ParentSolution { get; private set; }
        public string FileName { get; private set; }
        public string Platform { get; internal set; }
        public string ProjectName { get; private set; }
        public string TypeGuid { get; set; }
        public IList<ProjectSection> ProjectSections { get; set; }
        internal string Guid { get; set; }

        internal bool? upgradeToolsVersion;

        IProgressMonitor progressMonitor = new DummyProgressMonitor();

        /// <summary>
        /// Gets/Sets the progress monitor used during the load.
        /// This property never returns null.
        /// </summary>
        public IProgressMonitor ProgressMonitor
        {
            get { return progressMonitor; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                progressMonitor = value;
            }
        }

        public ProjectLoadInformation(Solution parentSolution, string fileName, string projectName)
        {
            if (parentSolution == null)
                throw new ArgumentNullException("parentSolution");
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            if (projectName == null)
                throw new ArgumentNullException("projectName");
            this.ParentSolution = parentSolution;
            this.FileName = fileName;
            this.ProjectName = projectName;
        }
    }
}
