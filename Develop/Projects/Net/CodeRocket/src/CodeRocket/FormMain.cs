using System;
using System.IO;
using System.Windows.Forms;
using CodeRocket.Commands;
using CodeRocket.Commands.Generate;
using CodeRocket.Commands.Project;
using CodeRocket.Commands.Schema;
using CodeRocket.Common;
using CodeRocket.Controls;
using Savchin.CodeGeneration.Common;
using Savchin.Forms;
using Savchin.Forms.Core.Commands;
using Savchin.Forms.Docking;
using Savchin.Data.Schema.Controls;
using Savchin.CodeGeneration;

namespace CodeRocket
{
    public partial class FormMain : Form, IFormMain, IFileTabControl
    {
        #region Properties
        private readonly string _configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");


        private readonly PropertyConsole _consoleProperty = new PropertyConsole();
        private readonly PowerDesigerConsole m_powerDesigerConsole = new PowerDesigerConsole();
        private readonly AssemblyConsole m_assemblyConsole = new AssemblyConsole();
        private readonly SchemaConsole m_schemaConsole = new SchemaConsole();
        private readonly ErrorConsole m_errorConsole = new ErrorConsole();
        private readonly ProjectConsole m_projectConsole = new ProjectConsole();

        #region Commands


        private readonly ShowConsoleCommand _showConsoleCommand = new ShowConsoleCommand();
        private readonly AboutCommand _aboutCommand = new AboutCommand();
        private readonly CloseAllWindowsCommand _closeAllWindowsCommand = new CloseAllWindowsCommand();

        private readonly SchemaReverseEngineeringCommand _schemaReverseEngineeringCommand = new SchemaReverseEngineeringCommand();
        private readonly SchemaSaveCommand _schemaSaveCommand = new SchemaSaveCommand();
       
        private readonly RecentProjectOpenCommand _recentProjectOpenCommand = new RecentProjectOpenCommand();
        private readonly ProjectOpenCommad _projectOpenCommad = new ProjectOpenCommad();
        private readonly ProjectCloseCommand _projectCloseCommand = new ProjectCloseCommand();
        private readonly ProjectSaveCommand _projectSaveCommand = new ProjectSaveCommand();
        private readonly ProjectSaveAsCommand _projectSaveAsCommand = new ProjectSaveAsCommand();

        private readonly GenerateToOutputCommand _generateToOutputCommand = new GenerateToOutputCommand();
        private readonly GenerateToSolutionCommand _generateToSolutionCommand = new GenerateToSolutionCommand();
        private readonly GenerateToSolutionDirCommand _generateToSolutionDirCommand = new GenerateToSolutionDirCommand();
        private readonly OpenTemplateCommand _openTemplateCommand= new OpenTemplateCommand();

        #endregion

        /// <summary>
        /// Gets the schema browser.
        /// </summary>
        /// <value>The schema browser.</value>
        public SchemaBrowser SchemaBrowser
        {
            get { return m_schemaConsole.Browser; }
        }

        /// <summary>
        /// Gets the project browser.
        /// </summary>
        /// <value>The project browser.</value>
        public GeneratorProjectBrowser ProjectBrowser
        {
            get { return m_projectConsole.Browser; }
        }
        /// <summary>
        /// Gets the error viewer.
        /// </summary>
        /// <value>The error viewer.</value>
        public ErrorViewer ErrorViewer
        {
            get { return m_errorConsole.Browser; }
        }

        /// <summary>
        /// Gets the dock panel.
        /// </summary>
        /// <value>The dock panel.</value>
        public DockPanel DockPanel
        {
            get { return dockPanel; }
        }
        #endregion


        #region Initialize

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        public FormMain()
        {
            Log.CodeRocket.Debug("Code Rokert Start");

            InitializeComponent();
            InitializeCommands();
            InitializeRecentProjectsItem();

            IsMdiContainer = true;

            m_schemaConsole.Browser.ContextMenuStrip = contextMenuShema;
            m_projectConsole.Browser.GenerationDoubleClick += new Savchin.CodeGeneration.GenerationEventHandler(Browser_GenerationDoubleClick);
            AppCore.Current.Initialize(
                this,
                m_powerDesigerConsole.Browser,
                m_assemblyConsole.Browser,
                m_schemaConsole.Browser,
                m_projectConsole.Browser,
                this,
                _consoleProperty.Browser,
                m_errorConsole.Browser);




        }



        private void InitializeRecentProjectsItem()
        {

            foreach (string recentProject in AppCore.Current.CurrentSettings.RecentProjects)
            {
                var menuItem = new ToolStripMenuItem
                                   {
                                       Text = GetShortFileLabel(recentProject),
                                       Tag = recentProject
                                   };
                menuItem.BindCommand(_recentProjectOpenCommand);
                recentProjectsToolStripMenuItem.DropDownItems.Add(menuItem);
            }
        }
        private string GetShortFileLabel(string filePath)
        {
            return (filePath.Length > 40)
                   ?
                       filePath.Substring(0, 3) + "..." +
                       filePath.Substring(filePath.Length - 35, 35)
                   : filePath;
        }

