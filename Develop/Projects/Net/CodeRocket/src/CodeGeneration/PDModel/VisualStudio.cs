using System;
using System.IO;
using EnvDTE;
using EnvDTE80;
using EnvDTE90;
using Microsoft.VisualBasic;
using Constants = EnvDTE.Constants;

namespace Savchin.CodeGeneration
{
    public class VisualStudio : IDisposable
    {



        /// <summary>
        /// Version
        /// </summary>
        public enum Version
        {
            /// <summary>
            /// v80
            /// </summary>
            v80,
            /// <summary>
            /// v90
            /// </summary>
            v90
        }

        #region Properties
        private readonly Version _studioVersion;
        private DTE2 _DTE;
        private readonly string _solutionFile;


        /// <summary>
        /// Gets the type of the studio.
        /// </summary>
        /// <value>The type of the studio.</value>
        private Type StudioType
        {
            get
            {
                return Type.GetTypeFromProgID(StudioProgID);
            }
        }
        /// <summary>
        /// Gets the studio prog ID.
        /// </summary>
        /// <value>The studio prog ID.</value>
        private string StudioProgID
        {
            get { return _studioVersion == Version.v80 ? "VisualStudio.DTE.8.0" : "VisualStudio.DTE.9.0"; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualStudio"/> class.
        /// </summary>
        /// <param name="version">The version.</param>
        public VisualStudio(Version version, string solutionFile)
        {
            _studioVersion = version;
            _solutionFile = solutionFile;
        }
        /// <summary>
        /// Intializes the DTE.
        /// </summary>
        private void IntializeDTE()
        {
            _DTE = (DTE2)Activator.CreateInstance(StudioType);


        }

        /// <summary>
        /// Checks the visual studio solution.
        /// </summary>
        public void CheckVisualStudioSolution()
        {
            if (_DTE == null)
            {
                IntializeDTE();
            }


            if (!(File.Exists(_solutionFile)))
                throw new CodeGenerationException("Solution not found: " + _solutionFile);


            if (_DTE.Solution.IsOpen == false)
            {
                _DTE.Solution.Open(_solutionFile);
            }

            if (string.Compare(_DTE.Solution.FullName, _solutionFile) != 0)
                throw new CodeGenerationException("Incotrrect solution");

            _DTE.MainWindow.Activate();
            _DTE.MainWindow.Visible = true;
        }
        /// <summary>
        /// Adds the file to solution.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="solutionPath">The solution path.</param>
        public void AddFileToSolution(string filePath, string solutionPath)
        {
            _DTE.Windows.Item(Constants.vsWindowKindSolutionExplorer).Activate();
            _DTE.ToolWindows.SolutionExplorer.GetItem(solutionPath).Select(vsUISelectionType.vsUISelectionTypeSelect);
            _DTE.ItemOperations.AddExistingItem(filePath);
        }

        /// <summary>
        /// Saves the solution.
        /// </summary>
        public void SaveSolution()
        {
            _DTE.Solution.Saved = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_DTE != null)
            {
                _DTE.Solution.Close(true);
                _DTE.Quit();
                _DTE = null;
            }
        }
    }
}
