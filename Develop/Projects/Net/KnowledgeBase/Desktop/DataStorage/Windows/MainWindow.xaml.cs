using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AvalonDock;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Core;
using KnowledgeBase.Desktop.Controls.Docs;
using KnowledgeBase.Desktop.Core;
using Microsoft.Win32;
using Savchin.Core;
using Savchin.Wpf.Core;


namespace KnowledgeBase.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private DocServer _server = new DocServer();
        private readonly NavigateHistory _history = new NavigateHistory();
        /// <summary>
        /// Gets or sets the open documents.
        /// </summary>
        /// <value>The open documents.</value>
        public ObservableCollection<DocumentContent> OpenDocuments { get; set; }

        private string layoutFilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            OpenDocuments = new ObservableCollection<DocumentContent>();

            InitializeComponent();

            if (!this.IsDesignMode()) Init();
        }

        private void Init()
        {
            var user = KbContext.CurrentKb.GetCurrentUser();

            layoutFilePath = Path.Combine(AppInfo.ApplicationPath,
                              string.Format("Layout.{0}.xml", user.Login));

            Title += " " + user.Login;
            //  ColorFactory.ChangeColors(Colors.Black);

            DataContext = this;
            listUserFiles.DataContext = AppCore.Workspace.UserFiles;


            if (!KbContext.CurrentKb.HasUserAdminPermission)
            {
                docUsers.Hide();
                docKeywords.Hide();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.FrameworkElement.Initialized"/> event. This method is invoked whenever <see cref="P:System.Windows.FrameworkElement.IsInitialized"/> is set to true internally.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs"/> that contains the event data.</param>
        protected override void OnInitialized(System.EventArgs e)
        {
            base.OnInitialized(e);
            categoryInfo.CategoryID = 0;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var w = App.Current.Windows.OfType<NotifyWindow>().FirstOrDefault();
            if (w != null)
            {
                e.Cancel = true;
                Hide();
                return;
            }
            _dockingManager.SaveLayout(layoutFilePath);

            _server.Dispose();
            _server = null;
        }

        #region Event Handlers
        /// <summary>
        /// Categories the tree selected category changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KnowledgeBase.Desktop.Core.ObjectIdentifierEventArgs"/> instance containing the event data.</param>
        private void CategoryTreeSelectedCategoryChanged(object sender, Core.ObjectIdentifierEventArgs e)
        {
            ViewCategory(e.Id, true);
        }

        #region Commands
        /// <summary>
        /// Handles the CanExecute event of the EditCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void ItemCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // e.Handled = true;
            var param = (e.Parameter as ItemParams);
            if (param == null) return;


            if (param.Type == ObjectType.File)
            {
                e.Handled = false;
            }
            else
            {
                e.CanExecute = true;
            }

        }

        /// <summary>
        /// Handles the Executed event of the FindCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void FindCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var filter = e.Parameter as SearchFilter;
            if (filter == null) return;

            if (!IsVisible)
                Show();

            var doc = new SearchResults { Filter = filter };
            OpenDocuments.Add(doc);
            _dockingManager.ActiveDocument = doc;
        }

        #region Manage Commands
        /// <summary>
        /// Handles the Executed event of the EditCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var param = (ItemParams)e.Parameter;
            switch (param.Type)
            {
                case ObjectType.Category:
                    EditCategoryWindow.EditCategory(param.IntId);

                    break;
                case ObjectType.Knowledge:
                    EditKnowledgeWindow.EditKnowledge(param.IntId);
                    break;
                case ObjectType.User:
                    EditUserWindow.EditUser(param.IntId);
                    docUsers.ResetData();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Handles the Executed event of the OpenCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            var param = (ItemParams)e.Parameter;
            switch (param.Type)
            {
                case ObjectType.Category:
                    ViewCategory(param.IntId, true);
                    break;
                case ObjectType.Knowledge:
                    ViewKnowledge(param.IntId);
                    break;
                default:
                    e.Handled = false;
                    break;
            }

        }
        /// <summary>
        /// Handles the Executed event of the DeleteCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                DeleteItem(e);
            }
            catch (PermissionException ex)
            {
                Messages.ShowSecurityAlert(ex);
            }
        }

        /// <summary>
        /// Handles the Executed event of the ExportCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void ExportCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            var param = e.Parameter as ItemParams;
            if (param == null) return;
            switch (param.Type)
            {
                case ObjectType.Category:

                    var dialog = new SaveFileDialog()
                    {
                        Title = "Create database file",
                        Filter = LocalFileContext.FileFilter,
                        CreatePrompt = false,
                        OverwritePrompt = true
                    };

                    if (!(dialog.ShowDialog(Application.Current.MainWindow) ?? false))
                        return;

                    var destination = LocalFileContext.CreateDatabase(dialog.FileName);

                    var copier = new ObjectCopier();

                    copier.Copy(KbContext.CurrentKb, destination, param.IntId);
                    MessageBox.Show("Export finished.", "Export");
                    break;
                default:
                    e.Handled = false;
                    break;
            }
        }

        /// <summary>
        /// Handles the Executed event of the AddCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void AddCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            var param = e.Parameter as ItemParams;
            if (param == null) return;
            switch (param.Type)
            {
                case ObjectType.Category:
                    EditCategoryWindow.CreateCategory(param.IntId);
                    break;
                case ObjectType.Knowledge:
                    EditKnowledgeWindow.CreateKnowledge(param.IntId);
                    categoryInfo.ReloadItems();
                    break;
                case ObjectType.User:
                    EditUserWindow.CreateUser();
                    docUsers.ResetData();
                    break;
                default:
                    e.Handled = false;
                    break;
            }


        }
        #endregion

        #region Navigation
        /// <summary>
        /// Handles the Executed event of the BackwardCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void BackwardCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var categoryId = _history.Backward();
            if (categoryId > -1) ViewCategory(categoryId, false);
        }
        /// <summary>
        /// Handles the CanExecute event of the BackwardCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void BackwardCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _history.CanBackward;
        }

        /// <summary>
        /// Handles the Executed event of the ForwardCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void ForwardCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var categoryId = _history.Forward();
            if (categoryId > -1) ViewCategory(categoryId, false);
        }

        /// <summary>
        /// Handles the CanExecute event of the ForwardCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void ForwardCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _history.CanForward;
        }


        private void HideToTrayCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var w = App.Current.Windows.OfType<NotifyWindow>().FirstOrDefault() ?? new NotifyWindow();
            w.Show();
            this.Hide();
        }
        #endregion

        #endregion



        private void DockingManagerLoaded(object sender, RoutedEventArgs e)
        {
            LoadLayout();
        }

        private void DockingManager_RequestDocumentClose(object sender, RequestDocumentCloseEventArgs e)
        {
            OpenDocuments.Remove(e.DocumentToClose);
        }
        private void TestItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new TestWindow();
            window.Show();
        }

        private void itemWebServer_Click(object sender, RoutedEventArgs e)
        {
            if (itemWebServer.IsChecked)
            {
                itemWebServer.Header = "Start server";
                _server.Stop();
            }
            else
            {
                itemWebServer.Header = "Stop server";
                _server.Start();
            }
            itemWebServer.IsChecked = !itemWebServer.IsChecked;
        }
        #endregion

        #region Managment Actions
        private void DeleteItem(ExecutedRoutedEventArgs e)
        {
            var param = (ItemParams)e.Parameter;
            switch (param.Type)
            {
                case ObjectType.Category:
                    var category = KbContext.CurrentKb.ManagerCategory.GetByID(param.IntId);
                    if (category == null) return;

                    if (MessageBox.Show(string.Format("Are you sure to delete '{0}' category?", category.Name),
                                        "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        KbContext.CurrentKb.ManagerCategory.Delete(category);
                    }
                    break;
                case ObjectType.Knowledge:
                    var knowledge = KbContext.CurrentKb.ManagerKnowledge.GetByID(param.IntId);
                    if (knowledge == null) return;
                    if (MessageBox.Show(string.Format("Are you sure to delete '{0}' knowledge?", knowledge.Title),
                                        "Delete Item", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        KbContext.CurrentKb.ManagerKnowledge.Delete(knowledge);
                        categoryInfo.ReloadItems();
                    }
                    break;
                case ObjectType.User:
                    var user = KbContext.CurrentKb.ManagerUser.GetByID(param.IntId);
                    if (user == null) return;
                    if (MessageBox.Show(string.Format("Are you sure to delete '{0}' user?", user.Login),
                                        "Delete Item", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        KbContext.CurrentKb.ManagerUser.Delete(user);
                        docUsers.ResetData();
                    }
                    break;
                default:
                    e.Handled = false;
                    break;
            }
        }
        private void ViewCategory(int id, bool storeHistory)
        {
            if (storeHistory) _history.Add(categoryInfo.CategoryID);
            categoryInfo.CategoryID = id;
        }

        private void ViewKnowledge(int id)
        {

            var doc = OpenDocuments.OfType<KnowledgeViewControl>().FirstOrDefault(e => e.Knowledge.KnowledgeId == id);
            if (doc == null)
            {
                var knowledge = KbContext.CurrentKb.ManagerKnowledge.GetByID(id);
                if (knowledge == null) return;


                doc = new KnowledgeViewControl { Knowledge = new KnowledgeView(knowledge) };
                //doc.InfoTip = "Info tipo for " + doc.Title;
                //doc.ContentTypeDescription = "Sample document";
                //doc.Closing += new EventHandler<System.ComponentModel.CancelEventArgs>(doc_Closing);
                //doc.Closed += new EventHandler(doc_Closed);
                OpenDocuments.Add(doc);
            }
            _dockingManager.ActiveDocument = doc;
        }
        #endregion

        private void LoadLayout()
        {
            if (!File.Exists(layoutFilePath))
                return;

            _dockingManager.DeserializationCallback = (s, e_args) =>
            {
                if (e_args.Name == "_contentDummy")
                {
                    e_args.Content = new DockableContent
                    {
                        Title = "Dummy Content",
                        Content = new TextBlock()
                        {
                            Text =
                                "Content Loaded On Demand!"
                        }
                    };
                }
            };
            try
            {
                using (var fs = new FileStream(layoutFilePath, FileMode.Open, FileAccess.Read))
                {
                    _dockingManager.RestoreLayout(fs);


                }
            }

            catch (Exception ex)
            {
                //KbContext.Current.
            }
        }



        private void ShowDocking_Click(object sender, RoutedEventArgs e)
        {
            var menu = (MenuItem)sender;

            if (menu.Tag is DockableContent)
            {
                Toggle((DockableContent)menu.Tag);
            }
            else if (menu.Tag is DocumentContent)
            {
                Toggle((DocumentContent)menu.Tag);
            }

        }
        private void Toggle(DockableContent doc)
        {
            if (doc.State == DockableContentState.Hidden)
                doc.Show();
            else
                doc.Hide();
        }
        private void Toggle(DocumentContent doc)
        {

        }


    }
}