        private void InitializeCommands()
        {
            #region Windows Commands
            assemblyToolStripMenuItem.Tag = m_assemblyConsole;
            assemblyToolStripMenuItem.BindCommand(_showConsoleCommand);

            dataSchemaToolStripMenuItem.Tag = m_schemaConsole;
            dataSchemaToolStripMenuItem.BindCommand(_showConsoleCommand);

            projectToolStripMenuItem.Tag = m_projectConsole;
            projectToolStripMenuItem.BindCommand(_showConsoleCommand);

            propertyToolStripMenuItem.Tag = _consoleProperty;
            propertyToolStripMenuItem.BindCommand(_showConsoleCommand);

            errorsToolStripMenuItem.Tag = m_errorConsole;
            errorsToolStripMenuItem.BindCommand(_showConsoleCommand);

            powerDesignerToolStripMenuItem.Tag = m_powerDesigerConsole;
            powerDesignerToolStripMenuItem.BindCommand(_showConsoleCommand);

            closeAllToolStripMenuItem.BindCommand(_closeAllWindowsCommand);
            #endregion

            aboutToolStripMenuItem.BindCommand(_aboutCommand);

            schemaSaveToolStripMenuItem.BindCommand(_schemaSaveCommand);
            reverseEngeniringToolStripMenuItem.BindCommand(_schemaReverseEngineeringCommand);

            openProjectToolStripMenuItem.BindCommand(_projectOpenCommad);
            closeProjectToolStripMenuItem.BindCommand(_projectCloseCommand);
            saveProjectToolStripMenuItem.BindCommand(_projectSaveCommand);
            saveProjectAsToolStripMenuItem.BindCommand(_projectSaveAsCommand);

            generateToOutputToolStripMenuItem.BindCommand(_generateToOutputCommand);
            generateToSolutionToolStripMenuItem.BindCommand(_generateToSolutionCommand);
            generateToSolutionDirToolStripMenuItem.BindCommand(_generateToSolutionDirCommand);

            CommandManager.TrackCommands(menuMain);

        }

        /// <summary>
        /// Handles the Load event of the FormMain1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FormMain1_Load(object sender, EventArgs e)
        {
            if (File.Exists(_configFile))
                dockPanel.LoadFromXml(_configFile, GetContentFromPersistString);
        }

        /// <summary>
        /// Handles the FormClosing event of the FormMain1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void FormMain1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommandManager.ReleaseTracking(menuMain);

            dockPanel.SaveAsXml(_configFile);
            AppCore.Current.Unload();
            Log.CodeRocket.Debug("Code Rokert End");
        } 
        #endregion

        /// <summary>
        /// Gets the content from persist string.
        /// </summary>
        /// <param name="persistString">The persist string.</param>
        /// <returns></returns>
        private IDockContent GetContentFromPersistString(string persistString)
        {

            if (persistString == typeof(PropertyConsole).ToString())
                return _consoleProperty;

            if (persistString == typeof(PowerDesigerConsole).ToString())
                return m_powerDesigerConsole;

            if (persistString == typeof(AssemblyConsole).ToString())
                return m_assemblyConsole;

            if (persistString == typeof(SchemaConsole).ToString())
                return m_schemaConsole;

            if (persistString == typeof(ErrorConsole).ToString())
                return m_errorConsole;

            if (persistString == typeof(ProjectConsole).ToString())
                return m_projectConsole;


            return null;
        }

        /// <summary>
        /// Handles the GenerationDoubleClick event of the Browser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Savchin.CodeGeneration.Common.GenerationEventArgs"/> instance containing the event data.</param>
        void Browser_GenerationDoubleClick(object sender, GenerationEventArgs e)
        {
            _openTemplateCommand.Execute(ProjectBrowser.Project.GetFullPath(e.Generation.TemplateFile), sender);
        }

        #region Implementation of IFileTabControl

        /// <summary>
        /// Clears the tabs.
        /// </summary>
        void IFileTabControl.ClearTabs()
        {
            foreach (var form in this.MdiChildren)
            {
                form.Close();
            }

        }

        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        void IFileTabControl.AddFile(string filePath)
        {
            var dummyDoc = new FileWindow
                               {
                                   Text = Path.GetFileName(filePath)
                               };
            dummyDoc.Show(dockPanel);
            try
            {
                dummyDoc.FileName = filePath;
            }
            catch (Exception exception)
            {
                dummyDoc.Close();
                MessageBoxEx.Show(this, "Error open file " + filePath, exception.Message);
            }
        }

        /// <summary>
        /// Saves the selected.
        /// </summary>
        void IFileTabControl.SaveSelected()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Acives the editor.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        ICodeEditor IFileTabControl.AciveEditor
        {
            get { throw new System.NotImplementedException(); }
        }

        #endregion
    }
}
