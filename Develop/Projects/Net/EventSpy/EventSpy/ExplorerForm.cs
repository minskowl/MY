using System;
using System.IO;
using System.Windows.Forms;
using NUnit.Extensions.Forms;
using NUnit.Extensions.Forms.Recorder;
using Savchin.EventSpy.Commands;
using Savchin.EventSpy.Consoles;
using Savchin.EventSpy.Core;
using Savchin.Forms.Core.Commands;
using Savchin.Forms.Docking;
using Savchin.WinApi;

namespace Savchin.EventSpy
{
    public partial class ExplorerForm : Form
    {
        private readonly StartFormCommand _startFormCommand = new StartFormCommand();


        private readonly OutputWindow _mOutputWindow = new OutputWindow();
        private readonly PropertyWindow _mPropertyWindow = new PropertyWindow();
        private readonly AssemblyWindow _mAssemblyWindow = new AssemblyWindow();
        private readonly FormsWindow _mFormsWindow = new FormsWindow();

        private bool _loading = true;
        private readonly string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");


        private TestWriter _testWriter;
        private EventHandler _handlerTest;

        #region Initailze
        /// <summary>
        /// Initializes a new instance of the <see cref="ExplorerForm"/> class.
        /// </summary>
        public ExplorerForm()
        {

            InitializeComponent();

            EventSpyCore.MainForm = this;
            EventSpyCore.Output = _mOutputWindow;
            startFormToolStripMenuItem.BindCommand(_startFormCommand);
        }

        /// <summary>
        /// Handles the FormClosing event of the ExplorerForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void ExplorerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dockPanel.SaveAsXml(configFile);

        }

        /// <summary>
        /// Handles the Load event of the ExplorerForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ExplorerForm_Load(object sender, EventArgs e)
        {

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, GetContentFromPersistString);


        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Activated"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (_loading)
            {
                _loading = false;
                _startFormCommand.Execute(null,this);

            }

        }
        #endregion

        /// <summary>
        /// Starts the explore.
        /// </summary>
        public void StartExplore(Form form, bool inline)
        {
            EventSpyCore.ExploreForm = form;
            _mPropertyWindow.SelectedForm = form;
            _mAssemblyWindow.AddCurentDomain();
            if (inline)
            {
                this.IsMdiContainer = true;
                form.MdiParent = this;

            }
            form.Show();
      


        }


        private IDockContent GetContentFromPersistString(string persistString)
        {

            if (persistString == typeof(PropertyWindow).ToString())
                return _mPropertyWindow;
            if (persistString == typeof(AssemblyWindow).ToString())
                return _mAssemblyWindow;
            if (persistString == typeof(OutputWindow).ToString())
                return _mOutputWindow;
            if (persistString == typeof(FormsWindow).ToString())
                return _mFormsWindow;


            return null;
        }


        void controlSelector_ControlSelected(object sender, FormComponentEventArgs e)
        {
            _mPropertyWindow.SelectedObject = e.Component;
            try
            {
                AddLog("NUnit FullName Name: " + ControlHelper.GetFullName(e.Form, e.Component));
            }
            catch(AmbiguousNameException)
            {
                AddLog("No NUnit FullName Name");
            }
            try
            {
                AddLog("NUnit UniqueShort Name: " + ControlHelper.GetUniqueShortName(e.Form, e.Component));
            }
            catch (AmbiguousNameException)
            {
                AddLog("No NUnit UniqueShort Name");
            }
            //AddLog(e.Control.Bounds.ToString());
            selectControlToolStripMenuItem.Checked = false;
        }




        private void AddLog(string message)
        {
            _mOutputWindow.AddMessage(message);
        }



        #region MenuItems
        /// <summary>
        /// Handles the Click event of the selectControlToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void selectControlToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (selectControlToolStripMenuItem.Checked)
            {
                controlSelector.IsSelectionEnabled = false;
                selectControlToolStripMenuItem.Checked = false;
            }
            else
            {
                controlSelector.IsSelectionEnabled = true;
                selectControlToolStripMenuItem.Checked = true;
            }
        }


        /// <summary>
        /// Handles the Click event of the outputToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mOutputWindow.Show(dockPanel);
        }

        /// <summary>
        /// Handles the Click event of the propertyToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void propertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mPropertyWindow.Show(dockPanel);
        }
        /// <summary>
        /// Handles the Click event of the assembyToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void assembyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mAssemblyWindow.Show(dockPanel);
        }

        /// <summary>
        /// Handles the Click event of the formsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void formsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mFormsWindow.Show(dockPanel);
        }

        /// <summary>
        /// Handles the Click event of the captureTestToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void captureTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_handlerTest == null)
                _handlerTest = new EventHandler(TestChanged);

            if (captureTestToolStripMenuItem.Checked)
            {
                _testWriter.TestChanged -= _handlerTest;
                captureTestToolStripMenuItem.Checked = false;
            }
            else
            {
                if (_testWriter == null)
                {
                    CreateTestWiter();
                }
                _testWriter.TestChanged += _handlerTest;
                captureTestToolStripMenuItem.Checked = true;
            }
        }



        #endregion

        /// <summary>
        /// Tests the changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TestChanged(object sender, EventArgs e)
        {
            _mOutputWindow.AddMessage(Logger.Test, _testWriter.Test);
        }


        private void CreateTestWiter()
        {
            _testWriter = new TestWriter(EventSpyCore.ExploreForm);
            _testWriter.FormsExcludeListening.Add(typeof(StartUpForm));
            _testWriter.FormsExcludeListening.Add(typeof(Form1));
            _testWriter.FormsExcludeListening.Add(typeof(AssemblyWindow));
            _testWriter.FormsExcludeListening.Add(typeof(OutputWindow));
            _testWriter.FormsExcludeListening.Add(typeof(PropertyWindow));
            _testWriter.FormsExcludeListening.Add(GetType());
        }

        private void mouseTrackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(mouseTrackingToolStripMenuItem.Checked)
            {
                mouseTrackingToolStripMenuItem.Checked = false;
                globalEventProvider1.MouseMove -= new MouseEventHandler(globalEventProvider1_MouseMove);
            }
            else
            {
                mouseTrackingToolStripMenuItem.Checked = true;
                globalEventProvider1.MouseMove += new MouseEventHandler(globalEventProvider1_MouseMove);
            }
        }

        void globalEventProvider1_MouseMove(object sender, MouseEventArgs e)
        {
            statusLabelMouseCoord.Text = string.Format("X={0} Y={1}", e.X, e.Y);
        }







    }
}
