//==============================================================================
//	File:		EditorForm.cs
//
//	Namespace:	CustomApplication_CS1
//
//	Classes:	EditorForm
//
//	Purpose:	Provides the application entry point and the implementation for
//				the main UI class.
//
//==============================================================================
// {project info here}
// {copyright info here}
//==============================================================================

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Dundas.Diagramming.EditorComponents.CommandManager;
using Dundas.Diagramming.EditorComponents.ComponentContainer;
using Dundas.Diagramming.EditorComponents.Designer;
using Dundas.Diagramming.Toolbox.SplashScreen;
using Savchin.Controls.Common;
using Savchin.Data.Schema;

namespace ModelDesigner
{
    #region EditorForm

    /// <summary>
    /// The main editor form class provides the main editor UI for the 
    /// application.
    /// </summary>
    [Description("Editor Form class.")]
    public class EditorForm : DundasEditorComponentContainer
    {
        #region Fields

        /// <summary>
        /// The filename of any document to load.
        /// </summary>
        [Description("The filename of any document to load.")]
        private string documentName = null;


        readonly SchemaEditor modelBrowser = new SchemaEditor();
        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Constructor which takes a document filename.
        /// </summary>
        /// <param name="documentName">
        /// The document filename if there is one, else <b>null</b> for new 
        /// document.
        /// </param>
        [Description("Constructor.")]
        public EditorForm(
            string documentName)
        {
            // Remember the document name.
            this.documentName = documentName;
        }

        #endregion // Constructors

        #region Properties

        #endregion // Properties

        #region Indexers

        #endregion // Indexers

        #region Methods

        /// <summary>
        /// Initializes the form for the application.
        /// </summary>
        /// <returns>
        /// Boolean <b>true</b> if successful, else <b>false</b>.
        /// </returns>
        [Description("Initializes the form for the application.")]
        public bool InitializeForm()
        {
            // Required for Windows Form Designer support
            InitializeComponent();

            // Initialize the system.
            Initialize();


            // Add optional components.
            if (!AddBaseEditorComponent(BaseEditorComponents.LibraryManager, null))
            {
                return false;
            }
            if (!AddBaseEditorComponent(BaseEditorComponents.DiagramExplorer, new ComponentLayout(ComponentLayoutDockState.DockedLeft, true, new Size(200, (Height / 2) - 0), DockStyle.Top)))
            {
                return false;
            }
            if (!AddBaseEditorComponent(BaseEditorComponents.PropertyBrowser, new ComponentLayout(ComponentLayoutDockState.DockedRight, true, new Size(240, Height - 360), DockStyle.Top)))
            {
                return false;
            }
            if (!AddBaseEditorComponent(BaseEditorComponents.PanZoomViewer, new ComponentLayout(ComponentLayoutDockState.DockedRight, true, new Size(240, 200), DockStyle.Fill)))
            {
                return false;
            }

            modelBrowser.DoubleClick += OnModelBrowserDblClcked;
            AddEditorComponent("Test", modelBrowser, new ComponentLayout(ComponentLayoutDockState.DockedLeft, true, new Size(200, (Height / 3) - 80), DockStyle.Top));



            // Complete the initialization.
            CompleteInitialization();

            return true;
        }

        /// <summary>
        /// Builds the desktop host and the document object.
        /// </summary>
        [Description("Builds the desktop host and the document object.")]
        protected override void BuildDocument()
        {
            // If there is a document name, load the document.
            if (documentName != null)
                LoadFileDocument(documentName);
            else
                CreateEmptyDocument();

        }

        /// <summary>
        /// Builds any optional user interface for the application.
        /// </summary>
        /// <remarks>
        /// Custom application implementors can override this method and add 
        /// any additional menu or toolbar items.
        /// </remarks>
        [Description("Builds any optional user interface for the application.")]
        protected override void BuildOptionalUserInterface()
        {
            // TODO : add optional UI here based on the following sample code.

            //			// Add a new sub menu after the tools menu.
            //			MenuItem toolsSubMenu = GetSubMenu("Tools");
            //			MenuItem customSubMenu = new MenuItem("&Custom");
            //			AddSubMenu(customSubMenu, toolsSubMenu.Index + 1);
            //
            //			// Add the custom commands.
            //			Command command = new Command();
            //			command.Owner = this;
            //			command.Name = "CustomCommand";
            //			command.Click += 
            //				new EventHandler(this.command_CustomCommand_Clicked);
            //			this.Commands.Add(command.Name, command);
            //			this.Commands[command.Name].Enabled = false;
            //
            //			// Add the UI for the custom commands.
            //			AppendMenuCommandInstance(
            //				"CustomCommand",
            //				"&Custom Command",
            //				-1,
            //				"Custom command sample",
            //				customSubMenu);
        }

        // Builds any application-specific commands.
        [Description("Builds any application-specific commands.")]
        protected override void BuildOptionalCommands()
        {
            base.BuildOptionalCommands();

            // Create our custom command and add it to the command dictionary.
            Command commandOpenSchema = new Command();
            commandOpenSchema.Owner = this;
            commandOpenSchema.Name = "OpenSchema";
            commandOpenSchema.Click += new EventHandler(OnOpenSchemaClicked);
            commandOpenSchema.Description = "Open existing data schema";
            commandOpenSchema.Enabled = true;
            Commands.Add(commandOpenSchema.Name, commandOpenSchema);

            // Create our custom command and add it to the command dictionary.
            Command command = new Command();
            command.Owner = this;
            command.Name = "Test";
            command.Click += new EventHandler(OnTestClicked);
            command.Description = "Test Action";
            command.Enabled = true;
            Commands.Add(command.Name, command);
        }
        #endregion // Methods

