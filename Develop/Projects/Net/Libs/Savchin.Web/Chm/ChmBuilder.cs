using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace Savchin.Web.Chm
{
    /// <summary>
    /// ChmBuilder
    /// </summary>
    public class ChmBuilder
    {
        private string hhcCompilerPath;

        public string HhcCompilerPath
        {
            get { return hhcCompilerPath; }
            set { hhcCompilerPath = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChmBuilder"/> class.
        /// </summary>
        public ChmBuilder()
        {
            string value = (string)Registry.GetValue(@"HKEY_CLASSES_ROOT\CLSID\{76313CF3-787F-11D0-A1F0-0800361A1803}\InprocServer32", null,
                               null);
            if (!string.IsNullOrEmpty(value))
                hhcCompilerPath = Path.GetDirectoryName(value);
        }

        /// <summary>
        /// Builds the specified project.
        /// </summary>
        /// <param name="project">The project.</param>
        public void Build(ChmProject project)
        {
            project.Save();

            Build(project.FilePath);
        }

        /// <summary>
        /// Builds the specified project file path.
        /// </summary>
        /// <param name="projectFilePath">The project file path.</param>
        public void Build(string projectFilePath)
        {
            Process.Start(Path.Combine(HhcCompilerPath, "hhc.exe"), projectFilePath);
        }
    }
}
