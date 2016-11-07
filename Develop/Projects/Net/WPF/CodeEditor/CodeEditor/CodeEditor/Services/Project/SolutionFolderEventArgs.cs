// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using Savchin.Project;

namespace Savchin.CodeEditor.Services.Project
{
    public delegate void SolutionFolderEventHandler(object sender, SolutionFolderEventArgs e);

    public class SolutionFolderEventArgs : EventArgs
    {
        ISolutionFolder solutionFolder;

        public ISolutionFolder SolutionFolder
        {
            get
            {
                return solutionFolder;
            }
        }

        public SolutionFolderEventArgs(ISolutionFolder solutionFolder)
        {
            this.solutionFolder = solutionFolder;
        }

    }
}