        #region Events
        /// <summary>
        /// Called when [model browser DBL clcked].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnModelBrowserDblClcked(object sender, EventArgs e)
        {
            object obj = modelBrowser.SelectedSchemaObject;
            if (obj == null)
                return;
            string typeName = obj.GetType().Name;
            EditorDocument editorDocument = (EditorDocument)Designer.ActiveDocument;

            switch (typeName)
            {
                case "TableSchema":

                    editorDocument.InnerDocument.ActivePage.Elements.Add(new TableElement((TableSchema)obj));
                    break;
                case "ForeignKey":
                    editorDocument.InnerDocument.ActivePage.Elements.Add(new ForeignKeyElement((ForeignKeySchema)obj));
                    break;
                default:
                    break;
            }
        }
        private void OnTestClicked(object sender, EventArgs e)
        {
            modelBrowser.ShowSchema(@"..\..\test.schema");
            modelBrowser.ExpandAll();
        }

        private void OnOpenSchemaClicked(object sender, EventArgs e)
        {
            // Opens Open File dialog for diagram file.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Diagram files (*" + DatabaseSchema.Extension + ")|*" + DatabaseSchema.Extension + "|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            // Shows the welcome wizard form. 
            openFileDialog.InitialDirectory = Application.StartupPath;

            // Creates new image map project.
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                modelBrowser.ShowSchema(openFileDialog.FileName);
            }
        }
        #endregion // Events

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        [Description("Required method for Designer support.")]
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorForm));
            this.SuspendLayout();
            // 
            // commandImageList
            // 
            this.commandImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("commandImageList.ImageStream")));
            this.commandImageList.Images.SetKeyName(0, "");
            this.commandImageList.Images.SetKeyName(1, "");
            this.commandImageList.Images.SetKeyName(2, "");
            this.commandImageList.Images.SetKeyName(3, "");
            this.commandImageList.Images.SetKeyName(4, "");
            this.commandImageList.Images.SetKeyName(5, "");
            this.commandImageList.Images.SetKeyName(6, "");
            this.commandImageList.Images.SetKeyName(7, "");
            this.commandImageList.Images.SetKeyName(8, "");
            this.commandImageList.Images.SetKeyName(9, "");
            this.commandImageList.Images.SetKeyName(10, "");
            this.commandImageList.Images.SetKeyName(11, "");
            this.commandImageList.Images.SetKeyName(12, "");
            this.commandImageList.Images.SetKeyName(13, "");
            this.commandImageList.Images.SetKeyName(14, "");
            this.commandImageList.Images.SetKeyName(15, "");
            this.commandImageList.Images.SetKeyName(16, "");
            this.commandImageList.Images.SetKeyName(17, "");
            this.commandImageList.Images.SetKeyName(18, "");
            this.commandImageList.Images.SetKeyName(19, "");
            this.commandImageList.Images.SetKeyName(20, "");
            // 
            // EditorForm
            // 
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditorForm";
            this.Text = "Custom Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        #region Application Entry Point

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="arguments">
        /// The command line arguments.
        /// </param>
        /// <returns>
        /// The result of running the application (error code).
        /// </returns>
        [Description("The main entry point for the application.")]
        [STAThread]
        static int Main(string[] arguments)
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            // Check the parameters.
            string documentName = null;
            foreach (string argument in arguments)
            {
                if (argument[0] == '/' || argument[0] == '-')
                {
                    // TODO : handle any switches here.
                    MessageBox.Show(
                        "Invalid argument: " + argument,
                        "Diagram Editor",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return 1;
                }
                else
                {
                    if (documentName != null)
                    {
                        MessageBox.Show(
                            "Two document names were provided, please use only " +
                            "one document name in the command line",
                            "Diagram Editor",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return 1;
                    }

                    documentName = argument;
                }
            }

            // If we can use XP styles, do so.
            if (Environment.OSVersion.Version.Major > 5 ||
                (Environment.OSVersion.Version.Major == 5 &&
                Environment.OSVersion.Version.Minor >= 1))
            {
                Application.EnableVisualStyles();
                Application.DoEvents();
            }

            // Build splash screen region's path.
            GraphicsPath path = new GraphicsPath();
            path.FillMode = FillMode.Winding;
            path.AddRectangle(new Rectangle(9, 20, 460, 288));
            path.AddEllipse(new Rectangle(337, 0, 132, 132));

            // Show the splash screen.
            SplashScreen.CurrentSplash = new SplashScreen();
            SplashScreen.CurrentSplash.Region = new Region(path);
            SplashScreen.CurrentSplash.SplashImage = new Bitmap(typeof(EditorForm), "Images.Splash.png");
            SplashScreen.CurrentSplash.VersionLocation = new Point(140, 125);
            SplashScreen.CurrentSplash.MessageLocation = new Point(16, 145);
            SplashScreen.CurrentSplash.Show();
            SplashScreen.CurrentSplash.Message = "Preparing application...";

            // Run the application with the main form.
            EditorForm form = new EditorForm(documentName);
            if (!form.InitializeForm())
                return 2;
            form.Icon = new Icon(typeof(EditorForm), "App.ico");
            if (SplashScreen.CurrentSplash.Cancelled)
                return 1;
            Application.Run(form);
            return 0;
        }

        /// <summary>
        /// Handles the ThreadException event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Threading.ThreadExceptionEventArgs"/> instance containing the event data.</param>
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ExceptionForm.ShowException("Mode Designer Error", "Unhandled Exception", e.Exception);
        }

        #endregion // Application Entry Point
    }

    #endregion // EditorForm
}
