using System;
using System.Windows;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Controls;
using KnowledgeBase.Core;
using KnowledgeBase.Desktop.Controls;
using KnowledgeBase.Desktop.Core;
using Savchin.Validation;
using Savchin.Wpf.Controls.Core;

namespace KnowledgeBase.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for EditCategoryWindow.xaml
    /// </summary>
    public partial class EditCategoryWindow : Window
    {
        private int id;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditCategoryWindow"/> class.
        /// </summary>
        public EditCategoryWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="parentCategoryId">The parent category id.</param>
        public static void CreateCategory(int parentCategoryId)
        {
            if (!SecurityAgent.HasAdminRights(parentCategoryId)) return;
            var win = new EditCategoryWindow { Owner = Application.Current.MainWindow };
            win.listCategories.Value = parentCategoryId;
            win.Show();
        }

        /// <summary>
        /// Edits the category.
        /// </summary>
        /// <param name="id">The id.</param>
        public static void EditCategory(int id)
        {
            if (!SecurityAgent.HasAdminRights(id)) return;

            var entity = AppCore.Workspace.Categories.GetById(id);
            if (entity == null) return;

            var win = new EditCategoryWindow { Owner = Application.Current.MainWindow };
            win.ShowCategory(entity);
            win.ShowDialog();
        }


        #region Event Handlers

        /// <summary>
        /// Handles the Click event of the ButtonOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveCategory();

                Close();
            }
            catch (ValidationException ex)
            {
                ErrorLabel.ShowException(this, "Error save category", ex);
            }
            catch (PermissionException ex)
            {
                Messages.ShowSecurityAlert(ex.Message, listCategories.Value);
            }
            catch (Exception ex)
            {
                ErrorForm.Show("Error save category", ex);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        private void ShowCategory(Category entity)
        {
            id = entity.CategoryID;
            boxName.Text = entity.Name;
            listCategories.Value = entity.ParentCategoryID ?? 0;
        }

        private void SaveCategory()
        {
            var entity = id > 0 ? AppCore.Workspace.Categories.GetById(id) : new Category();
            if (entity == null) entity = new Category();

            entity.Name = boxName.Text.Trim();
            entity.ParentCategoryID = listCategories.Value;

            AppCore.Workspace.Categories.Save(entity);
        }


    }
}
