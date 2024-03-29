﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Project;
using Savchin.CodeEditor.Services.Parse;
using Savchin.CodeEditor.Services.Project;
using Savchin.CodeEditor.Services.Project.Items;
using Savchin.CodeEditor.Services.ProjectBinding;

namespace Savchin.Project
{
    public enum OutputType
    {
        [Description("${res:Dialog.Options.PrjOptions.Configuration.CompileTarget.Exe}")]
        Exe,
        [Description("${res:Dialog.Options.PrjOptions.Configuration.CompileTarget.WinExe}")]
        WinExe,
        [Description("${res:Dialog.Options.PrjOptions.Configuration.CompileTarget.Library}")]
        Library,
        [Description("${res:Dialog.Options.PrjOptions.Configuration.CompileTarget.Module}")]
        Module
    }

    /// <summary>
    /// A compilable project based on MSBuild.
    /// </summary>
    public abstract class CompilableProject : MSBuildBasedProject, IUpgradableProject
    {
        #region Static methods
        /// <summary>
        /// Gets the file extension of the assembly created when building a project
        /// with the specified output type.
        /// Example: OutputType.Exe => ".exe"
        /// </summary>
        public static string GetExtension(OutputType outputType)
        {
            switch (outputType)
            {
                case OutputType.WinExe:
                case OutputType.Exe:
                    return ".exe";
                case OutputType.Module:
                    return ".netmodule";
                default:
                    return ".dll";
            }
        }
        #endregion

        /// <summary>
        /// A list of project properties that cause reparsing of references when they are changed.
        /// </summary>
        protected readonly ISet<string> reparseReferencesSensitiveProperties = new SortedSet<string>();

        /// <summary>
        /// A list of project properties that cause reparsing of code when they are changed.
        /// </summary>
        protected readonly ISet<string> reparseCodeSensitiveProperties = new SortedSet<string>();

        protected CompilableProject(ProjectCreateInformation information)
            : base(information)
        {
            this.OutputType = OutputType.Exe;
            this.RootNamespace = information.RootNamespace;
            this.AssemblyName = information.ProjectName;

            ClientProfileTargetFramework clientProfile = information.TargetFramework as ClientProfileTargetFramework;
            if (clientProfile != null)
            {
                SetProperty(null, null, "TargetFrameworkVersion", clientProfile.FullFramework.Name, PropertyStorageLocations.Base, true);
                SetProperty(null, null, "TargetFrameworkProfile", "Client", PropertyStorageLocations.Base, true);
            }
            else if (information.TargetFramework != null)
            {
                SetProperty(null, null, "TargetFrameworkVersion", information.TargetFramework.Name, PropertyStorageLocations.Base, true);
            }

            SetProperty("Debug", null, "OutputPath", @"bin\Debug\",
                        PropertyStorageLocations.ConfigurationSpecific, true);
            SetProperty("Release", null, "OutputPath", @"bin\Release\",
                        PropertyStorageLocations.ConfigurationSpecific, true);
            InvalidateConfigurationPlatformNames();

            SetProperty("Debug", null, "DebugSymbols", "True",
                        PropertyStorageLocations.ConfigurationSpecific, true);
            SetProperty("Release", null, "DebugSymbols", "False",
                        PropertyStorageLocations.ConfigurationSpecific, true);

            SetProperty("Debug", null, "DebugType", "Full",
                        PropertyStorageLocations.ConfigurationSpecific, true);
            SetProperty("Release", null, "DebugType", "None",
                        PropertyStorageLocations.ConfigurationSpecific, true);

            SetProperty("Debug", null, "Optimize", "False",
                        PropertyStorageLocations.ConfigurationSpecific, true);
            SetProperty("Release", null, "Optimize", "True",
                        PropertyStorageLocations.ConfigurationSpecific, true);
        }

        protected CompilableProject(ProjectLoadInformation information)
            : base(information)
        {
        }

