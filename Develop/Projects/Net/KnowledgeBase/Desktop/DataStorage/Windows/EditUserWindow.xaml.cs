using System;
using System.Windows;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Controls;
using KnowledgeBase.Desktop.Controls;
using Savchin.Validation;
using Savchin.Wpf.Controls.Core;

namespace KnowledgeBase.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        private int id;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditUserWindow"/> class.
        /// </summary>
        public EditUserWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        public static void CreateUser()
        {
            var win = new EditUserWindow
            {
                Owner = Application.Current.MainWindow,
                Title = "Create new user"
            };
            win.ShowDialog();
        }

        /// <summary>
        /// Edits the user.
        /// </summary>
        /// <param name="id">The id.</param>
        public static void EditUser(int id)
        {
            var entity = KbContext.CurrentKb.ManagerUser.GetByID(id);
            if (entity == null) return;

            var win = new EditUserWindow
            {
                Owner = Application.Current.MainWindow,
                Title = string.Format("Edit '{0}' user", entity.Login)
            };
            win.ShowUser(entity);
            win.ShowDialog();
        }


        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveUser();

                Close();
            }
            catch (ValidationException ex)
            {
                ErrorLabel.ShowException(this, "Error save knowledge", ex);
            }
            catch (Exception ex)
            {
                ErrorForm.Show("Error save knowledge", ex);
            }
        }



        /// <summary>
        /// Saves the knowledge.
        /// </summary>
        private void SaveUser()
        {
            var entity = id > 0 ? KbContext.CurrentKb.ManagerUser.GetByID(id) : new User();
            if (entity == null) entity = new User();

            entity.Login = boxLogin.Text.Trim();
            entity.Password = boxPassword.Password;
            entity.FirstName = boxFirstName.Text.Trim();
            entity.LastName = boxLastName.Text.Trim();
            entity.Email = boxEmail.Text.Trim();
            entity.SecurityAnswer = boxSecurityAnswer.Text.Trim();
            entity.SecurityQuestion = boxSecurityQuestion.Text.Trim();

            KbContext.CurrentKb.ManagerUser.Save(entity, boxUserRights.GetPermissions());
        }


        private void ShowUser(User entity)
        {
            id = entity.UserID;

            boxLogin.Text = entity.Login;
            boxPassword.Password = entity.Password;
            boxFirstName.Text = entity.FirstName;
            boxLastName.Text = entity.LastName;
            boxEmail.Text = entity.Email;
            boxSecurityAnswer.Text = entity.SecurityAnswer;
            boxSecurityQuestion.Text = entity.SecurityQuestion;

            var rights = KbContext.CurrentKb.ManageUserRight.GetByUserID(entity.UserID);
            boxUserRights.ShowRights(entity, rights);
        }
    }
}
