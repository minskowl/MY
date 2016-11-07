using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using BotvaSpider.Commands;
using BotvaSpider.Configuration;
using BotvaSpider.Consoles;
using BotvaSpider.Controls;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Logging;

using Savchin.Forms.Core.Commands;
using Savchin.Forms.Docking;
using Savchin.Forms.Helpers;
using Savchin.WinApi;

namespace BotvaSpider
{
    /// <summary>
    /// MainForm
    /// </summary>
    public partial class MainForm : MainFormBase
    {

        readonly OutputWindow m_outputWindow = new OutputWindow();
        private readonly string configFileDockPanel = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

        /// <summary>
        /// Gets the dock panel.
        /// </summary>
        /// <value>The dock panel.</value>
        public override DockPanel DockPanel
        {
            get { return dockPanel; }
        }

        #region Initialize
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            IsMdiContainer = true;
            Icon = Properties.Resources.logo;
            notifyIcon.Icon = Resources.Gradient_Ok;

            alertDelegate = Alert;
#if DEBUG
            testToolStripMenuItem.Visible = true;
#else
            testToolStripMenuItem.Visible = false;
            messageListToolStripMenuItem.Visible = false;
#endif
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode) return;

            if (!ValidateDbConnection())
            {
                Application.Exit();
                Close();
                return;
            }
            normalBackGround = BackColor;
            Text += " " + AppCore.Version;

            BindCommands();

            InitializeDocking();

            foreach (var file in GameSettings.GetConfigs())
            {
                var item = (ToolStripMenuItem)savedConfigsToolStripMenuItem.DropDownItems.Add(Path.GetFileName(file));
                item.Tag = file;
                item.Checked = item.Text == AppCore.AppSettings.GameConfig;
                item.Click += LoadConfig_Click;

            }

