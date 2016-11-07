using System;
using System.ComponentModel;
using System.Windows.Forms;
using FlatSearcher.Controls;
using FlatSearcher.Core;
using MyCustomWebBrowser.Core;
using Savchin.Forms.Core.Commands;
using Savchin.Logging;

namespace FlatSearcher
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            panelSearch.webBrowser.StartNewWindow += webBrowser_StartNewWindow;
            UpdateStatus();
            exportToExcelToolStripMenuItem.Tag = panelMap;
            exportToExcelToolStripMenuItem.BindCommand(new ExportToExcelCommand());

            this.Icon = Resources.house;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);


            var db = SearchContext.Current.Data;

            var result = panelMap.GetPoints();
            db.Criteria.Polygons = result;
            db.Save();
            var settings= Properties.Settings.Default;
            settings.FileName = db.FileName;
            settings.Save();
        }


        void webBrowser_StartNewWindow(object sender, ExtendedWebBrowser2.BrowserExtendedNavigatingEventArgs e)
        {
            var control = new SearchResultControl
                              {
                                  Dock = DockStyle.Fill,
                                  Map = panelMap
                              };
            e.AutomationObject = control.Browser.Application;
            control.FlatReaded += control_FlatReaded;


            var tab = new TabPage { Text = "Loading..." };

            tab.Controls.Add(control);


            tabs.TabPages.Add(tab);
            tabs.SelectedTab = tab;
        }

        void control_FlatReaded(object sender, FlatEventArgs e)
        {
            panelSearch.ParseResults();
        }

        #region Menu Handlers
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (var d = new OpenFileDialog())
            {
                d.Filter = "Text files (*.xml)|*.xml|All files (*.*)|*.*";
                if (d.ShowDialog() != DialogResult.OK) return;
                OpenDataBase(d.FileName);
                UpdateStatus();
            }

        }

        private void OpenDataBase(string file)
        {
            try
            {
                SearchContext.Current.Data = Database.Load(file);
                UpdateStatus();
            }
            catch (Exception ex)
            {
                var message = "Ошибка открытия файла " + file;
                MessageBox.Show(message);
                SearchContext.Current.Log.AddMessage(Severity.Error, message, ex);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SearchContext.Current.Data.Save();
            }
            catch (Exception ex)
            {
                var message = "Ошибка сохранения файла " + SearchContext.Current.Data.FileName;
                MessageBox.Show(message);
                SearchContext.Current.Log.AddMessage(Severity.Error, message, ex);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var d = new SaveFileDialog())
            {
                d.Filter = "Text files (*.xml)|*.xml|All files (*.*)|*.*";
                if (d.ShowDialog() != DialogResult.OK) return;
                SaveDataBase(d.FileName);

            }
        }

        private void SaveDataBase(string fileName)
        {
            try
            {
                SearchContext.Current.Data.Save(fileName);
                UpdateStatus();
                panelMap.Init();
            }
            catch (Exception ex)
            {
                var message = "Ошибка сохранения файла " + fileName;
                MessageBox.Show(message);
                SearchContext.Current.Log.AddMessage(Severity.Error, message, ex);
            }

        }
        #endregion

        private void UpdateStatus()
        {
            toolStripStatusLabel1.Text = SearchContext.Current.Data.FileName;
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panelSearch.SaveQuery();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelSearch.RestoreQuery();
        }

        private void tabs_DoubleClick(object sender, EventArgs e)
        {
            var tab = tabs.SelectedTab;
            if (tab == tabPage1 || tab == tabPage2) return;

            tabs.TabPages.Remove(tab);
        }
    }
}