        /// <summary>
        /// Gets the path where temporary files are written to during compilation.
        /// </summary>
        [Browsable(false)]
        public string IntermediateOutputFullPath
        {
            get
            {
                string outputPath = GetEvaluatedProperty("IntermediateOutputPath");
                if (string.IsNullOrEmpty(outputPath))
                {
                    outputPath = GetEvaluatedProperty("BaseIntermediateOutputPath");
                    if (string.IsNullOrEmpty(outputPath))
                    {
                        outputPath = "obj";
                    }
                    outputPath = Path.Combine(outputPath, this.ActiveConfiguration);
                }
                return Path.Combine(Directory, outputPath);
            }
        }

        /// <summary>
        /// Gets the full path to the xml documentation file generated by the project, or
        /// <c>null</c> if no xml documentation is being generated.
        /// </summary>
        [Browsable(false)]
        public string DocumentationFileFullPath
        {
            get
            {
                string file = GetEvaluatedProperty("DocumentationFile");
                if (string.IsNullOrEmpty(file))
                    return null;
                return Path.Combine(Directory, file);
            }
        }

        // Make Language abstract again to ensure backend-binding implementers don't forget
        // to set it.
        public abstract override string Language
        {
            get;
        }

        public abstract override ICSharpCode.SharpDevelop.Dom.LanguageProperties LanguageProperties
        {
            get;
        }

        [Browsable(false)]
        public string TargetFrameworkVersion
        {
            get { return GetEvaluatedProperty("TargetFrameworkVersion") ?? "v2.0"; }
            set { SetProperty("TargetFrameworkVersion", value); }
        }

        [Browsable(false)]
        public string TargetFrameworkProfile
        {
            get { return GetEvaluatedProperty("TargetFrameworkProfile"); }
            set { SetProperty("TargetFrameworkProfile", value); }
        }

        public override string AssemblyName
        {
            get { return GetEvaluatedProperty("AssemblyName") ?? Name; }
            set { SetProperty("AssemblyName", value); }
        }

        public override string RootNamespace
        {
            get { return GetEvaluatedProperty("RootNamespace") ?? ""; }
            set { SetProperty("RootNamespace", value); }
        }

        /// <summary>
        /// The full path of the assembly generated by the project.
        /// </summary>
        public override string OutputAssemblyFullPath
        {
            get
            {
                string outputPath = GetEvaluatedProperty("OutputPath") ?? "";
                return FileUtility.NormalizePath(Path.Combine(Path.Combine(Directory, outputPath), AssemblyName + GetExtension(OutputType)));
            }
        }

        /// <summary>
        /// The full path of the folder where the project's primary output files go.
        /// </summary>
        public string OutputFullPath
        {
            get
            {
                string outputPath = GetEvaluatedProperty("OutputPath");
                // FileUtility.NormalizePath() cleans up any back references.
                // e.g. C:\windows\system32\..\system becomes C:\windows\system
                return FileUtility.NormalizePath(Path.Combine(Directory, outputPath));
            }
        }

        [Browsable(false)]
        public OutputType OutputType
        {
            get
            {
                try
                {
                    return (OutputType)Enum.Parse(typeof(OutputType), GetEvaluatedProperty("OutputType") ?? "Exe", true);
                }
                catch (ArgumentException)
                {
                    return OutputType.Exe;
                }
            }
            set
            {
                SetProperty("OutputType", value.ToString());
            }
        }

        protected override ParseProjectContent CreateProjectContent()
        {
            ParseProjectContent newProjectContent = new ParseProjectContent(this);
            return newProjectContent;
        }

        protected override void OnActiveConfigurationChanged(EventArgs e)
        {
            base.OnActiveConfigurationChanged(e);
            if (!isLoading)
            {
                ParserService.Reparse(this, true, true);
            }
        }

        protected override void OnActivePlatformChanged(EventArgs e)
        {
            base.OnActivePlatformChanged(e);
            if (!isLoading)
            {
                ParserService.Reparse(this, true, true);
            }
        }