            AppCore.LogSystem.EntryAdded += LogApp_EntryAdded;
            AppCore.LogFights.EntryAdded += LogApp_EntryAdded;
            AppCore.LogOutput.EntryAdded += LogApp_EntryAdded;
            LoadModules();
        }




        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            SaveDockingState();
            base.OnClosing(e);
        }



        /// <summary>
        /// Handles the FormClosing event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppCore.Unload();
        }
        #endregion

        #region Alerts

        void LogApp_EntryAdded(object sender, LogEntryEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(alertDelegate, new object[1] { e.Entry });
            }
            else
            {
                Alert(e.Entry);
            }

        }

        private Color normalBackGround;
        private LogEntryType currentAllert = 0;

        /// <summary>
        /// Gets a value indicating whether this instance is show alert.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is show alert; otherwise, <c>false</c>.
        /// </value>
        public bool IsShowAlert
        {
            get { return BackColor != normalBackGround; }
        }

        private readonly AlertHandler alertDelegate;
        private delegate void AlertHandler(LogEntry entry);
        /// <summary>
        /// Alerts the specified type.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public void Alert(LogEntry entry)
        {
            try
            {
                if (!AppCore.BotvaSettings.ShowAllerts) return;

                if (currentAllert > entry.Type) return;
                if (entry.Type < LogEntryType.Suggestion) return;

                currentAllert = entry.Type;
                notifyIcon.BalloonTipTitle = entry.Title;
                notifyIcon.BalloonTipText = string.IsNullOrEmpty(entry.Message) ? entry.Title : entry.Message;

                switch (entry.Type)
                {
                    case LogEntryType.Suggestion:
                        this.Flash(FLASHW.FLASHW_ALL, 5, 0);
                        BackColor = Color.Blue;
                        notifyIcon.Icon = Resources.Rounded_Help;
                        notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                        break;
                    case LogEntryType.Warning:
                        this.Flash(FLASHW.FLASHW_ALL, 20, 0);
                        BackColor = Color.Yellow;
                        notifyIcon.Icon = Resources.Warning;
                        notifyIcon.BalloonTipIcon = ToolTipIcon.Warning;
                        break;
                    case LogEntryType.Error:
                        this.Flash(FLASHW.FLASHW_ALL, 100, 0);
                        BackColor = Color.Red;
                        notifyIcon.Icon = Resources.Gradient_Cancel;
                        notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
                        break;
                    default:
                        return;
                }
                notifyIcon.ShowBalloonTip(0);
            }
            catch (Exception ex)
            {
                AppCore.LogSystem.Log.Error("Error show alert in MainForm", ex);
            }
        }

        /// <summary>
        /// Unsets the error alert.
        /// </summary>
        public override void HideAlert()
        {
            if (!IsShowAlert) return;

            BackColor = normalBackGround;
            this.StopFlash();
            notifyIcon.Icon = Resources.Gradient_Ok;
        }

        /// <summary>
        /// Adds the console.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="console">The console.</param>
        public override void AddConsole<T>(string name)
        {
            //TODO: Uncomment
            //windowsToolStripMenuItem.DropDownItems.Add(name).BindCommand(new ConsoleCommand<T>());
        }

        #endregion

        #region Events Handlers

        #region MenuItems
        /// <summary>
        /// Handles the Click event of the outputToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_outputWindow.Show(dockPanel);
        }


        /// <summary>
        /// Handles the Click event of the LoadConfig control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LoadConfig_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem downItem in savedConfigsToolStripMenuItem.DropDownItems)
            {
                downItem.Checked = false;

            }
            var item = (ToolStripMenuItem)sender;
            item.Checked = true;
            var newSettings = (string)item.Tag;
            AppCore.SetActiveSettings(newSettings);
        }

        /// <summary>
        /// Handles the Click event of the aboutToolStripMenuItem1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var form = new FormAbout())
            {
                form.ShowDialog();
            }
        }

        /// <summary>
        /// Handles the Click event of the maskingToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void maskingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            notifyIcon.Visible = true;
        }

        /// <summary>
        /// Handles the Click event of the testToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormTest();
            form.Show();
        }
        #endregion

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon.Visible = false;
        }

        #endregion

        #region Docking Support

        private void InitializeDocking()
        {
            if (File.Exists(configFileDockPanel))
                dockPanel.LoadFromXml(configFileDockPanel, GetContentFromPersistString);
        }
        private void SaveDockingState()
        {
            dockPanel.SaveAsXml(configFileDockPanel);
        }
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(OutputWindow).ToString())
                return m_outputWindow;

            return null;
        }

        #endregion

        private bool ValidateDbConnection()
        {
            if (ObjectProvider.Instance.ConnectionIsValid()) return true;

            var form = FormFileSelect.Create(
                "Не могу найти базу коров",
                "Найдите у себя файл Botva.mdb без него бот не работает.",
                "Botva.mdb");

            do
            {
                if (form.ShowDialog() != DialogResult.OK) return false;

                AppCore.GameSettings.DatabasePath = form.FileName;
                AppCore.GameSettings.Save();
            }
            while (!ObjectProvider.Instance.ConnectionIsValid());

            return true;
        }

        private void LoadModules()
        {
            var direcory = new DirectoryInfo(Path.Combine(AppSettings.ApplicatioPath, "Modules"));
            var modules = direcory.GetFiles("*.dll");
            foreach (var module in modules)
            {
                LoadModule(module.FullName);

            }
        }
        private void LoadModule(string filePath)
        {
            try
            {
                var assembly = Assembly.LoadFile(filePath);
                var types = assembly.GetTypes();
                AddConsoles(types);
            }
            catch (Exception ex)
            {
                AppCore.LogSystem.Warn("Ошибка загрузки модуля " + Path.GetFileNameWithoutExtension(filePath),
                    filePath, ex);
            }
        }

        private void AddConsoles(Type[] types)
        {
            var moduleType = typeof(IModule);
            foreach (var type in types)
            {
                if (!type.IsInterface && type.IsClass && type.GetInterface("IModule") != null)
                {
                    var module = (IModule)Activator.CreateInstance(type);
                    module.Initilaize(this, statisticToolStripMenuItem);
                }
            }
        }

        private void BindCommands()
        {
            settingsToolStripMenuItem.BindCommand(new SettingsEditCommand());

            crystalsToolStripMenuItem.BindCommand(new ConsoleCommand<CrystalMapConsole>());
            smithyToolStripMenuItem.BindCommand(new ConsoleCommand<SmithyConsole>());
   
            balanceToolStripMenuItem.BindCommand(new ShowBalanceStatisticsCommand());
        }



    }
}
