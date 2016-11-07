﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)


using CSharpBinding;
using Savchin.CodeEditor.Services;
using Savchin.Project;

namespace Savchin.CodeEditor.CSharpBinding
{
    public class CSharpProjectBinding : IProjectBinding
    {
        public const string LanguageName = "C#";

        public string Language
        {
            get
            {
                return LanguageName;
            }
        }

        public IProject LoadProject(ProjectLoadInformation loadInformation)
        {
            return new CSharpProject(loadInformation);
        }

        public IProject CreateProject(ProjectCreateInformation info)
        {
            return new CSharpProject(info);
        }

        public bool HandlingMissingProject
        {
            get
            {
                return false;
            }
        }
    }
}
