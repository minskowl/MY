using Savchin.CodeGeneration.Common;
using Savchin.Forms.Browsers;

namespace Savchin.CodeGeneration
{
    public interface IGenerationManager
    {
        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        GenerateProject Project { get; }
        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        IObjectBrowser Browser { get; }

        /// <summary>
        /// Generates the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        void Generate(GenerateMode mode);
        /// <summary>
        /// Generates to out put dir.
        /// </summary>
        void GenerateToOutPutDir();

        /// <summary>
        /// Generates to solution.
        /// </summary>
        void GenerateToSolution();

        /// <summary>
        /// Generates to solution dir.
        /// </summary>
        void GenerateToSolutionDir();


        /// <summary>
        /// Loads the project.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        void LoadProject(string fileName);

        /// <summary>
        /// Loads the project.
        /// </summary>
        /// <param name="project">The project.</param>
        void LoadProject(GenerateProject project);

        /// <summary>
        /// Closes the project.
        /// </summary>
        void CloseProject();

        /// <summary>
        /// Gets or sets the file tabs.
        /// </summary>
        /// <value>The file tabs.</value>
        IFileTabControl FileTabs { get; set; }
        /// <summary>
        /// Gets or sets the error viewer.
        /// </summary>
        /// <value>The error viewer.</value>
        IErrorViewer ErrorViewer { get; set; }
    }
}