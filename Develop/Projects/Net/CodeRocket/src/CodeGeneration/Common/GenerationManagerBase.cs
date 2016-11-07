using System.IO;
using Savchin.CodeGeneration.Common;
using Savchin.Forms.Browsers;

namespace Savchin.CodeGeneration
{
    public abstract class GenerationManagerBase<GeneratorType, BrowserType> : IGenerationManager
        where GeneratorType : class, IGenerator, new()
        where BrowserType : class, IObjectBrowser
    {
        #region Properties

        private BrowserType browser;
        private IErrorViewer errorViewer;

        protected IFileTabControl fileTabs;
        private GeneratorType generator;

        protected GenerateProject project;

        private GeneratorProjectBrowser projectBrowser;

        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        /// <value>The browser.</value>
        public BrowserType Browser
        {
            get { return browser; }
            set { browser = value; }
        }

        /// <summary>
        /// Gets or sets the project browser.
        /// </summary>
        /// <value>The project browser.</value>
        public GeneratorProjectBrowser ProjectBrowser
        {
            get { return projectBrowser; }
            set { projectBrowser = value; }
        }

        /// <summary>
        /// Gets the generator.
        /// </summary>
        /// <value>The generator.</value>
        public GeneratorType Generator
        {
            get { return generator; }
        }

        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        IObjectBrowser IGenerationManager.Browser
        {
            get { return browser; }
        }

        /// <summary>
        /// Gets or sets the file tabs.
        /// </summary>
        /// <value>The file tabs.</value>
        public IFileTabControl FileTabs
        {
            get { return fileTabs; }
            set { fileTabs = value; }
        }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        /// <value>The project.</value>
        public GenerateProject Project
        {
            get { return project; }
        }

        /// <summary>
        /// Gets or sets the error viewer.
        /// </summary>
        /// <value>The error viewer.</value>
        public IErrorViewer ErrorViewer
        {
            get { return errorViewer; }
            set { errorViewer = value; }
        }

        #endregion

        #region Generate

        /// <summary>
        /// Generates to out put dir.
        /// </summary>
        public void GenerateToOutPutDir()
        {
            Generate(GenerateMode.OutPutDir);
        }

        /// <summary>
        /// Generates to solution.
        /// </summary>
        public void GenerateToSolution()
        {
            Generate(GenerateMode.Solution);
        }

        /// <summary>
        /// Generates to solution dir.
        /// </summary>
        public void GenerateToSolutionDir()
        {
            Generate(GenerateMode.SolutionDir);
        }

        /// <summary>
        /// Generates the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        public virtual void Generate(GenerateMode mode)
        {
            if (errorViewer != null)
                errorViewer.ShowErrors(generator.Errors);
        }

        #endregion

        #region Project operation

        /// <summary>
        /// Saves the project.
        /// </summary>
        public void SaveProject()
        {
            if (project != null)
                project.Save();
        }

        /// <summary>
        /// Saves the project.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void SaveProject(string fileName)
        {
            if (project != null)
                project.Save(fileName);
        }

        #region LoadProject

        /// <summary>
        /// Loads the project.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void LoadProject(string fileName)
        {
            LoadProject(GenerateProject.Load(fileName));
        }

        /// <summary>
        /// Loads the project.
        /// </summary>
        /// <param name="project">The project.</param>
        public void LoadProject(GenerateProject project)
        {
            this.project = project;

            //project.Validate();

            if (projectBrowser != null)
                projectBrowser.ShowProject(project);

            generator = new GeneratorType();
            generator.TemplateGenerated += TemplateGenerated;
            generator.InitalizeFromProject(project);

            if (browser != null && File.Exists(project.SchemaFileFullPath))
            {
                browser.OpenFile(project.SchemaFileFullPath);
            }
            OnProjectLoaded();
        }


        /// <summary>
        /// Called when [project loaded].
        /// </summary>
        protected virtual void OnProjectLoaded()
        {
        }

        #endregion

        #region Close Project

        /// <summary>
        /// Closes the project.
        /// </summary>
        public void CloseProject()
        {
            if (project == null)
                return;

            project = null;
            fileTabs.ClearTabs();
            projectBrowser.Clear();

            if (generator != null)
            {
                generator.TemplateGenerated -= TemplateGenerated;
                generator.Dispose();
                generator = null;
            }
            if (browser != null)
                browser.Clear();

            //OnProjectClosed();
        }

        #endregion

        #endregion

        protected void ClearOutput()
        {
            if (fileTabs != null)
                fileTabs.ClearTabs();
        }

        protected void TemplateGenerated(object sender, TemplateGenerateEventArgs e)
        {
            if (fileTabs != null)
                fileTabs.AddFile(e.FilePath);
        }
    }
}