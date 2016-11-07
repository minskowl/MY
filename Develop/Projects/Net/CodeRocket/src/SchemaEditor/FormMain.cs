using System;
using System.IO;
using System.Windows.Forms;
using Savchin.Controls.Docking;
using Savchin.Core;
using Savchin.Data.Schema.Controls;
using SchemaEditor.Commands;
using SchemaEditor.Commands.File;
using SchemaEditor.Commands.Edit;
using SchemaEditor.Controls;
using SchemaEditor.Core;

namespace SchemaEditor
{
    public partial class FormMain : Form, IFormMain
    {

        #region Fileds
        private readonly string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
        
        private readonly PropertyConsole m_propertyConsole = new PropertyConsole();
        private readonly SchemaConsole m_schemaConsole = new SchemaConsole();
        private readonly ErrorConsole m_errorConsole = new ErrorConsole();

        private readonly OpenSchemaCommand openSchemaCommand = new OpenSchemaCommand();
        private readonly CloseSchemaCommand closeSchemaCommand = new CloseSchemaCommand();
        private readonly NewSchemaCommand newSchemaCommand = new NewSchemaCommand();
        private readonly SaveAsSchemaCommand saveAsSchemaCommand = new SaveAsSchemaCommand();
        private readonly SaveSchemaCommand saveSchemaCommand = new SaveSchemaCommand();

        private readonly ExitCommand exitCommand = new ExitCommand();
        private readonly ReverseEngeniringCommand reverseEngeniringCommand = new ReverseEngeniringCommand();
        private readonly ShowConsoleCommand showConsoleCommand = new ShowConsoleCommand();

        #endregion

        #region Properties


        /// <summary>
        /// Gets the schema browser.
        /// </summary>
        /// <value>The schema browser.</value>
        public SchemaBrowser SchemaBrowser
        {
            get { return m_schemaConsole.Browser; }
        }
        /// <summary>
        /// Gets the exception viewer.
        /// </summary>
        /// <value>The exception viewer.</value>
        public ExceptionViewer ExceptionViewer
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

        /// <summary>
        /// Adds the control.
        /// </summary>
        /// <param name="control">The control.</param>
        public void AddControl(Control control)
        {
            dockPanel.Controls.Add(control);
        }

        #endregion


        #region Initialize
        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        public FormMain()
        {
            Log.SchemaEditor.Debug("Schema Editor Start");

            IsMdiContainer = true;
            InitializeComponent();
            InitializeCommands();
            m_schemaConsole.Browser.NodeMouseClick += new TreeNodeMouseClickEventHandler(Browser_NodeMouseClick);


        }

        private void InitializeCommands()
        {
            openToolStripMenuItem.BindCommand(openSchemaCommand);
            closeToolStripMenuItem.BindCommand(closeSchemaCommand);
            newToolStripMenuItem.BindCommand(newSchemaCommand);
            saveAsToolStripMenuItem.BindCommand(saveAsSchemaCommand);
            saveToolStripMenuItem.BindCommand(saveSchemaCommand);

            exitToolStripMenuItem.BindCommand(exitCommand);
            reverseEgeToolStripMenuItem.BindCommand(reverseEngeniringCommand);

            propertyToolStripMenuItem.Tag = m_propertyConsole;
            propertyToolStripMenuItem.BindCommand(showConsoleCommand);

            errorsToolStripMenuItem.Tag = m_errorConsole;
            errorsToolStripMenuItem.BindCommand(showConsoleCommand);

            schemaToolStripMenuItem.Tag = m_schemaConsole;
            schemaToolStripMenuItem.BindCommand(showConsoleCommand);

            CommandManager.TrackCommands(menuMain);
        }


        private void FormMain1_Load(object sender, EventArgs e)
        {
            AppCore.Initialize(this);
            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, GetContentFromPersistString);
        }

        private void FormMain1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommandManager.ReleaseTracking(menuMain);

            dockPanel.SaveAsXml(configFile);

            Log.SchemaEditor.Debug("Schema Editor End");
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
                return m_propertyConsole;

            if (persistString == typeof(SchemaConsole).ToString())
                return m_schemaConsole;

            if (persistString == typeof(ErrorConsole).ToString())
                return m_errorConsole;


            return null;
        }


        void Browser_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            m_propertyConsole.Browser.SelectedObject = e.Node.Tag;
        }
    }
}
