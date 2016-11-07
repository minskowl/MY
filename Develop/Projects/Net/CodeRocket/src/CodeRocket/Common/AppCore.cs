using System;
using System.IO;
using System.Windows.Forms;
using Savchin.CodeGeneration;
using Savchin.Data.Schema.Controls;
using Savchin.CodeGeneration.Common;
using Savchin.Controls.Browsers;
using Savchin.Forms.Browsers;

namespace CodeRocket.Common
{
    /// <summary>
    /// AppCore class 
    /// </summary>
    class AppCore 
    {
        static readonly AppCore current = new AppCore();

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>The current.</value>
        public static AppCore Current
        {
            get { return current; }
        }

        #region Properties

        private readonly string fileFilter;
        private readonly OpenFileDialog openFileDialog = new OpenFileDialog();

        /// <summary>
        /// Gets the open file dialog.
        /// </summary>
        /// <value>The open file dialog.</value>
        public OpenFileDialog OpenFileDialog
        {
            get { return openFileDialog; }
        }
        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        public IObjectBrowser Browser
        {
            get { return current.Browser; }
        }
        private readonly SaveFileDialog saveFileDialog = new SaveFileDialog();
        /// <summary>
        /// Gets the save file dialog.
        /// </summary>
        /// <value>The save file dialog.</value>
        public SaveFileDialog SaveFileDialog
        {
            get { return saveFileDialog; }
        }


        private Settings currentSettings;
        /// <summary>
        /// Gets the current settings.
        /// </summary>
        /// <value>The current settings.</value>
        public Settings CurrentSettings
        {
            get { return currentSettings; }
        }

        private IFormMain _form;
        readonly PDGenerationManager _pdManager = new PDGenerationManager();
        readonly AssemblyGenerationManager _assemblyManager = new AssemblyGenerationManager();
        readonly SchemaGenerationManager _schemaManager = new SchemaGenerationManager();

        private IGenerationManager _activeManager;
        private PropertyGrid _propertyGrid;
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="AppCore"/> class.
        /// </summary>
        private AppCore()
        {
            fileFilter = "Generation projects|*" + GenerateProject.FileExtension + "|XML|*.xml|ALL|*.*";
            openFileDialog.Filter = fileFilter;
            saveFileDialog.FileName = fileFilter;
            currentSettings = new Settings();
            try
            {
                currentSettings = Settings.Load();
            }
            catch (FileNotFoundException)
            {
                currentSettings = new Settings();
            }
        }

        /// <summary>
        /// Initializes manager.
        /// </summary>
        /// <param name="pdBrowser">The pd browser.</param>
        /// <param name="assemblyBrowser">The assembly browser.</param>
        /// <param name="schemaBrowser">The schema browser.</param>
        /// <param name="projectBrowser">The project browser.</param>
        /// <param name="fileTabControl">The file tab control.</param>
        /// <param name="propertyGrid">The property grid.</param>
        /// <param name="errorViewer">The error viewer.</param>
        public void Initialize(IFormMain form,
                               PDBrowser pdBrowser,
                               AssemblyBrowser assemblyBrowser,
                               SchemaBrowser schemaBrowser,
                               GeneratorProjectBrowser projectBrowser,
                               IFileTabControl fileTabControl,
                               PropertyGrid propertyGrid,
                                IErrorViewer errorViewer
                              )
        {
            _form = form;
            _pdManager.Browser = pdBrowser;
            _pdManager.FileTabs = fileTabControl;
            _pdManager.ProjectBrowser = projectBrowser;
            _pdManager.ErrorViewer = errorViewer;

            _assemblyManager.Browser = assemblyBrowser;
            _assemblyManager.FileTabs = fileTabControl;
            _assemblyManager.ProjectBrowser = projectBrowser;
            _assemblyManager.ErrorViewer = errorViewer;

            _schemaManager.Browser = schemaBrowser;
            _schemaManager.FileTabs = fileTabControl;
            _schemaManager.ProjectBrowser = projectBrowser;
            _schemaManager.ErrorViewer = errorViewer;

            if (propertyGrid != null)
            {
                _propertyGrid = propertyGrid;
                projectBrowser.AfterSelect += new EventHandler(Browsers_AfterSelect);
                schemaBrowser.AfterSelect += new EventHandler(Browsers_AfterSelect);
                assemblyBrowser.AfterSelect += new EventHandler(Browsers_AfterSelect);
            }
        }
        /// <summary>
        /// Closes the project.
        /// </summary>
        public void CloseProject()
        {
            if (_activeManager != null)
            {
                _activeManager.CloseProject();
            }

        }
        public void Unload()
        {
            CloseProject();
            currentSettings.Save();
        }

        void Browsers_AfterSelect(object sender, EventArgs e)
        {
            IObjectBrowser browser = (IObjectBrowser)sender;
            _propertyGrid.SelectedObject = browser.SelectedObject;

        }

        /// <summary>
        /// News the project.
        /// </summary>
        /// <param name="type">The type.</param>
        public GenerateProject NewProject(ProjectType type)
        {
            GenerateProject project = new GenerateProject();
            project.ProjectType = type;
            LoadProject(project);
            return project;
        }

        #region IGenerationManager
        /// <summary>
        /// Loads the project.
        /// </summary>
        /// <param name="projectFileName">Name of the project file.</param>
        public void LoadProject(String projectFileName)
        {
            LoadProject(GenerateProject.Load(projectFileName));
            currentSettings.AddRecentProject(projectFileName);
        }

        /// <summary>
        /// Loads the project.
        /// </summary>
        /// <param name="project">The project.</param>
        private void LoadProject(GenerateProject project)
        {
            CloseProject();

            switch (project.ProjectType)
            {
                case ProjectType.Assembly:
                    _assemblyManager.LoadProject(project);
                    _activeManager = _assemblyManager;
                    break;
                case ProjectType.PD:
                    _pdManager.LoadProject(project);
                    _activeManager = _pdManager;
                    break;
                case ProjectType.Schema:
                    _schemaManager.LoadProject(project);
                    _activeManager = _schemaManager;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }



        public IFileTabControl FileTabs
        {
            get
            {
                if (ActiveManager != null)
                    return ActiveManager.FileTabs;
                else
                    return null;
            }
            set
            {
                if (ActiveManager != null)
                    ActiveManager.FileTabs = value;
            }
        }

        public IErrorViewer ErrorViewer
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        public GenerateProject Project
        {
            get
            {
                if (ActiveManager != null)
                    return ActiveManager.Project;
                return null;
            }
        }

        /// <summary>
        /// Gets the form.
        /// </summary>
        /// <value>The form.</value>
        public IFormMain Form
        {
            get { return _form; }
        }

        /// <summary>
        /// Gets or sets the active manager.
        /// </summary>
        /// <value>The active manager.</value>
        public IGenerationManager ActiveManager
        {
            get { return _activeManager; }
        }


   


        /// <summary>
        /// Generates the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        public void Generate(GenerateMode mode)
        {
            if (ActiveManager != null)
            {
                ActiveManager.Generate(mode);
            }
        }






  


        #endregion


    }
}
