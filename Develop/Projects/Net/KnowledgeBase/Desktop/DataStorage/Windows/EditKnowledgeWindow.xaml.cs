using System.IO;
using System.Linq;
using System.Windows;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Core;
using KnowledgeBase.Desktop.Models;
using Savchin.Core;

namespace KnowledgeBase.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for EditKnowledgeWindow.xaml
    /// </summary>
    public partial class EditKnowledgeWindow
    {


        private ISummaryEditor boxSummary;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditKnowledgeWindow"/> class.
        /// </summary>
        public EditKnowledgeWindow()
        {
            InitializeComponent();

            boxSummary = AppCore.GetObject<ISummaryEditor>();
            tabSummary.Content = boxSummary;


            listUserFiles.DataContext = AppCore.Workspace.UserFiles;
        }



        /// <summary>
        /// Creates the knowledge.
        /// </summary>
        /// <param name="parentId">The parent id.</param>
        public static void CreateKnowledge(int parentId)
        {
            var win = new EditKnowledgeWindow
                          {
                              Owner = Application.Current.MainWindow,
                          };
            win.DataContext = new EditKnowledgeModel(0, win, win.boxSummary) { Entity = { CategoryID = parentId } }; ;

            win.ShowDialog();
        }

        /// <summary>
        /// Edits the knowledge.
        /// </summary>
        /// <param name="id">The id.</param>
        public static void EditKnowledge(int id)
        {
            var win = new EditKnowledgeWindow
                     {
                         Owner = Application.Current.MainWindow,
                     };
            win.DataContext = new EditKnowledgeModel(id, win, win.boxSummary);

            win.ShowDialog();
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((EditKnowledgeModel)DataContext).Saved += new System.EventHandler(EditKnowledgeWindow_Saved);
        }

        void EditKnowledgeWindow_Saved(object sender, System.EventArgs e)
        {
            Close();
        }

        #region Event Handlers

        #region Button Handlers

        //private void ButtonOk_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        SaveKnowledge();

        //        Close();
        //    }
        //    catch (ValidationException ex)
        //    {
        //        ErrorLabel.ShowException(this, "Error save knowledge", ex);
        //    }
        //    catch (PermissionException ex)
        //    {
        //        Messages.ShowSecurityAlert(ex.Message, listCategories.SelectedCategoryId);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorForm.Show("Error save knowledge", ex);
        //    }
        //}


        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion




        #endregion


        private void ShowSummary(KnowledgeView view)
        {
            var userFiles = AppCore.Workspace.UserFiles
                .Select(f => new NameValuePair<string>(f.Name, f.Path)).ToArray();
            var artFiles = KbContext.CurrentKb.ManagerFileInclude.GetByKnowledgeID(view.KnowledgeId)
                .Select(f => new NameValuePair<string>(f.FileName, Path.Combine(view.FilesDir, f.FileName))).ToArray();


            boxSummary.UserFiles = userFiles;
            boxSummary.ArticleFiles = artFiles;
            boxSummary.Value = view.Summary;

        }



    }
}
