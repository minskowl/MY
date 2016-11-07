using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Core;
using BotvaSpider.Logging;
using Savchin.Forms;
using Savchin.Forms.ListView;
using Savchin.Utils;

namespace BotvaSpider.Controls
{
    public partial class LogViewer : UserControl
    {
        private ILogger activeLogger;
        private LogEntryType levelForm;
        private LogEntry[] SearchResults;
        private int searchPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogViewer"/> class.
        /// </summary>
        public LogViewer()
        {
            refreshDelegate = RefreshList;

            InitializeComponent();

            imageList1.Images.Add(new Bitmap(16, 16));
            imageList1.Images.Add(LogEntry.InfoIcon);
            imageList1.Images.Add(LogEntry.SuggestionIcon);
            imageList1.Images.Add(LogEntry.WarningIcon);
            imageList1.Images.Add(LogEntry.ErrorIcon);

            list.CellToolTipGetter = ((column, modelObject) => ((LogEntry)modelObject).Message);

            сolumnTitle.AspectGetter = (x => ((LogEntry)x).Title);
            сolumnData.AspectGetter = (x => ((LogEntry)x).Date);
            columnImage.ImageGetter = (x => (int)((LogEntry)x).Type);
            columnImage.AspectGetter = (x => (int)((LogEntry)x).Type);

        }

        /// <summary>
        /// Sets the level filter.
        /// </summary>
        /// <param name="type">The type.</param>
        public void SetLevelFilter(LogEntryType type)
        {
            levelForm = type;
            SetData();
        }

        /// <summary>
        /// Shows the specified logger type.
        /// </summary>
        /// <param name="loggerType">Type of the logger.</param>
        public void Show(LoggerType loggerType)
        {
            if (activeLogger != null)
            {
                activeLogger.EntryAdded -= activeLogger_EntryAdded;
            }
            activeLogger = AppCore.GetLogger(loggerType);

            activeLogger.EntryAdded += activeLogger_EntryAdded;
            SetData();

        }




        #region Event Handlers
        /// <summary>
        /// Handles the EntryAdded event of the activeLogger control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void activeLogger_EntryAdded(object sender, LogEntryEventArgs e)
        {
            if (list.InvokeRequired)
            {
                list.Invoke(refreshDelegate, new[] { sender, e });
            }
            else
            {
                RefreshList(sender, e);
            }
        }

        /// <summary>
        /// Handles the ItemDoubleClick event of the list control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemEventArgs"/> instance containing the event data.</param>
        private void list_ItemDoubleClick(object sender, ItemEventArgs e)
        {
            var data = (LogEntry)list.GetModelObject(e.Item.Index);
            if (data.Object != null)
                FormObject.ShowObject(AppCore.FormMain, data.Title, data.Object);
        }

        /// <summary>
        /// Handles the ItemClick event of the list control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemEventArgs"/> instance containing the event data.</param>
        private void list_ItemClick(object sender, ItemEventArgs e)
        {
            AppCore.FormMain.HideAlert();
        }

        #region Context Menu

        /// <summary>
        /// Handles the Click event of the copyToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var builder = new StringBuilder();
                foreach (var entry in GetSelectedEntries())
                {
                    builder.AppendLine(entry.ToString());
                }
                Clipboard.SetText(builder.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка", "Несмогли скопировать данные. Попробуйте еще раз.");
                AppCore.LogSystem.Error("Ошибка копирования в буфер в просмотрщике логов.", ex);

            }
        }
        /// <summary>
        /// Handles the Click event of the saveToFileToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (activeLogger == null) return;

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            TypeSerializer<List<LogEntry>>.ToXmlFile(saveFileDialog1.FileName, activeLogger.Entries);
        }
        /// <summary>
        /// Handles the Click event of the autoScrollToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void autoScrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoScrollToolStripMenuItem.Checked = !autoScrollToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Handles the Click event of the searchToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new FormText())
            {
                form.Text = "Искать...";
                if (form.ShowDialog() != DialogResult.OK) return;
                var searchText = form.Value.Trim().ToLower();
                if (string.IsNullOrEmpty(searchText)) return;

                DoSearch(searchText);
                return;
            }
        }



        /// <summary>
        /// Handles the Click event of the findNextToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SearchResults == null || SearchResults.Length - 1 == searchPosition) return;
            searchPosition++;
            ShowEntry(SearchResults[searchPosition], true);
        }
        #endregion

        #endregion

        /// <summary>
        /// Gets the selected entries.
        /// </summary>
        /// <returns></returns>
        private List<LogEntry> GetSelectedEntries()
        {
            var result = new List<LogEntry>();
            foreach (int index in list.SelectedIndices)
            {
                result.Add((LogEntry)list.GetModelObject(index));
            }
            return result;
        }
        /// <summary>
        /// Gets the list of event handlers that are attached to this <see cref="T:System.ComponentModel.Component"/>.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// An <see cref="T:System.ComponentModel.EventHandlerList"/> that provides the delegates for this component.
        /// </returns>
        private LogEntry[] Entries
        {
            get { return activeLogger.Entries.Where(e => e.Type >= levelForm).ToArray(); }
        }

        private void SetData()
        {
            var entries = Entries;
            list.DataSource.SetObjects(entries);
            var count = entries.Length;
            list.VirtualListSize = count;
            if (count > 0)
                ShowEntry(count - 1, true);
            list.Focus();
        }

        private readonly EventHandler<LogEntryEventArgs> refreshDelegate;


        private void RefreshList(object sender, LogEntryEventArgs e)
        {
            try
            {
                if (e.Entry.Type < levelForm) return;

                //TODO: Uncomment
                //list.DataSource.AddObjects(e.Entry);
                if (!list.Visible) return;

                var newSize = list.DataSource.GetObjectCount();
                try
                {
                    list.VirtualListSize = newSize;
                }
                catch
                {
                    list.VirtualListSize = newSize;
                }
                if (autoScrollToolStripMenuItem.Checked)
                {
                    ShowEntry(e.Entry, false);
                }

            }
            catch (Exception ex)
            {
                AppCore.LogSystem.Warn("Ошибка обновления лога.", ex);
            }
        }

        private void ShowEntry(LogEntry entry, bool select)
        {
            ShowEntry(list.DataSource.GetObjectIndex(entry), select);
        }

        private void ShowEntry(int index, bool select)
        {
            list.EnsureVisible(index);
            if (select) list.SelectedIndex = index;
        }

        private void DoSearch(string searchText)
        {
            SearchResults = Entries.Where(entry =>
                                          entry.Title.ToLower().Contains(searchText) ||
                                          (!string.IsNullOrEmpty(entry.Message) && entry.Message.ToLower().Contains(searchText))).ToArray();
            if (SearchResults == null || SearchResults.Length == 0)
            {
                MessageBox.Show("Ничего не найдено");
                return;
            }
            searchPosition = 0;
            ShowEntry(SearchResults[searchPosition], true);
        }
    }
}