        protected override void OnPropertyChanged(ProjectPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == "TargetFrameworkVersion")
                CreateItemsListFromMSBuild();
            if (!isLoading)
            {
                if (reparseReferencesSensitiveProperties.Contains(e.PropertyName))
                {
                    ParserService.Reparse(this, true, false);
                }
                if (reparseCodeSensitiveProperties.Contains(e.PropertyName))
                {
                    ParserService.Reparse(this, false, true);
                }
            }
        }

        [Browsable(false)]
        public override string TypeGuid
        {
            get
            {
                return ProjectBindingService.GetCodonPerLanguageName(Language).Guid;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public StartAction StartAction
        {
            get
            {
                try
                {
                    return (StartAction)Enum.Parse(typeof(StartAction), GetEvaluatedProperty("StartAction") ?? "Project");
                }
                catch (ArgumentException)
                {
                    return StartAction.Project;
                }
            }
            set
            {
                SetProperty("StartAction", value.ToString());
            }
        }

        protected override ProjectBehavior CreateDefaultBehavior()
        {
            return new DotNetStartBehavior(this, base.CreateDefaultBehavior());
        }

        #region IUpgradableProject
        [Browsable(false)]
        public virtual bool UpgradeDesired
        {
            get
            {
                return MinimumSolutionVersion < Solution.SolutionVersionVS2010;
            }
        }

        public virtual CompilerVersion CurrentCompilerVersion
        {
            get
            {
                switch (MinimumSolutionVersion)
                {
                    case Solution.SolutionVersionVS2005:
                        return CompilerVersion.MSBuild20;
                    case Solution.SolutionVersionVS2008:
                        return CompilerVersion.MSBuild35;
                    case Solution.SolutionVersionVS2010:
                    case Solution.SolutionVersionVS11:
                        return CompilerVersion.MSBuild40;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public virtual TargetFramework CurrentTargetFramework
        {
            get
            {
                string fxVersion = this.TargetFrameworkVersion;
                string fxProfile = this.TargetFrameworkProfile;
                if (string.Equals(fxProfile, "Client", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (ClientProfileTargetFramework fx in TargetFramework.TargetFrameworks.OfType<ClientProfileTargetFramework>())
                        if (fx.FullFramework.Name == fxVersion)
                            return fx;
                }
                else
                {
                    foreach (TargetFramework fx in TargetFramework.TargetFrameworks)
                        if (fx.Name == fxVersion)
                            return fx;
                }
                return null;
            }
        }

        public virtual IEnumerable<CompilerVersion> GetAvailableCompilerVersions()
        {
            return GetOrCreateBehavior().GetAvailableCompilerVersions();
        }

        public virtual void UpgradeProject(CompilerVersion newVersion, TargetFramework newFramework)
        {
            GetOrCreateBehavior().UpgradeProject(newVersion, newFramework);
        }

        public static FileName GetAppConfigFile(IProject project, bool createIfNotExists)
        {
            FileName appConfigFileName = ICSharpCode.Core.FileName.Create(Path.Combine(project.Directory, "app.config"));

            if (!File.Exists(appConfigFileName))
            {
                if (createIfNotExists)
                {
                    File.WriteAllText(appConfigFileName,
                                      "<?xml version=\"1.0\"?>" + Environment.NewLine +
                                      "<configuration>" + Environment.NewLine
                                      + "</configuration>");
                }
                else
                {
                    return null;
                }
            }

            if (!project.IsFileInProject(appConfigFileName))
            {
                FileProjectItem fpi = new FileProjectItem(project, ItemType.None, "app.config");
                ProjectService.AddProjectItem(project, fpi);
                FileService.FireFileCreated(appConfigFileName, false);
                //TODO: Uncomment
                //ProjectBrowserPad.RefreshViewAsync();
            }
            return appConfigFileName;
        }
        #endregion
    }
}
